using CefSharp.WinForms;
using CefSharp;
using HappyBrowser.Config;
using HappyBrowser.Controls;
using HappyBrowser.Controls.CtlEventArgs;
using HappyBrowser.Handler;
using System.IO;
using HappyBrowser.Controls.BrowserTabStrip;
using Gma.System.MouseKeyHook;
using HappyBrowser.Util;
using HappyBrowser.Entity;
using HappyBrowser.SubForm;
using System.Diagnostics;
using HappyBrowser.Services;

namespace HappyBrowser
{
    public partial class FrmMain : Form
    {
        private readonly Dictionary<int, DownTaskEntity> downTasks = new Dictionary<int, DownTaskEntity>();
        private IKeyboardMouseEvents? globalHook;

        private FindWebContent? findWeb;

        /// <summary>
        /// 当前浏览器
        /// </summary>
        public CtlChromiumBrowser? CurrentBrowser;

        public FrmMain()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void InitBrowser()
        {
            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSettings settings = new();

            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = BrowserConfig.InternalURL,
                SchemeHandlerFactory = new SchemeHandlerFactory()
            });

            settings.UserAgent = BrowserConfig.UserAgent;
            settings.AcceptLanguageList = BrowserConfig.AcceptLanguage;

            settings.IgnoreCertificateErrors = true;

            settings.CachePath = GetAppDir("Cache");

            if (BrowserConfig.Proxy)
            {
                CefSharpSettings.Proxy = new ProxyOptions(BrowserConfig.ProxyIP,
                    BrowserConfig.ProxyPort.ToString(), BrowserConfig.ProxyUsername,
                    BrowserConfig.ProxyPassword, BrowserConfig.ProxyBypassList);
            }

