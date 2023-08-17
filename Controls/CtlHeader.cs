using HappyBrowser.Controls.CtlEventArgs;
using HappyBrowser.Properties;
using HappyBrowser.Services;
using HappyBrowser.Util;
using System.IO;

namespace HappyBrowser.Controls
{
    public partial class CtlHeader : UserControl
    {

        public event EventHandler<HeaderForwardOrBackClickAgrs>? ForwardOrBackClick;
        public event EventHandler<HeaderUrlChangedAgrs>? UrlChanged;
        public event EventHandler<HeaderSearchChangedAgrs>? SearchChanged;

        public event EventHandler? OpenDownloadWindow;

        public CtlHeader()
        {
            InitializeComponent();

            icbSearchIco.Items.Clear();
            icbSearchIco.Items.Add(new CtlComboBoxItem(Resources.google_32, "谷歌", "google"));
            icbSearchIco.Items.Add(new CtlComboBoxItem(Resources.badu_32, "百度", "baidu"));
            icbSearchIco.SelectedIndex=0;
        }

        private void CtlHeader_Load(object sender, EventArgs e)
        {
            this.txtAddress.AutoSize = true;
            this.txtAddress.Height = 28;

            #region 收藏夹相关
            BookMarksService.SubMenuClick += (object? sender, BookMarkSubMenuClickedEventArgs args) =>
            {
                if (args.BookMarkType == BookMarkType.Url)
                {
                    this.txtAddress.Text = args.Url!;
                    UrlChanged?.Invoke(sender, new HeaderUrlChangedAgrs(args.Url, args.Ico, EnumNewTab.New, args.Id));
                }
                else if (args.BookMarkType == BookMarkType.AddUrl)
                {

                    //bool ret = BookMarksService.AddNewBookMark(args.Url,）
                }
            };
            BookMarksService.BookMarksChanged += (object? sender, EventArgs e) =>
            {
                ToolStripMenuItem[] itms = BookMarksService.ReadToDisplay();
                this.msFavoritesContainer.Items.Clear();
                this.msFavoritesContainer.Items.AddRange(itms);
            };
            ToolStripMenuItem[] itms = BookMarksService.ReadToDisplay();
            this.msFavoritesContainer.Items.AddRange(itms);
            #endregion 收藏夹相关
        }

        #region 接收浏览器相关信息
        public void SetUrl(string url)
        {
            InvokeIfNeeded(() =>
            {
                this.txtAddress.Text = URLUtils.DecodeURL(url);
            });
        }

        public void SetStatus(BrowserStateChangedEventArgs args)
        {
            InvokeIfNeeded(() =>
            {
                if (args.IsLoading)
                {
                    this.tsbBackUrl.Enabled = false;
                    this.tsbGoUrl.Enabled = false;
                }
                else
                {
                    this.tsbBackUrl.Enabled = args.CanGoBack;
                    this.tsbGoUrl.Enabled = args.CanGoForward;
                }
            });
        }

        public void SetSelectUrl()
        {
            InvokeIfNeeded(() =>
            {
                this.txtAddress.SelectAll();
            });
        }
        #endregion 接收浏览器相关信息

        #region 地址操作
        /// <summary>
        /// 收藏地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAddress_OnWebCollectClicked(object sender, EventArgs e)
        {

        }

        private void TxtAddress_OnAccessWebUrl(object sender, EventArgs e)
        {
            UrlChanged?.Invoke(sender, new HeaderUrlChangedAgrs(this.txtAddress.Text, EnumNewTab.Current, ""));
        }

        #endregion 地址操作

        #region 网页前进后退
        private void TsbGoUrl_Click(object sender, EventArgs e)
        {
            ForwardOrBackClick?.Invoke(sender, new HeaderForwardOrBackClickAgrs(EnumBrowserAction.Forward));
        }

        private void TsbBackUrl_Click(object sender, EventArgs e)
        {
            ForwardOrBackClick?.Invoke(sender, new HeaderForwardOrBackClickAgrs(EnumBrowserAction.Back));
        }

