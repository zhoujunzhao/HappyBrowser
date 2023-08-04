using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Text.RegularExpressions;
using System.IO;
using HappyBrowser.Controls.CtlEventArgs;
using Newtonsoft.Json.Linq;
using HappyBrowser.Util;
using System.Text;

namespace HappyBrowser.Services
{
    public class BookMarksService
    {
        public const string BOOK_MARKS_FILE_NAME = "bookmarks.json";
        public static event EventHandler<BookMarkSubMenuClickedEventArgs>? SubMenuClick;

        public static event EventHandler? BookMarksChanged;

        #region 标签导入
        public static void ImportFromFile(string impFile)
        {
            string oriHTML = File.ReadAllText(impFile);
            // 去掉</p>
            oriHTML = oriHTML.Replace("<p>", "").Replace("<P>", "");

            int idx = oriHTML.IndexOf("</H3>");

            string str = oriHTML.Substring(idx, 11).ToLower().Trim();
            bool isExist = str.StartsWith("</h3></dt>");

            if (!isExist)
            {
                oriHTML=oriHTML.Replace("</H3>", "</H3></DT>");
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(oriHTML);
            HtmlNode rootNode = doc.DocumentNode.SelectSingleNode("dl").SelectSingleNode("dl");
            JObject rootItem = new()
            {
                { "id", "root" },
                { "text", "书签栏" }
            };
            CreateChildItem(rootNode.SelectNodes("dt"), rootItem);

            string filePath = Path.Combine(ConfigService.GetRootConfigPath(),BOOK_MARKS_FILE_NAME);
            File.WriteAllText(filePath, rootItem.ToString(),Encoding.UTF8);
            BookMarksChanged?.Invoke(null,null);
        }

        private static void CreateChildItem(HtmlNodeCollection nodes, JObject parentMenu, bool isAddNew = false)
        {
            JArray childItms = new();
            for (int i = 0; i<nodes.Count; i++)
            {
                HtmlNode dtNode = nodes[i].FirstChild;
                JObject itm = new();
                childItms.Add(itm);

                itm.Add("id", UUIDUtil.NewUUID);
                itm.Add("text", dtNode.InnerText);

                if (dtNode.GetAttributeValue("icon", "") != "")
                {
                    itm.Add("icon", dtNode.GetAttributeValue("icon", ""));
                }

                if (dtNode.GetAttributeValue("href", "") != "")
                {
                    itm.Add("href", dtNode.GetAttributeValue("href", ""));
                }

                if (dtNode.GetAttributeValue("add_date", "") != "")
                {
                    itm.Add("add_date", dtNode.GetAttributeValue<long>("add_date", 0));
                }

                if (dtNode.GetAttributeValue("last_modified", "") != "")
                {
                    itm.Add("last_modified", dtNode.GetAttributeValue<long>("last_modified", 0));
                }

                if (dtNode.Name.ToLower() == "h3")// 文件夹
                {
                    HtmlNode tempNode = nodes[i].NextSibling;
                    while (tempNode.Name.ToLower() != "dl")
                    {
                        tempNode = tempNode.NextSibling;
                    }
                    CreateChildItem(tempNode.SelectNodes("dt"), itm, true);
                }                
            }
            parentMenu.Add("children", childItms);
        }
        #endregion 标签导入

        #region 读取标签至界面
        public static ToolStripMenuItem[] ReadToDisplay()
        {
            string filePath = Path.Combine(ConfigService.GetRootConfigPath(), BOOK_MARKS_FILE_NAME);
            if (!File.Exists(filePath))
            {
                return Array.Empty<ToolStripMenuItem>();
            }
            string oriJson = File.ReadAllText(filePath);

            JObject rootJson = (JObject)JToken.Parse(oriJson);

            ToolStripMenuItem rootItem = new ToolStripMenuItem();
            if (rootJson.ContainsKey("children"))
            {
                BuildTreeMenu(rootJson?.GetValue("children")?.ToObject<JArray>(), rootItem);
            }
            ToolStripMenuItem[] itms = new ToolStripMenuItem[rootItem.DropDownItems.Count];
            rootItem.DropDownItems.CopyTo(itms, 0);
            return itms;
        }

        private static void BuildTreeMenu(JArray? nodes, ToolStripMenuItem parentMenu, bool isAddNew = false)
        {
            // 有子集就可以添加新的
            if (isAddNew)
            {
                SetAddTSM(parentMenu);
            }

            foreach (JObject node in nodes)
            {
                ToolStripMenuItem? itm = new();
                itm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                if (node.ContainsKey("id"))
                {
                    itm.Name = node.Value<string>("id");
                }
                itm.Text = node?.GetValue("text")?.ToString();
                if (itm.Text!.Length > 15)
                {
                    itm.ToolTipText = itm.Text;
                    itm.Text = itm.Text[..15] + "...";
                }

                if (node!.ContainsKey("icon"))
                {
                    Image img = ImageUtil.ConvertBase64ToImage(node.GetValue("icon")!.ToString());
                    itm.Image = img;
                }
                else
                {
                    itm.Image = Properties.Resources.url_16;
                }
                if (node.ContainsKey("href"))
                {
                    itm.Tag = node.GetValue("href")!.ToString(); 
                    itm.Click += new EventHandler(SubMenu_Click);
                }
                if (node.ContainsKey("children"))
                {
                    itm.Image = Properties.Resources.folder_16;
                    BuildTreeMenu(node?.GetValue("children")?.ToObject<JArray>(), itm, true);
                }
                parentMenu.DropDownItems.Add(itm);
            }
        }

        #endregion 读取标签至界面

        #region 添加修改删除标签
        public static bool AddNewBookMark(string parentId, string url, string title, Image ico)
        {
            if (string.IsNullOrEmpty(parentId) || string.IsNullOrEmpty(url) || string.IsNullOrEmpty(title))
            {
                return false;
            }
            string filePath = Path.Combine(ConfigService.GetRootConfigPath(), BOOK_MARKS_FILE_NAME);
            using StreamReader file = File.OpenText(filePath);
            string info = file.ReadToEnd();
            file.Close();
            JObject bookMarks = (JObject)JToken.Parse(info);
            JObject? parentNode = FindBookMark(parentId, bookMarks.Value<JArray>("children"));
            JObject currNode = new();
            currNode.Add("id",UUIDUtil.NewUUID);
            currNode.Add("text", title);
            if (ico != null)
            {
                currNode.Add("icon", ImageUtil.ConvertImageToBase64(ico));
            }
            currNode.Add("href", url);
            long timeStamp = DateUtil.GetTimeStamp();
            currNode.Add("add_date", timeStamp);
            currNode.Add("last_modified", timeStamp);
            if (parentNode == null) 
            {
                return false;
            }
            JArray? jArray;
            if(parentNode.ContainsKey("children"))
            {
                jArray = parentNode.Value<JArray>("children");
            }
            else
            {
                jArray = new JArray();
                parentNode.Add("children", jArray);
            }
            jArray?.Add(currNode);

            File.WriteAllText(filePath,bookMarks.ToString(),Encoding.UTF8);

            return true;
        }

        public static bool ModifyBookMark(string id, Image ico)
        {
            if (string.IsNullOrEmpty(id) || ico == null)
            {
                return false;
            }
            string filePath = Path.Combine(ConfigService.GetRootConfigPath(), BOOK_MARKS_FILE_NAME);
            using StreamReader file = File.OpenText(filePath);
            string info = file.ReadToEnd();
            file.Close();
            JObject bookMarks = (JObject)JToken.Parse(info);
            JObject? node = FindBookMark(id, bookMarks.Value<JArray>("children"));
            if(node != null){
                if (node.ContainsKey("icon"))
                {
                    node["icon"] = ImageUtil.ConvertImageToBase64(ico);
                }
                else
                {
                    node.Add("icon", ImageUtil.ConvertImageToBase64(ico));
                }
             }
            
            File.WriteAllText(filePath, bookMarks.ToString(), Encoding.UTF8);

            return true;
        }
        #endregion 添加修改删除标签

        private static JObject? FindBookMark(string? id,JArray? bms)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            foreach (JObject itm in bms)
            {
                if(itm.ContainsKey("id") && itm.Value<string>("id") == id)
                {
                    return itm;
                }
                if(itm.ContainsKey("children"))
                {
                    JObject? ret = FindBookMark(itm.Value<string>("id"), itm.Value<JArray>("children"));
                    if(ret != null)
                    {
                        return ret;
                    }
                }
            }
            return null;
        }

