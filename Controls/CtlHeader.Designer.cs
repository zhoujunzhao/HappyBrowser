
namespace HappyBrowser.Controls
{
    partial class CtlHeader
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components=new System.ComponentModel.Container();
            tsLeftBtnContainer=new ToolStrip();
            tsbOpenB=new ToolStripButton();
            toolStripSeparator1=new ToolStripSeparator();
            tsbBackUrl=new ToolStripButton();
            tsbGoUrl=new ToolStripButton();
            tsbRefresh=new ToolStripButton();
            tsbRevert=new ToolStripButton();
            tsRightBtnContainer=new ToolStrip();
            msFavoritesContainer=new MenuStrip();
            cmsFavOperate=new ContextMenuStrip(components);
            tsmiFavImp=new ToolStripMenuItem();
            tsmiFavExp=new ToolStripMenuItem();
            tsBroserSet=new ToolStrip();
            tsbOpenDownList=new ToolStripButton();
            tsddBroserSet=new ToolStripDropDownButton();
            tsmiBrowserSet=new ToolStripMenuItem();
            toolStripSeparator3=new ToolStripSeparator();
            tsmiAbout=new ToolStripMenuItem();
            txtSearch=new CtlSearchBox();
            icbSearchIco=new CtlComboBox();
            opdSelectFavFile=new OpenFileDialog();
            cmsClosedUrlHistory=new ContextMenuStrip(components);
            toolStripSeparator2=new ToolStripSeparator();
            tsmiCleanUrlHistory=new ToolStripMenuItem();
            tsmiViewAllUrlHistory=new ToolStripMenuItem();
            tlpContainer=new TableLayoutPanel();
            txtAddress=new CtlAddressBox();
            tsLeftBtnContainer.SuspendLayout();
            cmsFavOperate.SuspendLayout();
            tsBroserSet.SuspendLayout();
            cmsClosedUrlHistory.SuspendLayout();
            tlpContainer.SuspendLayout();
            SuspendLayout();
            // 
            // tsLeftBtnContainer
            // 
            tsLeftBtnContainer.Dock=DockStyle.Fill;
            tsLeftBtnContainer.GripMargin=new Padding(0);
            tsLeftBtnContainer.GripStyle=ToolStripGripStyle.Hidden;
            tsLeftBtnContainer.ImageScalingSize=new Size(20, 20);
            tsLeftBtnContainer.Items.AddRange(new ToolStripItem[] { tsbOpenB, toolStripSeparator1, tsbBackUrl, tsbGoUrl, tsbRefresh, tsbRevert });
            tsLeftBtnContainer.Location=new Point(0, 0);
            tsLeftBtnContainer.Name="tsLeftBtnContainer";
            tsLeftBtnContainer.Size=new Size(240, 26);
            tsLeftBtnContainer.Stretch=true;
            tsLeftBtnContainer.TabIndex=0;
            // 
            // tsbOpenB
            // 
            tsbOpenB.AutoSize=false;
            tsbOpenB.DisplayStyle=ToolStripItemDisplayStyle.Text;
            tsbOpenB.ImageTransparentColor=Color.Magenta;
            tsbOpenB.Name="tsbOpenB";
            tsbOpenB.Size=new Size(36, 27);
            tsbOpenB.Text="测试";
            tsbOpenB.Click+=TsbOpenB_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name="toolStripSeparator1";
            toolStripSeparator1.Size=new Size(6, 26);
            // 
            // tsbBackUrl
            // 
            tsbBackUrl.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbBackUrl.Image=Properties.Resources.backUrl_32;
            tsbBackUrl.ImageTransparentColor=Color.Magenta;
            tsbBackUrl.Name="tsbBackUrl";
            tsbBackUrl.Size=new Size(24, 23);
            tsbBackUrl.Text="后退";
            tsbBackUrl.Click+=TsbBackUrl_Click;
            // 
            // tsbGoUrl
            // 
            tsbGoUrl.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbGoUrl.Image=Properties.Resources.goUrl_32;
            tsbGoUrl.ImageTransparentColor=Color.Magenta;
            tsbGoUrl.Name="tsbGoUrl";
            tsbGoUrl.Size=new Size(24, 23);
            tsbGoUrl.Text="前进";
            tsbGoUrl.Click+=TsbGoUrl_Click;
            // 
            // tsbRefresh
            // 
            tsbRefresh.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbRefresh.Image=Properties.Resources.refresh_32;
            tsbRefresh.ImageTransparentColor=Color.Magenta;
            tsbRefresh.Name="tsbRefresh";
            tsbRefresh.Size=new Size(24, 23);
            tsbRefresh.Text="刷新";
            tsbRefresh.Click+=TsbRefresh_Click;
            // 
            // tsbRevert
            // 
            tsbRevert.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbRevert.Image=Properties.Resources.revert_32;
            tsbRevert.ImageTransparentColor=Color.Magenta;
            tsbRevert.Name="tsbRevert";
            tsbRevert.Size=new Size(24, 23);
            tsbRevert.Text="恢复";
            tsbRevert.ToolTipText="右键点击显示历史列表，双击查看所有历史";
            tsbRevert.Click+=TsbRevert_Click;
            tsbRevert.DoubleClick+=TsbRevert_DoubleClick;
            tsbRevert.MouseUp+=TsbRevert_MouseUp;
            // 
            // tsRightBtnContainer
            // 
            tsRightBtnContainer.Dock=DockStyle.Fill;
            tsRightBtnContainer.Location=new Point(740, 0);
            tsRightBtnContainer.Name="tsRightBtnContainer";
            tsRightBtnContainer.Size=new Size(200, 26);
            tsRightBtnContainer.TabIndex=1;
            tsRightBtnContainer.Text="toolStrip1";
            // 
            // msFavoritesContainer
            // 
            msFavoritesContainer.AutoSize=false;
            msFavoritesContainer.BackColor=SystemColors.Control;
            tlpContainer.SetColumnSpan(msFavoritesContainer, 6);
            msFavoritesContainer.ContextMenuStrip=cmsFavOperate;
            msFavoritesContainer.Dock=DockStyle.Fill;
            msFavoritesContainer.GripMargin=new Padding(0);
            msFavoritesContainer.Location=new Point(0, 26);
            msFavoritesContainer.Name="msFavoritesContainer";
            msFavoritesContainer.Padding=new Padding(2, 1, 2, 0);
            msFavoritesContainer.ShowItemToolTips=true;
            msFavoritesContainer.Size=new Size(1000, 30);
            msFavoritesContainer.TabIndex=0;
            // 
            // cmsFavOperate
            // 
            cmsFavOperate.Items.AddRange(new ToolStripItem[] { tsmiFavImp, tsmiFavExp });
            cmsFavOperate.Name="cmsFavOperate";
            cmsFavOperate.Size=new Size(101, 48);
            // 
            // tsmiFavImp
            // 
            tsmiFavImp.Name="tsmiFavImp";
            tsmiFavImp.Size=new Size(100, 22);
            tsmiFavImp.Text="导入";
            tsmiFavImp.Click+=TsmiFavImp_Click;
            // 
            // tsmiFavExp
            // 
            tsmiFavExp.Name="tsmiFavExp";
            tsmiFavExp.Size=new Size(100, 22);
            tsmiFavExp.Text="导出";
            tsmiFavExp.Click+=TsmiFavExp_Click;
            // 
            // tsBroserSet
            // 
            tsBroserSet.Dock=DockStyle.Fill;
            tsBroserSet.GripMargin=new Padding(0);
            tsBroserSet.GripStyle=ToolStripGripStyle.Hidden;
            tsBroserSet.ImageScalingSize=new Size(20, 20);
            tsBroserSet.Items.AddRange(new ToolStripItem[] { tsbOpenDownList, tsddBroserSet });
            tsBroserSet.Location=new Point(940, 0);
            tsBroserSet.Name="tsBroserSet";
            tsBroserSet.Padding=new Padding(0);
            tsBroserSet.Size=new Size(60, 26);
            tsBroserSet.TabIndex=8;
            tsBroserSet.Text="浏览器设置";
            // 
            // tsbOpenDownList
            // 
            tsbOpenDownList.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbOpenDownList.Font=new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tsbOpenDownList.Image=Properties.Resources.downlload_32;
            tsbOpenDownList.ImageTransparentColor=Color.Magenta;
            tsbOpenDownList.Name="tsbOpenDownList";
            tsbOpenDownList.Size=new Size(24, 23);
            tsbOpenDownList.Text="toolStripButton1";
            tsbOpenDownList.ToolTipText="打开下载列表";
            tsbOpenDownList.Click+=TsbOpenDownList_Click;
            // 
            // tsddBroserSet
            // 
            tsddBroserSet.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsddBroserSet.DropDownItems.AddRange(new ToolStripItem[] { tsmiBrowserSet, toolStripSeparator3, tsmiAbout });
            tsddBroserSet.Image=Properties.Resources.browser_set_32;
            tsddBroserSet.ImageTransparentColor=Color.Magenta;
            tsddBroserSet.Name="tsddBroserSet";
            tsddBroserSet.Size=new Size(33, 23);
            tsddBroserSet.Text="设置";
            // 
            // tsmiBrowserSet
            // 
            tsmiBrowserSet.Image=Properties.Resources.browser_set_16;
            tsmiBrowserSet.Name="tsmiBrowserSet";
            tsmiBrowserSet.Size=new Size(136, 22);
            tsmiBrowserSet.Text="浏览器设置";
            tsmiBrowserSet.Click+=TsmiBrowserSet_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name="toolStripSeparator3";
            toolStripSeparator3.Size=new Size(133, 6);
            // 
            // tsmiAbout
            // 
            tsmiAbout.Image=Properties.Resources.browser_logo;
            tsmiAbout.Name="tsmiAbout";
            tsmiAbout.Size=new Size(136, 22);
            tsmiAbout.Text="关于";
            tsmiAbout.Click+=TsmiAbout_Click;
            // 
            // txtSearch
            // 
            txtSearch.Dock=DockStyle.Fill;
            txtSearch.Location=new Point(555, 0);
            txtSearch.Margin=new Padding(0);
            txtSearch.Name="txtSearch";
            txtSearch.Size=new Size(185, 26);
            txtSearch.TabIndex=7;
            txtSearch.OnGoSearch+=TxtSearch_OnGoSearch;
            // 
            // icbSearchIco
            // 
            icbSearchIco.DisplayText=false;
            icbSearchIco.Dock=DockStyle.Fill;
            icbSearchIco.DrawMode=DrawMode.OwnerDrawFixed;
            icbSearchIco.DropDownStyle=ComboBoxStyle.DropDownList;
            icbSearchIco.ImageSize=new Size(18, 18);
            icbSearchIco.ItemHeight=22;
            icbSearchIco.Location=new Point(525, 0);
            icbSearchIco.Margin=new Padding(0);
            icbSearchIco.Name="icbSearchIco";
            icbSearchIco.Size=new Size(30, 28);
            icbSearchIco.TabIndex=6;
            // 
            // opdSelectFavFile
            // 
            opdSelectFavFile.DefaultExt="html";
            opdSelectFavFile.FileName="bookmarks.html";
            opdSelectFavFile.Filter="html文件|*.html|htm文件|*.htm";
            opdSelectFavFile.Title="选择需要导入的标签文件";
            // 
            // cmsClosedUrlHistory
            // 
            cmsClosedUrlHistory.Items.AddRange(new ToolStripItem[] { toolStripSeparator2, tsmiCleanUrlHistory, tsmiViewAllUrlHistory });
            cmsClosedUrlHistory.Name="cmsClosedUrlHistory";
            cmsClosedUrlHistory.Size=new Size(173, 54);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name="toolStripSeparator2";
            toolStripSeparator2.Size=new Size(169, 6);
            // 
            // tsmiCleanUrlHistory
            // 
            tsmiCleanUrlHistory.Name="tsmiCleanUrlHistory";
            tsmiCleanUrlHistory.Size=new Size(172, 22);
            tsmiCleanUrlHistory.Text="清空访问历史";
            // 
            // tsmiViewAllUrlHistory
            // 
            tsmiViewAllUrlHistory.Name="tsmiViewAllUrlHistory";
            tsmiViewAllUrlHistory.Size=new Size(172, 22);
            tsmiViewAllUrlHistory.Text="查看所有访问历史";
            // 
            // tlpContainer
            // 
            tlpContainer.ColumnCount=6;
            tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 185F));
            tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tlpContainer.Controls.Add(tsBroserSet, 5, 0);
            tlpContainer.Controls.Add(tsLeftBtnContainer, 0, 0);
            tlpContainer.Controls.Add(txtSearch, 3, 0);
            tlpContainer.Controls.Add(txtAddress, 1, 0);
            tlpContainer.Controls.Add(msFavoritesContainer, 0, 1);
            tlpContainer.Controls.Add(tsRightBtnContainer, 4, 0);
            tlpContainer.Controls.Add(icbSearchIco, 2, 0);
            tlpContainer.Dock=DockStyle.Fill;
            tlpContainer.Location=new Point(0, 0);
            tlpContainer.Margin=new Padding(0);
            tlpContainer.Name="tlpContainer";
            tlpContainer.RowCount=2;
            tlpContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpContainer.Size=new Size(1000, 56);
            tlpContainer.TabIndex=3;
            // 
            // txtAddress
            // 
            txtAddress.Dock=DockStyle.Fill;
            txtAddress.Location=new Point(240, 0);
            txtAddress.Margin=new Padding(0);
            txtAddress.Name="txtAddress";
            txtAddress.Size=new Size(285, 26);
            txtAddress.TabIndex=1;
            txtAddress.OnAccessWebUrl+=TxtAddress_OnAccessWebUrl;
            txtAddress.OnWebCollectClicked+=TxtAddress_OnWebCollectClicked;
            // 
            // CtlHeader
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            Controls.Add(tlpContainer);
            Margin=new Padding(0);
            Name="CtlHeader";
            Size=new Size(1000, 56);
            Load+=CtlHeader_Load;
            tsLeftBtnContainer.ResumeLayout(false);
            tsLeftBtnContainer.PerformLayout();
            cmsFavOperate.ResumeLayout(false);
            tsBroserSet.ResumeLayout(false);
            tsBroserSet.PerformLayout();
            cmsClosedUrlHistory.ResumeLayout(false);
            tlpContainer.ResumeLayout(false);
            tlpContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ToolStrip tsRightBtnContainer;
        private MenuStrip msFavoritesContainer;
        private ToolStrip tsLeftBtnContainer;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbBackUrl;
        private ToolStripButton tsbGoUrl;
        private CtlSearchBox txtSearch;
        private CtlComboBox icbSearchIco;
        private ToolStripButton tsbRefresh;
        private ToolStrip tsBroserSet;
        private ToolStripButton tsbOpenDownList;
        private ToolStripDropDownButton tsddBroserSet;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem tsmiAbout;
        private ContextMenuStrip cmsFavOperate;
        private ToolStripMenuItem tsmiFavImp;
        private ToolStripMenuItem tsmiFavExp;
        private OpenFileDialog opdSelectFavFile;
        private ToolStripMenuItem tsmiBrowserSet;
        private ToolStripButton tsbRevert;
        private ContextMenuStrip cmsClosedUrlHistory;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiCleanUrlHistory;
        private ToolStripMenuItem tsmiViewAllUrlHistory;
        private ToolStripButton tsbOpenB;
        private TableLayoutPanel tlpContainer;
        private CtlAddressBox txtAddress;
    }
}
