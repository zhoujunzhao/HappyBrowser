using CefSharp;
using CefSharp.WinForms;
using HappyBrowser.Controls.CtlEventArgs;
using HappyBrowser.Entity;
using HappyBrowser.Handler;
using HappyBrowser.Services;
using HtmlAgilityPack;
using System.IO;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HappyBrowser.Controls
{
    public class CtlChromiumBrowser : ChromiumWebBrowser
    {
        #region 私有变量
        public event EventHandler<NewWindowEventArgs>? NewWindow;
        public event EventHandler<DownloadChangedEventArgs>? DownloadUrlChanged;
        public event EventHandler<FaviconChangedEventArgs>? FaviconChanged;

        public event EventHandler<DragMouseEventArgs>? DragEnd;

        public event EventHandler<BrowserRightKeyEventAgrs>? BrowserRightKeyEvent;

        private CefDragHandler cefDragHandler;
        private CefRequestHandler cefRequestHandler;

        private CefLifeSpanHandler cefLifeSpanHandler;
        private CefDownloadHandle cefDownloadHander;
        private CefMenuHandler cefMenuHandler;

        private string title;
        private Image? faviconIco;


        #endregion 私有变量

        #region 初始化
        public CtlChromiumBrowser() : base()
        {
            this.title = "";
            this.cefDragHandler = new();
            this.cefRequestHandler = new();
            this.cefLifeSpanHandler = new();
            this.cefDownloadHander = new();
            this.cefMenuHandler = new();
            Initialize();
        }

        public CtlChromiumBrowser(string url) : base(url)
        {
            this.title = "";
            this.cefDragHandler = new();
            this.cefRequestHandler = new();
            this.cefLifeSpanHandler = new();
            this.cefDownloadHander = new();
            this.cefMenuHandler = new();
            Initialize();
            
        }

        private void Initialize()
        {  
            this.cefLifeSpanHandler.NewWindow += (object? sender, NewWindowEventArgs e) =>
            {
                NewWindow?.Invoke(this, e);
            };

            this.cefDownloadHander.DownloadChanged += (object? sender, DownloadChangedEventArgs args) => {
                DownloadUrlChanged?.Invoke(this, args);
            };

            this.cefMenuHandler.BrowserRightKeyEvent += (object? sender, BrowserRightKeyEventAgrs e) => {
                if (e.RightKeyType == RightKeyType.NewWindow)
                {
                    NewWindow?.Invoke(this, new (null, e.Data!.ToString()!));
                }
                else if (e.RightKeyType == RightKeyType.FindWindow)
                {
                    BrowserRightKeyEvent?.Invoke(this,e);
                }
            };


            this.AllowDrop = true;
            this.LifeSpanHandler = this.cefLifeSpanHandler;
            this.KeyboardHandler = new CefKeyBoardHander();
            this.MenuHandler = this.cefMenuHandler;
            this.DownloadHandler = this.cefDownloadHander;
            this.DragHandler = cefDragHandler;

            this.RequestHandler = this.cefRequestHandler;

            this.FrameLoadEnd += CtlChromiumBrowser_FrameLoadEnd;
            
        }

        #endregion 初始化

        /// <summary>
        /// 返回当前网页标题
        /// </summary>
        public string Title
        {
            get => this.title;
        }

        /// <summary>
        /// 返回网站图标
        /// </summary>
        public Image? FaviconIco
        {
            get => this.faviconIco;
        }

        #region 网页加载完成后的处理
        private void CtlChromiumBrowser_FrameLoadEnd(object? sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                string url = e.Frame.Url;
                Uri uri = new(url);
                string domain = uri.Host;
                string http = uri.Scheme;
                int port = uri.Port;
                Task<JavascriptResponse> taskJs = e.Frame.EvaluateScriptAsync("document.title;");
                taskJs.ContinueWith(task => {
                    if (!task.IsFaulted)
                    {
                        if (task != null && task.Result != null && task.Result.Result != null)
                        {
                            this.title = task.Result.Result.ToString()!;
                        }
                        else
                        {
                            this.title = "";
                        }
                    }
                });

                Task<string> task02 = e.Frame.GetSourceAsync();
                task02.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        string resultStr = t.Result;

                        new Thread(() =>
                        {
                            GetFaviconIco(resultStr, domain, http, port);
                        }).Start();

                        LoginWebInfoEntity loginWebInfo = HtmlAnalysisService.Instance.CheckISLoginWebPage(resultStr);

                        if (loginWebInfo != null && loginWebInfo.IsLogin)
                        {
                            loginWebInfo.Url = url;
                            if (this.cefRequestHandler != null)
                            {
                                this.cefRequestHandler.IsIntercept = true;
                                this.cefRequestHandler.LoginWebInfo = loginWebInfo;
                            }
                            FillLoginInfo(loginWebInfo);
                        }
                    }
                });
            }
        }
        #endregion 网页加载完成后的处理


        public void Destroy()
        {
        }

        #region 填充登录信息

        /// <summary>
        /// 填充登录用户名和密码
        /// </summary>
        /// <param name="loginWebInfo"></param>
        private void FillLoginInfo(LoginWebInfoEntity loginWebInfo)
        {
            var (account, password) = ConfigService.LoginInfoHis.Load(loginWebInfo.Url);
            if (!string.IsNullOrEmpty(account))
            {
                string script="";
                if (!string.IsNullOrEmpty(loginWebInfo.AccountId))
                {
                    script += $"document.getElementById('{loginWebInfo.AccountId}').value='{account}';";
                }
                else if (!string.IsNullOrEmpty(loginWebInfo.AccountName))
                {
                    script += $"let eleAs=document.getElementsByName('{loginWebInfo.AccountName}');";
                    script += "for (let i=0; i<eleAs.length; i++){eleAs[i].value='" + account + "';}";
                }
                if (!string.IsNullOrEmpty(loginWebInfo.passwordId))
                {
                    script += $"document.getElementById('{loginWebInfo.passwordId}').value='{password}';";
                }
                else if (!string.IsNullOrEmpty(loginWebInfo.passwordName))
                {
                    script += $"let elePs=document.getElementsByName('{loginWebInfo.passwordName}');";
                    script += "for (let i=0; i<elePs.length; i++){elePs[i].value='" + password + "';}";
                }
                this.GetBrowser().MainFrame.ExecuteJavaScriptAsync(script);
            }
        }
        #endregion 填充登录信息

        #region 获取网站图标
        private void GetFaviconIco(string source,string domain,string http,int port)
        {
            HtmlDocument doc = new();
            doc.LoadHtml(source);

            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes(".//head//link");
            List<string> links = new();
            if (linkNodes != null)
            {
                foreach (HtmlNode linkNode in linkNodes)
                {
                    if (linkNode.GetAttributeValue("rel", "").ToLower()=="icon" || linkNode.GetAttributeValue("rel", "").ToLower()=="shortcut icon")
                    {
                        string href = linkNode.GetAttributeValue("href", "");
                        if (href.ToLower().EndsWith(".ico"))
                        {
                            if (!href.ToLower().StartsWith("http"))
                            {
                                if (port!=80 && port != 443)
                                {
                                    domain = $"{domain}:{port}";
                                }
                                href =$"{http}://{Path.Combine(domain, href)}"; ;
                            }
                            links.Add(href);
                        }
                    }
                }
            }
            links.Add("http://www.google.com/s2/favicons?domain=" + domain);
            GetFaviconIco(links);
        }

        private void GetFaviconIco(List<string> urls)
        {
            if (urls.Count<=0) return;
            string url = urls[0];
            urls.RemoveAt(0);
            try
            {
                HttpClient httpClient = new();
                Task<Stream> taskIco = httpClient.GetStreamAsync(url);
                taskIco.ContinueWith(task =>
                {
                    if (!task.IsFaulted)
                    {
                        Image ico = new Bitmap(task.Result);
                        if (ico == null && urls.Count>=1)
                        {
                            GetFaviconIco(urls);
                        }
                        else
                        {
                            this.faviconIco = ico;
                            FaviconChanged?.Invoke(this,new FaviconChangedEventArgs(this,this.faviconIco));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                if (urls.Count>=1)
                {
                    GetFaviconIco(urls);
                }
                LogUtil.Error(ex);
            }

        }

        #endregion 获取网站图标
    }
}