        private static void BuildTreeMenu(HtmlNodeCollection nodes, ToolStripMenuItem parentMenu,bool isAddNew = false)
        {
            // 有子集就可以添加新的
            if (isAddNew)
            {
                SetAddTSM(parentMenu);
            }

            for (int i=0;i<nodes.Count;i++)
            {
                HtmlNode dtNode = nodes[i].FirstChild;
                ToolStripMenuItem itm = new();
                itm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                itm.Text = dtNode.InnerText;

                if (dtNode.InnerText.Length > 15)
                {
                    itm.Text = dtNode.InnerText.Substring(0, 15) + "...";
                    itm.ToolTipText = dtNode.InnerText;
                }
                
                if (dtNode.Name.ToLower() == "h3")// 文件夹
                {
                    
                    itm.Image = Properties.Resources.folder_16;
                    HtmlNode tempNode = nodes[i].NextSibling;
                    while (tempNode.Name.ToLower() != "dl")
                    {
                        tempNode = tempNode.NextSibling;
                    }
                    BuildTreeMenu(tempNode.SelectNodes("dt"), itm,true);
                }
                else
                {
                    if(dtNode.GetAttributeValue("icon", "") == "")
                    {
                        itm.Image = Properties.Resources.url_16;
                    }
                    else
                    {
                        Image img = ImageUtil.ConvertBase64ToImage(dtNode.GetAttributeValue("icon", ""));
                        itm.Image = img;
                    }
                    if (dtNode.GetAttributeValue("href", "") != "")
                    {
                        itm.Tag = dtNode.GetAttributeValue("href", "");
                        itm.Click += new EventHandler(SubMenu_Click);
                    }
                    
                }
                
                parentMenu.DropDownItems.Add(itm);
            }
        }

