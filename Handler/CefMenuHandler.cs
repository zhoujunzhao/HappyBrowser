using CefSharp;
using CefSharp.WinForms;
using HappyBrowser.Controls;
using HappyBrowser.Controls.CtlEventArgs;
using HappyBrowser.SubForm;
using System.IO;
using System.Net.Http;


namespace HappyBrowser.Handler
{
    public class CefMenuHandler : IContextMenuHandler
    {
        public event EventHandler<BrowserRightKeyEventAgrs>? BrowserRightKeyEvent;

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            CtlChromiumBrowser cwb = (CtlChromiumBrowser)browserControl;
            model.Clear();

            if (string.IsNullOrEmpty(parameters.SelectionText))
            {
                model.AddItem((CefMenuCommand)MenuCommand.Web_Reload, "刷新");
                model.AddItem((CefMenuCommand)MenuCommand.Web_StopLoad, "停止");

                if (cwb.CanGoForward)
                {
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Forward, "前进");
                }
                if (cwb.CanGoBack)
                {
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Back, "后退");
                }
                if (string.IsNullOrEmpty(parameters.SourceUrl))
                {
                    model.AddSeparator();
                    model.AddItem((CefMenuCommand)MenuCommand.Web_SaveAs, "网页另存为...");
                    model.AddItem((CefMenuCommand)MenuCommand.Web_CopyLink, "复制网页地址");
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Favicon, "加入收藏夹...");
                    model.AddSeparator();
                    model.AddItem((CefMenuCommand)MenuCommand.Web_SelectAll, "全选");
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Find, "查找...");
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Print, "打印...");
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Translate, "翻成中文");
                    model.AddItem((CefMenuCommand)MenuCommand.Web_Code, "编码");

                }
                else if (!string.IsNullOrEmpty(parameters.SourceUrl))
                {
                    model.AddSeparator();
                    model.AddItem((CefMenuCommand)MenuCommand.Tab_NewTabOpen, "在新标签打开");
                    model.AddItem((CefMenuCommand)MenuCommand.Tab_CopyLink, "复制链接");
                }

                if (parameters.HasImageContents)
                {
                    model.AddSeparator();
                    model.AddItem((CefMenuCommand)MenuCommand.Img_NewTab, "在新标签打开图片");
                    model.AddItem((CefMenuCommand)MenuCommand.Img_SaveTo, "图片另存为");
                    model.AddItem((CefMenuCommand)MenuCommand.Img_CopyImg, "复制图片");
                    model.AddItem((CefMenuCommand)MenuCommand.Img_CopyLink, "复制图片地址");
                }
            }
            else
            {
                model.AddItem((CefMenuCommand)MenuCommand.Web_CopyText, "复制");
            }

            model.AddSeparator();
            model.AddItem((CefMenuCommand)MenuCommand.Tab_ViewSource, "查看源码");
            model.AddItem((CefMenuCommand)MenuCommand.Tab_CheckWeb, "审查元素");
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            CtlChromiumBrowser cwb = (CtlChromiumBrowser)browserControl;
            switch (commandId)
            {
                case (CefMenuCommand)MenuCommand.Web_CopyText:
                    Clipboard.SetText(parameters.SelectionText);
                    break;

                #region 浏览器页面操作
                case (CefMenuCommand)MenuCommand.Web_Reload:
                    cwb.Reload();
                    break;
                case (CefMenuCommand)MenuCommand.Web_StopLoad:
                    cwb.Stop();
                    break;
                case (CefMenuCommand)MenuCommand.Web_Forward:
                    if (cwb.CanGoForward)  cwb.Forward();
                    break;
                case (CefMenuCommand)MenuCommand.Web_Back:
                    if (cwb.CanGoBack) cwb.Back();
                    break;
                #endregion 浏览器页面操作

                #region 浏览器URL操作
                case (CefMenuCommand)MenuCommand.Web_SaveAs:
                    // TODO
                    break;
                case (CefMenuCommand)MenuCommand.Web_CopyLink:
                    Clipboard.SetText(cwb.Address);
                    break;
                case (CefMenuCommand)MenuCommand.Web_Favicon:
                    // TODO
                    break;
                #endregion 浏览器URL操作

                #region 网页内容操作
                case (CefMenuCommand)MenuCommand.Web_SelectAll:
                    cwb.SelectAll();
                    break;
                case (CefMenuCommand)MenuCommand.Web_Find:
                    BrowserRightKeyEvent?.Invoke(cwb, new(RightKeyType.FindWindow));
                    break;
                case (CefMenuCommand)MenuCommand.Web_Print:
                    cwb.Print();
                    break;
                case (CefMenuCommand)MenuCommand.Web_Translate:
                    // TODO
                    break;
                case (CefMenuCommand)MenuCommand.Web_Code:
                    // TODO
                    break;
                #endregion 网页内容操作

                #region 标签相关操作
                case (CefMenuCommand)MenuCommand.Tab_NewTabOpen:
                    BrowserRightKeyEvent?.Invoke(cwb,new (RightKeyType.NewWindow, parameters.SourceUrl));
                    break;
                case (CefMenuCommand)MenuCommand.Tab_CopyLink:
                    Clipboard.SetText(parameters.SourceUrl);
                    break;
                case (CefMenuCommand)MenuCommand.Tab_ViewSource:
                    cwb.ViewSource();
                    break;
                case (CefMenuCommand)MenuCommand.Tab_CheckWeb:
                    cwb.ShowDevTools();
                    break;
                #endregion 标签相关操作

                #region 图片相关操作
                case (CefMenuCommand)MenuCommand.Img_NewTab:
                    BrowserRightKeyEvent?.Invoke(cwb, new(RightKeyType.NewWindow, parameters.SourceUrl));
                    break;
                case (CefMenuCommand)MenuCommand.Img_SaveTo:
                    
                    break;
                case (CefMenuCommand)MenuCommand.Img_CopyImg:
                    SaveImage(parameters.SourceUrl);
                    break;
                case (CefMenuCommand)MenuCommand.Img_CopyLink:
                    Clipboard.SetText(parameters.SourceUrl);
                    break;


                    #endregion 图片相关操作
            }
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            var webBrowser = (ChromiumWebBrowser)browserControl;
            Action setContextAction = delegate ()
            {
                webBrowser.ContextMenuStrip = null;
            };
            webBrowser.Invoke(setContextAction);
        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }

        private void SaveImage(string imageUrl)
        {
            HttpClient httpClient = new HttpClient();
            
            Task<byte[]> taskBytes = httpClient.GetByteArrayAsync(imageUrl);
            taskBytes.ContinueWith(t => {
                if (!t.IsFaulted)
                {
                    using (var ms = new MemoryStream(t.Result))
                    {
                        Image bitmap = Image.FromStream(ms);
                    }
                    
                }
            });

            Task<Stream> task =  httpClient.GetStreamAsync(imageUrl);
            task.ContinueWith(t => {
                if (!t.IsFaulted)
                {
                    Image bitmap = Image.FromStream(t.Result);
                    Clipboard.SetImage(bitmap);
                    bitmap.Dispose();
                }
            });
            //System.Net.WebClient client = new WebClient();
            //System.IO.Stream stream = client.OpenRead(imageUrl);
            //Bitmap bitmap = new Bitmap(stream);
            //Clipboard.SetImage(bitmap);

            //if (bitmap != null)
            //{
            //    bitmap.Save(filename, format);
            //}

            //stream.Flush();
            //stream.Close();
            //client.Dispose();
        }


        private enum MenuCommand
        {
            Web_CopyText = 26500,

            Web_Reload = 26501,
            Web_StopLoad = 26502,
            Web_Forward = 26503,
            Web_Back = 26504,

            Web_SaveAs = 26505,
            Web_CopyLink=26506,
            Web_Favicon = 26507,

            Web_SelectAll = 26508,
            Web_Find = 26509,
            Web_Print = 26510,
            Web_Translate = 26511,
            Web_Code = 26512,


            Tab_NewTabOpen = 26531,
            Tab_CopyLink = 26532,
            Tab_ViewSource = 26533,
            Tab_CheckWeb = 26534,

            Img_NewTab = 26551,
            Img_SaveTo = 26552,
            Img_CopyImg = 26553,
            Img_CopyLink = 26554,


            NotFound = -1,
            
            ReloadNoCache = 103,
            
            Undo = 110,
            Redo = 111,
            Cut = 112,
            Copy = 113,
            Paste = 114,
            Delete = 115,
            SelectAll = 116,
            Find = 130,
            Print = 131,
            
            SpellCheckSuggestion0 = 200,
            SpellCheckSuggestion1 = 201,
            SpellCheckSuggestion2 = 202,
            SpellCheckSuggestion3 = 203,
            SpellCheckSuggestion4 = 204,
            SpellCheckLastSuggestion = 204,
            SpellCheckNoSuggestions = 205,
            AddToDictionary = 206,
            CustomFirst = 220,
            CustomLast = 250,
            UserFirst = 26500,
            UserLast = 28500
        }

    }
}