            Cef.Initialize(settings);

        }

        private void BrowserMain_Load(object sender, EventArgs e)
        {
            this.globalHook = Hook.GlobalEvents();
            globalHook.MouseDragFinished += GlobalHook_MouseDragFinished;
            globalHook.KeyDown +=GlobalHook_KeyDown;

            List<HistoryUrl> hisUrls = ConfigService.BrowsingHis.ReadAll();
            foreach (HistoryUrl hisUrl in hisUrls)
            {
                this.AddNewTabPage(hisUrl.Url!, false, hisUrl.Key, null, "", hisUrl.WorkGroup, hisUrl.Locked);
            }
            if (this.browserTabStrip.Items.Count<=1)
            {
                AddNewTabPage(BrowserConfig.NewTabURL);
            }
        }

        #region 全局鼠标键盘事件
        private void GlobalHook_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                BrowserTabStripItem? selectedItem = this.browserTabStrip.SelectedItem;
                if (selectedItem == null || selectedItem.Browser == null) return;
                CwbMain_BrowserRightKeyEvent(selectedItem.Browser, new(RightKeyType.FindWindow));

            }
        }

        private void GlobalHook_MouseDragFinished(object? sender, MouseEventArgs e)
        {
            DragDataEntity? curr = DragDataService.Get(this.Name);
            if (curr == null || curr.StartPosition == Point.Empty) return;
            Point curPoint = Cursor.Position;
            Point oldPoint = curr.StartPosition;
            int? w = curPoint.X - oldPoint.X;
            int? h = curPoint.Y - oldPoint.Y;
            if (w>1 && h>1)// 向右下拖动
            {
                this.Invoke(new Action(() =>
                {
                    if (curr!.IsLink)
                    {
                        this.AddNewTabPage(curr.Link, false, "", null, "", "", false, true);
                    }
                    else if (curr.IsText)
                    {
                        this.ctlHeader.ToSearchText(curr.Text);
                    }
                }));
            }
        }

        #endregion 全局鼠标键盘事件

        private static string GetAppDir(string name)
        {
            string appPath = Application.StartupPath;
            string newPath = Path.Combine(appPath, name);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            return newPath;

        }

        #region Header相关事件与方法
        private void Header_UrlChanged(object sender, HeaderUrlChangedAgrs e)
        {
            if (string.IsNullOrEmpty(e.Url))
            {
                return;
            }
            if (e.NewTab == EnumNewTab.New)
            {
                AddNewTabPage(e.Url, true, "", e.Ico, e.BookMarkId!);
            }
            else
            {
                OpenInCurrentTab(e.Url, e.Ico, e.BookMarkId!);
            }
        }

        private void CtlHeader_ForwardOrBackClick(object sender, HeaderForwardOrBackClickAgrs e)
        {
            if (this.CurrentBrowser != null)
            {
                if (e.ForwardOrBack == EnumBrowserAction.Forward)
                {
                    this.CurrentBrowser.Forward();
                }
                else if (e.ForwardOrBack == EnumBrowserAction.Back)
                {
                    this.CurrentBrowser.Back();
                }
                else if (e.ForwardOrBack == EnumBrowserAction.Refresh)
                {
                    this.CurrentBrowser.Reload();
                }
            }
        }

        private void CtlHeader_SearchChanged(object sender, HeaderSearchChangedAgrs e)
        {
            AddNewTabPage(e.Url, true);
        }
        #endregion Header相关事件与方法

        #region tab和浏览器核心
        #region 添加tabpage

        /// <summary>
        /// 添加一个新tab浏览
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isActive"></param>
        /// <param name="hisTabKey">不为空时，表示这是打开历史记录</param>
        /// <param name="tabImage">标签上的图片</param>
        /// <param name="isSide">是否在当前窗口的右边打开</param>
        public void AddNewTabPage(string url,
            bool isActive = true,
            string hisTabKey = "",
            Image? tabImage = null,
            string bookMarkId = "",
            string workGroup = "",
            bool locked = false,
            bool isSide = false)
        {
            if (string.IsNullOrEmpty(hisTabKey))
            {
                hisTabKey = UUIDUtil.NewUUID;
            }

            if (tabImage == null)
            {
                tabImage = Properties.Resources.url_16;
            }
            if (string.IsNullOrEmpty(workGroup))
            {
                workGroup = this.browserTabStrip.WorkGroup;
            }
            BrowserTabStripItem tabPage = new();
            tabPage.Locked = locked;
            tabPage.WorkGroup = workGroup;

            GetNewTabPage(tabPage, url, hisTabKey, tabImage, bookMarkId);

            if (isSide && this.browserTabStrip.SelectedItem != null)
            {
                int idx = this.browserTabStrip.Items.IndexOf(this.browserTabStrip.SelectedItem);
                if (idx>=0)
                {
                    this.browserTabStrip.Items.Insert(idx+1, tabPage);
                }
            }
            else
            {
                this.browserTabStrip.Items.Add(tabPage);
            }

            ConfigService.BrowsingHis.WriteHis(tabPage.Name, url, tabPage.WorkGroup, tabPage.Locked);

            if (isActive)
            {
                this.browserTabStrip.SelectedItem = tabPage;
            }

        }

        private void GetNewTabPage(BrowserTabStripItem tabPage, string url, string hisTabKey = "", Image? tabImage = null, string bookMarkId = "")
        {
            tabPage.Name = hisTabKey;
            tabPage.Title="加载中";

            if (tabImage != null)
            {
                tabPage.Image=tabImage;
            }

            #region 创建broswer控件
            CtlChromiumBrowser cwbMain;
            if (string.IsNullOrEmpty(url))
            {
                cwbMain = new CtlChromiumBrowser();
            }
            else
            {
                cwbMain = new CtlChromiumBrowser(url);
            }
            ConfigureBrowser(cwbMain);
            cwbMain.Name = "web" + hisTabKey;
            cwbMain.ActivateBrowserOnCreation=true;
            cwbMain.Dock=DockStyle.Fill;
            cwbMain.Location=new Point(0, 0);
            cwbMain.Size=new Size(965, 783);
            cwbMain.TabIndex=0;

            cwbMain.NewWindow+=CwbMain_StartNewWindow;
            cwbMain.AddressChanged+=CwbMain_AddressChanged;
            cwbMain.TitleChanged+=CwbMain_TitleChanged;
            cwbMain.FrameLoadEnd+=CwbMain_FrameLoadEnd;
            cwbMain.DragEnd+=CwbMain_DragEnd;
            cwbMain.LoadingStateChanged+=CwbMain_LoadingStateChanged;
            cwbMain.DownloadUrlChanged+=CwbMain_DownloadUrlChanged;
            cwbMain.FaviconChanged+=CwbMain_FaviconChanged;
            cwbMain.BrowserRightKeyEvent+=CwbMain_BrowserRightKeyEvent;

            BrowserTabEntity tabEntity = new()
            {
                IsOpen = false,
                OrigURL = url,
                CurURL = url,
                DateCreated=DateTime.Now,
                Ico=tabImage,
                Title="",
                Browser = cwbMain,
                Tab = tabPage
            };
            if (!string.IsNullOrEmpty(bookMarkId))
            {
                tabEntity.IsBookMarkOpen = true;
                tabEntity.BookMarkId = bookMarkId;
            }

            tabPage.Tag = tabEntity;

            tabPage.Controls.Add(cwbMain);
            #endregion 创建broswer控件
        }

        public void OpenInCurrentTab(string url, Image? tabImage = null, string bookMarkId = "")
        {
            if (this.browserTabStrip.Items.Count==0)
            {
                AddNewTabPage(url, false);
                return;
            }
            if (this.browserTabStrip.SelectedItem == null)
            {
                return;
            }
            BrowserTabStripItem tabPage = this.browserTabStrip.SelectedItem;
            BrowserTabEntity tabEntity = (BrowserTabEntity)tabPage.Tag;
            tabEntity.OrigURL = url;
            tabEntity.CurURL = url;
            tabEntity.Ico=tabImage;

            if (string.IsNullOrEmpty(bookMarkId))
            {
                tabEntity.IsBookMarkOpen = false;
                tabEntity.BookMarkId = "";
            }
            else
            {
                tabEntity.IsBookMarkOpen = true;
                tabEntity.BookMarkId = bookMarkId;
            }
            if (tabImage != null)
            {
                tabPage.Image = tabImage;
            }
            this.GetBrowser(tabPage)?.LoadUrl(url);

            ConfigService.BrowsingHis.WriteHis(tabPage.Name, url, tabPage.WorkGroup, tabPage.Locked);
        }


        #endregion 添加tabpage

        private BrowserTabStripItem? GetTabPage(CtlChromiumBrowser browser)
        {
            if (browser.Parent != null && browser.Parent is BrowserTabStripItem)
            {
                return (BrowserTabStripItem)browser.Parent;
            }
            return null;
        }

        private CtlChromiumBrowser? GetBrowser(BrowserTabStripItem tabPage)
        {
            if (tabPage == null ||tabPage.Browser == null)
            {
                return null;
            }
            return tabPage.Browser;
        }

        private void ConfigureBrowser(CtlChromiumBrowser browser)
        {

            BrowserSettings config = new BrowserSettings();

            //config.FileAccessFromFileUrls = (!CrossDomainSecurity).ToCefState();
            //config.UniversalAccessFromFileUrls = (!CrossDomainSecurity).ToCefState();
            //config.WebSecurity = WebSecurity.ToCefState();
            config.WebGl = BrowserConfig.WebGL.ToCefState();
            //config.ApplicationCache = ApplicationCache.ToCefState();

            browser.BrowserSettings = config;

        }

        private bool IsCurrentBrowser(object? sender)
        {
            if (this.CurrentBrowser ==null || sender == null) return false;
            if (sender is CtlChromiumBrowser browser)
            {
                return this.CurrentBrowser == browser;
            }
            return false;
        }

        #region Tab事件
        private void BrowserTabStrip_AddTabClicked(object sender, EventArgs e)
        {
            AddNewTabPage(BrowserConfig.NewTabURL);
        }

        private void BrowserTabStrip_TabStripItemSelectionChanged(TabStripItemChangedEventArgs e)
        {
            CtlChromiumBrowser? browser = null;

            if (e!=null && e.Item.Controls.Count>0 && e.Item.Controls[0] is CtlChromiumBrowser)
            {
                browser = ((CtlChromiumBrowser)e.Item.Controls[0]);
            }

            if (e!.ChangeType == BrowserTabStripItemChangeTypes.SelectionChanged)
            {
                if (browser != null)
                {
                    this.CurrentBrowser = browser;
                    this.ctlHeader.SetUrl(browser.Address);
                    this.ctlHeader.SetStatus(new BrowserStateChangedEventArgs(browser));
                }
            }
        }

        private void BrowserTabStrip_TabStripItemClosed(object sender, BrowserTabStripItemClosedEventArgs e)
        {
            if (e!=null && e.Item != null)
            {
                ConfigService.BrowsingHis.RemoveKey(e.Item.Name);
                CtlChromiumBrowser? ccb = GetBrowser(e.Item);
                if (ccb != null && ccb.Address != BrowserConfig.NewTabURL)
                {
                    ClosedUrlService.Add(ccb.Address, ccb.Title, ccb.FaviconIco);
                }
            }

            if (this.browserTabStrip.Items.Count<=1)
            {
                AddNewTabPage(BrowserConfig.NewTabURL);
            }
        }
        #endregion Tab事件

        #region 浏览器事件

        private void CwbMain_BrowserRightKeyEvent(object? sender, BrowserRightKeyEventAgrs e)
        {
            if (sender == null) return;
            CtlChromiumBrowser browser = (CtlChromiumBrowser)sender!;
            InvokeIfNeeded(() =>
            {
                if (e.RightKeyType == RightKeyType.FindWindow && findWeb == null)
                {
                    findWeb = new FindWebContent(browser);
                    findWeb.FormClosed += (object? sender, FormClosedEventArgs e) =>
                    {
                        findWeb = null;
                    };
                    findWeb.Location = new Point(this.ctlHeader.Right-findWeb.Width, this.ctlHeader.Bottom + 30);

                    findWeb.ShowDialog(this.browserTabStrip);
                }
            });

        }

        private void CwbMain_FaviconChanged(object? sender, FaviconChangedEventArgs e)
        {
            if (e == null || e.Browser == null || e.Favicon == null) return;
            InvokeIfNeeded(() =>
            {
                BrowserTabStripItem? tabPage = GetTabPage(e.Browser);
                if (tabPage != null)
                {
                    tabPage.Image = e.Favicon;
                    if (tabPage.Tag != null && tabPage.Tag is BrowserTabEntity)
                    {
                        BrowserTabEntity tabEntity = (BrowserTabEntity)tabPage.Tag;
                        if (tabEntity.IsBookMarkOpen && string.IsNullOrEmpty(tabEntity.BookMarkId) == false)
                        {
                            BookMarksService.ModifyBookMark(tabEntity.BookMarkId, e.Favicon);
                        }
                    }
                }

            });
        }

        private void CwbMain_DownloadUrlChanged(object? sender, DownloadChangedEventArgs e)
        {
            #region 下载处理
            int taskId = e.Id;

            InvokeIfNeeded(new Action(() =>
            {
                if (e.DownStatus == EnumDownStatus.Before)
                {
                    DownloadTask downloadTask = new DownloadTask(e.Url, e.FileName!);

                    DialogResult dialog = downloadTask.ShowDialog(this);

                    DownTaskEntity downTask = new()
                    {
                        Id = taskId,
                        WebBrowser = (CtlChromiumBrowser)sender!
                    };

                    if (dialog == DialogResult.OK)
                    {
                        string savePath = downloadTask.SavePath;
                        string fileName = downloadTask.FileName;
                        if (!Directory.Exists(savePath))
                        {
                            Directory.CreateDirectory(savePath);
                        }

                        string filePath = Path.Combine(savePath, fileName);
                        LogUtil.Info($"下载文件路径:{filePath}");
                        downTask.DownStatus= EnumDownStatus.InProgress;
                        e.BeforeDownloadCallback?.Continue(filePath, showDialog: false);
                    }
                    else
                    {
                        downTask.DownStatus= EnumDownStatus.Cancel;
                    }
                    if (this.downTasks.ContainsKey(taskId))
                    {
                        this.downTasks[taskId] = downTask;
                    }
                    else
                    {
                        this.downTasks.Add(taskId, downTask);
                    }
                }
                else if (e.DownStatus == EnumDownStatus.InProgress)
                {
                    if (this.downTasks.ContainsKey(taskId) && this.downTasks[taskId].DownStatus == EnumDownStatus.Cancel)
                    {
                        e.DownloadItemCallback?.Cancel();
                    }
                }
                else if (e.DownStatus == EnumDownStatus.Complete)
                {
                    if (this.downTasks.ContainsKey(taskId))
                    {
                        this.downTasks[taskId].WebBrowser.Stop();
                        this.downTasks[taskId].WebBrowser?.Destroy();
                        this.downTasks.Remove(taskId);
                    }
                }
                else if (e.DownStatus == EnumDownStatus.Cancel || e.DownStatus == EnumDownStatus.Error)
                {
                    if (this.downTasks.ContainsKey(taskId))
                    {
                        this.downTasks[taskId].WebBrowser.Stop();
                        this.downTasks[taskId].WebBrowser?.Destroy();
                        this.downTasks.Remove(taskId);
                    }
                }
                Debug.WriteLine(e.DownStatus.ToString() + "=" + e.PercentComplete + "%");
            }));
            #endregion 下载处理
        }

        private void CwbMain_LoadingStateChanged(object? sender, LoadingStateChangedEventArgs e)
        {
            if (this.IsCurrentBrowser(sender))
            {
                this.ctlHeader.SetStatus(new BrowserStateChangedEventArgs(e));
            }
        }

        private void CwbMain_DragEnd(object? sender, DragMouseEventArgs e)
        {
            if (e.DragDirection == EnumDragDirection.RightDown)
            {
                if (e.DragData!.IsLink)
                {
                    this.AddNewTabPage(e.DragData.Link, false, "", null, "", "", false, true);
                }
                else if (e.DragData.IsText)
                {
                    this.ctlHeader.ToSearchText(e.DragData.Text);
                }
            }
        }

        private void CwbMain_AddressChanged(object? sender, AddressChangedEventArgs e)
        {
            if (this.IsCurrentBrowser(sender))
            {
                this.ctlHeader.SetUrl(e.Address);
            }
        }

        private void CwbMain_TitleChanged(object? sender, TitleChangedEventArgs e)
        {
            InvokeIfNeeded(() =>
            {
                CtlChromiumBrowser browser = (CtlChromiumBrowser)sender!;
                BrowserTabStripItem? tabPage = GetTabPage(browser);
                if (tabPage != null)
                {
                    tabPage.Title = e.Title;
                }

                if (this.IsCurrentBrowser(sender))
                {
                    this.Text = e.Title;
                    if (browser.Address == BrowserConfig.NewTabURL)
                    {
                        this.ctlHeader.SetSelectUrl();
                    }
                }

            });


        }

        private void CwbMain_StartNewWindow(object? sender, NewWindowEventArgs e)
        {
            this.AddNewTabPage(e.url, false, "", null, "", "", false, true);
        }

        private void CwbMain_FrameLoadEnd(object? sender, CefSharp.FrameLoadEndEventArgs e)
        {
        }
        #endregion 浏览器事件

        #endregion tab和浏览器核心

        private void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }


    }
}