        private static void SetAddTSM(ToolStripMenuItem parentMenu)
        {
            if (parentMenu.DropDownItems.ContainsKey("AddNewUrl"))
            {
                return;
            }
            //List<string> parentIds = new List<string>();
            //parentId = parentMenu.Name;
            //while (parentId != "root")
            //{

            //}
            ToolStripMenuItem addTsm = new();
            addTsm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            addTsm.Text = "添加到本收藏夹...";
            addTsm.Name = "AddNewUrl_" + parentMenu.Name;
            addTsm.Tag = parentMenu.Name;
            addTsm.Click += new EventHandler(SubMenu_Click);
            parentMenu.DropDownItems.Insert(0, addTsm);
            ToolStripSeparator addTss = new();
            parentMenu.DropDownItems.Insert(1, addTss);
        }


        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SubMenu_Click(object? sender, EventArgs e)
        {
            if(sender == null)
            {
                return;
            }
            ToolStripMenuItem? toolStripMenuItem = (ToolStripMenuItem)sender;
            string? idOrUrl = toolStripMenuItem.Tag.ToString();
            if (toolStripMenuItem.Name == "AddNewUrl")
            {
                SubMenuClick?.Invoke(toolStripMenuItem.OwnerItem, new BookMarkSubMenuClickedEventArgs(BookMarkType.AddUrl, idOrUrl!,"", null));
            }
            else
            {
                SubMenuClick?.Invoke(toolStripMenuItem, new BookMarkSubMenuClickedEventArgs(BookMarkType.Url, toolStripMenuItem.Name, idOrUrl, toolStripMenuItem.Image));
            }
        }

    }
}