        private void TsbRefresh_Click(object sender, EventArgs e)
        {
            ForwardOrBackClick?.Invoke(sender, new HeaderForwardOrBackClickAgrs(EnumBrowserAction.Refresh));
        }
        #endregion 网页前进后退

        #region 搜索相关
        private void TxtSearch_OnGoSearch(object sender, CtlEventArgs.HeaderSearchChangedAgrs e)
        {
            ToSearchText(e.Url);
        }

        public void ToSearchText(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            if (this.icbSearchIco.SelectedItem == null) return;
            string searchUrl = "";
            string val = this.icbSearchIco.SelectedValue;
            if (val == "baidu")
            {
                searchUrl=$"https://www.baidu.com/s?wd={searchText}";
            }
            else if (val == "google")
            {
                searchUrl=$"https://www.google.com/search?q={searchText}";
            }
            if (!string.IsNullOrEmpty(searchUrl))
            {
                SearchChanged?.Invoke(this.txtSearch, new HeaderSearchChangedAgrs(searchUrl));
            }
        }
        #endregion 搜索相关

        #region 浏览器设置

        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            SubForm.FrmAbout about = new();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog(this);
        }

        private void TsbOpenDownList_Click(object sender, EventArgs e)
        {
            OpenDownloadWindow?.Invoke(sender, e);
        }

        private void TsmiBrowserSet_Click(object sender, EventArgs e)
        {
            SubForm.FrmBrowserSet browserSet = new();
            browserSet.StartPosition = FormStartPosition.CenterParent;
            browserSet.ShowDialog(this);
        }

        #endregion 浏览器设置

        #region 标签导入导出
        private void TsmiFavImp_Click(object sender, EventArgs e)
        {
            DialogResult result = this.opdSelectFavFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = this.opdSelectFavFile.FileName;
                if (!File.Exists(filePath))
                {
                    NotifyUtil.Warn("标签文件不存在。");
                    return;
                }
                try
                {
                    BookMarksService.ImportFromFile(filePath);
                    NotifyUtil.Success("导入成功。");
                }
                catch (Exception ex)
                {
                    NotifyUtil.Error("导入失败，请重试！");
                    LogUtil.Error(ex);
                }
            }

        }

        private void TsmiFavExp_Click(object sender, EventArgs e)
        {
            // TODO
            NotifyUtil.Error("开发中。。。");
        }
        #endregion 标签导入导出


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

        #region 访问历史url操作
        private void TsbRevert_Click(object sender, EventArgs e)
        {
            var (url, ico) = ClosedUrlService.GetOne();
            if (string.IsNullOrEmpty(url)) return;

            this.txtAddress.Text = url;
            UrlChanged?.Invoke(sender, new HeaderUrlChangedAgrs(url, ico, EnumNewTab.New, UUIDUtil.NewUUID));
        }

        private void TsbRevert_DoubleClick(object sender, EventArgs e)
        {
            // TODO 打所有历史窗口
        }

        private void TsbRevert_MouseUp(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem[] items = ClosedUrlService.ReadToDisplay((url, ico) =>
            {
                this.txtAddress.Text = url;
                UrlChanged?.Invoke(sender, new HeaderUrlChangedAgrs(url, ico, EnumNewTab.New, UUIDUtil.NewUUID));
            });
            this.cmsClosedUrlHistory.Items.Clear();
            this.cmsClosedUrlHistory.Items.AddRange(items);
            this.cmsClosedUrlHistory.Items.Add(this.toolStripSeparator2);
            this.cmsClosedUrlHistory.Items.Add(this.tsmiCleanUrlHistory);
            this.cmsClosedUrlHistory.Items.Add(this.tsmiViewAllUrlHistory);
            this.cmsClosedUrlHistory.Show(this, Cursor.Position);
        }
        #endregion 访问历史url操作

        private void TsbOpenB_Click(object sender, EventArgs e)
        {
            FrmTest frmTest = new();
            frmTest.StartPosition = FormStartPosition.CenterScreen;
            frmTest.Show(this);
        }
    }
}
