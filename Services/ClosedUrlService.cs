using System.IO;
using HappyBrowser.Controls.CtlEventArgs;
using Newtonsoft.Json.Linq;
using HappyBrowser.Util;
using System.Text;

namespace HappyBrowser.Services
{
    public class ClosedUrlService
    {
        public const string FILE_NAME = "closedUrl.json";
        public static event EventHandler? SubMenuClick;

        public static void Add(string url, string title, Image? ico)
        {
            if (string.IsNullOrEmpty(url)) return;
            if (string.IsNullOrEmpty(title)) return;
            JObject closedUrl = new()
            {
                { "url", url },
                { "title", title },
                { "closeTime", DateTime.Now }
            };
            if (ico != null)
            {
                closedUrl.Add("ico", ImageUtil.ConvertImageToBase64(ico));
            }
            JArray jArray = GetAll();

            jArray.AddFirst(closedUrl);
            Write(jArray);
        }

        /// <summary>
        /// 获取一个关闭历史并且移除
        /// </summary>
        /// <returns></returns>
        public static (string url, Image? ico) GetOne()
        {
            JArray jArray = GetAll();
            if (jArray == null || jArray.Count == 0)
            {
                return ("",null);
            }
            JObject obj = (JObject)jArray[0];
            jArray.Remove(0);
            Write(jArray);

            Image image;
            if (obj.ContainsKey("ico") && !string.IsNullOrEmpty(obj.Value<string>("ico")))
            {
                image = ImageUtil.ConvertBase64ToImage(obj.Value<string>("ico")!);
            }
            else
            {
                image = Properties.Resources.url_16;
            }

            string url = obj.ContainsKey("url") ? obj.Value<string>("url")! : "";

            return (url,image);
        }

        public static ClosedUrl? Remove(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            JArray jArray = GetAll();
            JObject? saveObj=null;
            foreach (JObject obj in jArray)
            {
                string saveUrl = obj.ContainsKey("url") ? obj.Value<string>("url")! : "";
                if (url.Trim().ToLower() == saveUrl.Trim().ToLower())
                {
                    saveObj = obj;
                    jArray.Remove(obj);
                    break;
                }
            }
            if (saveObj != null)
            {
                Write(jArray);
                return saveObj.ToObject<ClosedUrl>();
            }

            return null;
        }

        public static void RemoveAll()
        {
            Write(new JArray());
        }

        private static JArray GetAll()
        {
            string filePath = Path.Combine(ConfigService.GetSyncConfigPath(), FILE_NAME);
            string allHistory;
            if (File.Exists(filePath))
            {
                allHistory = File.ReadAllText(filePath);
                return (JArray)JToken.Parse(allHistory);
            }
            else
            {
                return new JArray();
            }
        }

        private static void Write(JArray jArray)
        {
            string filePath = Path.Combine(ConfigService.GetSyncConfigPath(), FILE_NAME);
            File.WriteAllText(filePath, jArray.ToString(), Encoding.UTF8);
        }

        #region 读取标签至界面
        public static ToolStripMenuItem[] ReadToDisplay( Action<string,Image> action)
        {
            JArray jArray = GetAll();
            if (jArray == null || jArray.Count == 0)
            {
                return Array.Empty<ToolStripMenuItem>();
            }

            List<ClosedUrl> closedUrls = jArray.ToObject<List<ClosedUrl>>()!;

            if (closedUrls == null || closedUrls.Count==0)
            {
                return Array.Empty<ToolStripMenuItem>();
            }

            ToolStripMenuItem itm ;
            List<ToolStripMenuItem> itms = new();

            int top = closedUrls.Count < 20 ? closedUrls.Count : 20;

            for (int i = 0; i < top; i++)
            {
                ClosedUrl closedUrl = closedUrls[i];

                itm = new();

                itm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                itm.Text = closedUrl.Title;
                itm.Tag = closedUrl.Url;
                if (itm.Text!.Length > 15)
                {
                    itm.ToolTipText = itm.Text;
                    itm.Text = itm.Text[..15] + "...";
                }

                if (string.IsNullOrEmpty(closedUrl.Ico))
                {
                    itm.Image = Properties.Resources.url_16;
                }
                else
                {
                    itm.Image = ImageUtil.ConvertBase64ToImage(closedUrl.Ico);
                }

                itm.Click += (object? sender, EventArgs e) => {
                    if (sender == null || sender is not ToolStripMenuItem)
                    {
                        return;
                    }
                    ToolStripMenuItem? toolStripMenuItem = (ToolStripMenuItem)sender;
                    string? url = toolStripMenuItem.Tag.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        action.Invoke(url,toolStripMenuItem.Image);
                        Remove(url);
                    }
                };

                itms.Add(itm);
            }
            return itms.ToArray<ToolStripMenuItem>();
        }

        #endregion 读取标签至界面

    }

    public class ClosedUrl
    {
        public String? Url;
        public String? Title;
        public DateTime? CloseTime;
        public String? Ico;
    }
}
