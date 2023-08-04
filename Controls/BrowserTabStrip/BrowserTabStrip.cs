using CefSharp;
using HappyBrowser.Properties;
using HappyBrowser.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace HappyBrowser.Controls.BrowserTabStrip {

	[DefaultEvent("TabStripItemSelectionChanged")]
	[DefaultProperty("Items")]
	[ToolboxItem(true)]
	public class BrowserTabStrip : BaseStyledPanel, ISupportInitialize, IDisposable {

        #region 常量
        
        /// <summary>
        /// 添加按钮的宽度
        /// </summary>
        private const int DEF_ADD_BUTTON_WIDTH = 25;

        /// <summary>
        /// 标签条高度
        /// </summary>
        private const int DEF_TAB_HEADER_HEIGHT = 30;

        /// <summary>
        /// 锁定标签的宽度
        /// </summary>
        private const int DEF_TAB_LOCKER_WIDTH = 26;

        /// <summary>
        /// 标签最大宽度
        /// </summary>
        private const int DEF_MAX_TAB_SIZE = 200;

        /// <summary>
        /// 标签绘制起始位置
        /// </summary>
        private const int DEF_START_POS = 53;

        #endregion 常量

        #region 本地变量
        private int tabStartPosi;

        /// <summary>
        /// 添加按钮区域
        /// </summary>
        private RectangleF addNewTabRect;
        private bool isAddTabMove = false;

        private BrowserTabStripItem? selectedItem;

        private BrowserTabStripItem? mouseClickItem;

        private TabHeaderLocation tabHeaderLocation = TabHeaderLocation.Bottom;

        private ContextMenuStrip? tabContextMenu;
        private ToolStripMenuItem? tabContextMenuItemClose;
        private ToolStripMenuItem? tabContextMenuItemLock;
        private ToolStripMenuItem? tabContextMenuItemGroup;

        private ToolStripMenuItem? tabContextMenuItemWorkGroup;
        private ToolStripMenuItem? tabContextMenuItemReadGroup;

        private BrowserTabStripItemCollection items;

		private ToolTip toolTip;

		private StringFormat sf;

		private static readonly Font defaultFont = new("Microsoft YaHei UI", 9f, FontStyle.Regular, GraphicsUnit.Point);

		private bool isIniting;

		private bool isDisplayCloseButton;

        private CtlComboBox ccbWorkSpace;

        private string currentWorkGroup = "work";


        private bool startDragging = false;
        private BrowserTabStripItem? dragItem;


        #endregion 本地变量

        #region 事件
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ControlCollection Controls => base.Controls;

        public event TabStripItemClosingHandler? TabStripItemClosing;

        public event TabStripItemChangedHandler? TabStripItemSelectionChanged;

        public event EventHandler<BrowserTabStripItemClosedEventArgs>? TabStripItemClosed;

        public event EventHandler? AddTabClicked;
        #endregion 事件

        #region 初始化
        public BrowserTabStrip()
        {
            BeginInit();
            SetStyle(ControlStyles.ContainerControl, value: true);
            SetStyle(ControlStyles.UserPaint, value: true);
            SetStyle(ControlStyles.ResizeRedraw, value: true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
            SetStyle(ControlStyles.Selectable, value: true);
            items = new();
            items.CollectionChanged += OnCollectionChanged;

            toolTip = new();
            isDisplayCloseButton = true;

            ccbWorkSpace = new();
            InitWorkGroupArea();

            tabContextMenu = new ContextMenuStrip();
            InitTabContextMenu(tabContextMenu.Items);

            Font = defaultFont;
            sf = new StringFormat();
            EndInit();
            UpdateLayout();
        }

        #region 工作区设置
        private void InitWorkGroupArea()
        {
            ccbWorkSpace.ImageSize=new Size(18, 18);
            ccbWorkSpace.ItemHeight=18;
            ccbWorkSpace.Location=new Point(0, 200-DEF_TAB_HEADER_HEIGHT);
            ccbWorkSpace.Margin=new Padding(0);
            ccbWorkSpace.BackColor = SystemColors.GradientInactiveCaption;
            ccbWorkSpace.Name="ccbWorkSpace";
            ccbWorkSpace.Size=new Size(DEF_START_POS - 1, 24);

            this.Controls.Add(ccbWorkSpace);
            ccbWorkSpace.Items.Clear();
            ccbWorkSpace.Items.Add(new CtlComboBoxItem(Resources.ws_work_32, "工作", "work"));
            ccbWorkSpace.Items.Add(new CtlComboBoxItem(Resources.ws_readbook_32, "阅读", "read"));
            ccbWorkSpace.SelectedIndex=0;
            ccbWorkSpace.SelectedIndexChanged+=CcbWorkSpace_SelectedIndexChanged;
        }

        private void CcbWorkSpace_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ccbWorkSpace.SelectedItem == null) return;
            string currWs = ccbWorkSpace.SelectedValue;
            BrowserTabStripItem? firstItem = null;

            foreach (BrowserTabStripItem item in Items)
            {
                if (firstItem == null && item.WorkGroup == currWs)
                {
                    firstItem = item;
                }
                item.Visible = item.WorkGroup == currWs;
            }
            this.currentWorkGroup = currWs;
            if (selectedItem!=null && selectedItem.WorkGroup != currWs)
            {
                SelectedItem=firstItem;
            }
            UpdateLayout();
            Invalidate();
            selectedItem?.Focus();
        }

        public string WorkGroup { get { return this.currentWorkGroup; } }
        #endregion 工作区设置

        #region tab右键菜单

        public void InitTabContextMenu(ToolStripItemCollection itms)
        {
            ToolStripMenuItem stripItem;
            stripItem = new();
            stripItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            stripItem.Text = "刷新";
            stripItem.Image = Resources.refresh_32;
            stripItem.Name = "tsmiTabFlash";
            stripItem.Click += (object? sender, EventArgs e) =>
            {
                if (mouseClickItem == null) return;
                if (mouseClickItem.Controls.Count>0)
                {
                    CtlChromiumBrowser browser = (CtlChromiumBrowser)mouseClickItem.Controls[0];
                    browser.Reload();
                }
                mouseClickItem = null;
            };
            itms.Add(stripItem);

            tabContextMenuItemClose = new();
            tabContextMenuItemClose.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tabContextMenuItemClose.Text = "关闭";
            tabContextMenuItemClose.Image = Resources.close_32;
            tabContextMenuItemClose.Name = "tsmiTabClose";
            tabContextMenuItemClose.Click += (object? sender, EventArgs e) =>
            {
                if (mouseClickItem == null) return;
                TabStripItemClosingEventArgs tabStripItemClosingEventArgs = new TabStripItemClosingEventArgs(mouseClickItem);
                this.TabStripItemClosing?.Invoke(tabStripItemClosingEventArgs);

                if (!tabStripItemClosingEventArgs.Cancel && mouseClickItem.CanClose)
                {
                    RemoveTab(mouseClickItem);
                    this.TabStripItemClosed?.Invoke(this, new BrowserTabStripItemClosedEventArgs(mouseClickItem));
                    Invalidate();
                }
                mouseClickItem = null;
            };
            itms.Add(tabContextMenuItemClose);

            tabContextMenuItemLock = new();
            tabContextMenuItemLock.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tabContextMenuItemLock.Text = "锁定";
            tabContextMenuItemLock.Name = "tabContextMenuItemLock";
            tabContextMenuItemLock.Click += TabContextMenuItemLock_Click;
            itms.Add(tabContextMenuItemLock);

            tabContextMenuItemGroup = new();
            tabContextMenuItemGroup.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tabContextMenuItemGroup.Text = "浏览组";
            tabContextMenuItemGroup.Name = "tabContextMenuItemGroup";
            itms.Add(tabContextMenuItemGroup);

            tabContextMenuItemWorkGroup = new();
            tabContextMenuItemWorkGroup.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tabContextMenuItemWorkGroup.Text = "加入工作组";
            tabContextMenuItemWorkGroup.Image = Resources.ws_work_32;
            tabContextMenuItemWorkGroup.Name = "tsmiAddWorkGroup";
            tabContextMenuItemWorkGroup.Click += (object? sender, EventArgs e) =>
            {
                if (mouseClickItem == null) return;
                mouseClickItem.WorkGroup = "work";
                ConfigService.BrowsingHis.ModifyWorkgroup(mouseClickItem.Name, "work");
                UpdateLayout();
                Invalidate();
                mouseClickItem = null;
            };
            tabContextMenuItemGroup.DropDownItems.Add(tabContextMenuItemWorkGroup);

            tabContextMenuItemReadGroup = new();
            tabContextMenuItemReadGroup.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tabContextMenuItemReadGroup.Text = "加入阅读组";
            tabContextMenuItemReadGroup.Image = Resources.ws_readbook_32;
            tabContextMenuItemReadGroup.Name = "tsmiAddReadGroup";
            tabContextMenuItemReadGroup.Click += (object? sender, EventArgs e) =>
            {
                if (mouseClickItem == null) return;
                mouseClickItem.WorkGroup = "read";
                ConfigService.BrowsingHis.ModifyWorkgroup(mouseClickItem.Name,"read");
                UpdateLayout();
                Invalidate();
                mouseClickItem = null;
            };
            tabContextMenuItemGroup.DropDownItems.Add(tabContextMenuItemReadGroup);

        }

        private void TabContextMenuItemLock_Click(object? sender, EventArgs e)
        {
            if (mouseClickItem == null) return;
            mouseClickItem.Locked = !mouseClickItem.Locked;    
            ConfigService.BrowsingHis.ModifyLocked(mouseClickItem.Name,mouseClickItem.Locked);
            mouseClickItem = null;
            UpdateLayout();
            Invalidate();
        }

        #endregion tab右键菜单

        #endregion 初始化

        #region 控件属性
        /// <summary>
        /// 标签位置，默认Bottom
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(TabHeaderLocation.Bottom)]
        public TabHeaderLocation HeaderLocation {
			get{ return tabHeaderLocation; }
			set {
				tabHeaderLocation = value; 
			}
		}

		/// <summary>
		/// 是否始终显示关闭按钮
		/// true 始终显示
		/// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        public bool DisplayCloseButton
        {
            get { return isDisplayCloseButton; }
            set
            {
                isDisplayCloseButton = value;
            }
        }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BrowserTabStripItemCollection Items => items;

		[DefaultValue(typeof(Size), "350,200")]
		public new Size Size {
			get {
				return base.Size;
			}
			set {
				if (!(base.Size == value)) {
					base.Size = value;
					UpdateLayout();
				}
			}
		}

        public HitTestResult HitTest(BrowserTabStripItem itm,Point pt) {
			if (itm.CloseButton!.IsVisible && itm.CloseButton.Rect.Contains(pt)) {
				return HitTestResult.CloseButton;
			}
			if (GetTabItemByPoint(pt) != null) {
				return HitTestResult.TabItem;
			}
			return HitTestResult.None;
		}

        #endregion 控件属性

        #region Tab操作

        /// <summary>
        /// 所有对选择项的变动最好都通过该属性
        /// 这样很有利于控制
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(null)]
        public BrowserTabStripItem? SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                BrowserTabStripItem? val = value;

                if (selectedItem == val)
                {
                    return;
                }

                if (val == null)
                {
                    BrowserTabStripItem? newItem = items.GetFirstVisible(this.currentWorkGroup);
                    if (newItem != null)
                    {
                        val = newItem;
                    }
                }

                foreach (BrowserTabStripItem itm in items)
                {
                    UnSelectItem(itm);
                }
                if (val != null)
                {
                    selectedItem = val;
                    selectedItem!.Dock = DockStyle.Fill;
                    selectedItem.Visible = true;
                    selectedItem.Selected = true;
                    selectedItem.Show();
                    this.TabStripItemSelectionChanged?.Invoke(new TabStripItemChangedEventArgs(selectedItem, BrowserTabStripItemChangeTypes.SelectionChanged));
                }
                Invalidate();

            }
        }

        /// <summary>
        /// 移除并设置选中一个
        /// </summary>
        /// <param name="tabItem"></param>
        public void RemoveTab(BrowserTabStripItem tabItem) {

            if (selectedItem == tabItem)
            {
                List<BrowserTabStripItem> lst = items.GetVisible(this.currentWorkGroup);
                int idx = lst.IndexOf(tabItem);
                UnSelectItem(tabItem);
                Items.Remove(tabItem);
                lst.Remove(tabItem);
                if (lst.Count > 0)
                {
                    if (idx<0) idx=0;
                    if(idx>=lst.Count) idx=lst.Count-1;
                    SelectedItem = lst[idx];
                }
                else
                {
                    SelectedItem = null;
                }
            }
            else
            {
                Items.Remove(tabItem);
                Invalidate();
            }
		}

		public BrowserTabStripItem? GetTabItemByPoint(Point pt) {
            List<BrowserTabStripItem> lst = items.GetVisible(this.currentWorkGroup);
			foreach (BrowserTabStripItem item in lst)
			{
                if (item.StripRect.Contains(pt) && item.Visible && item.IsDrawn)
                {
					return item;
                }
            }
			return null;
		}

        private void UnSelectItem(BrowserTabStripItem tabItem) {
			tabItem.Selected = false;
            tabItem.Hide();
        }
        #endregion Tab操作

        #region 标签绘制相关
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            UpdateLayout();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {

            if (selectedItem == null && Items.Count > 0 || selectedItem != null && selectedItem.WorkGroup != this.currentWorkGroup)
            {
                SelectedItem = Items.GetFirstVisible(this.currentWorkGroup);
                return;// 改变选项后，会在SelectedItem再次引起重绘，所以这里就直接退出了
            }
            Rectangle clientRectangle = base.ClientRectangle;
			clientRectangle.Width--;
			clientRectangle.Height--;
            tabStartPosi = DEF_START_POS;
            //e.Graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
			//e.Graphics.FillRectangle(Brushes.White, clientRectangle);

            #region 填充标签所在区域
            Rectangle tabAreaRect = new Rectangle(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, DEF_TAB_HEADER_HEIGHT);
            if (tabHeaderLocation == TabHeaderLocation.Bottom)
            {
                tabAreaRect.Y = clientRectangle.Height-DEF_TAB_HEADER_HEIGHT;
            }
            else if (tabHeaderLocation == TabHeaderLocation.Left)
            {                
            }
            else if (tabHeaderLocation == TabHeaderLocation.Right)
            {
            }
            else
            {
            }
            e.Graphics.FillRectangle(SystemBrushes.GradientInactiveCaption, tabAreaRect);
            #endregion 填充标签所在区域

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            List<BrowserTabStripItem> normalTabs = new List<BrowserTabStripItem>();

            foreach (BrowserTabStripItem item in Items)
            {
				if ((item.Visible || base.DesignMode) && item.WorkGroup == this.currentWorkGroup)
				{
                    item.CloseButton!.IsVisible = !item.Locked;
                    if (item.Locked)
                    {
                        OnCalcTabPage(e.Graphics, item);
                        item.IsDrawn = false;
                        OnDrawTabButton(e.Graphics, item);
                    }
                    else
                    {
                        normalTabs.Add(item);
                    }
				}
			}

            foreach (BrowserTabStripItem item in normalTabs)
            {
                OnCalcTabPage(e.Graphics, item);
                item.IsDrawn = false;
                OnDrawTabButton(e.Graphics, item);
            }

			//if (Items.DrawnCount == 0 || Items.VisibleCount == 0)
			//{
			//	e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_TAB_HEADER_HEIGHT), new Point(base.ClientRectangle.Width, DEF_TAB_HEADER_HEIGHT));
			//}
			//else if (SelectedItem != null && SelectedItem.IsDrawn)
			//{
				//int num = (int)(SelectedItem.StripRect.Height / 4f);
    //            if (tabHeaderLocation == TabHeaderLocation.Bottom)
    //            {
    //                e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, 0), new Point(base.ClientRectangle.Width, 0));
				//}
    //            else if (tabHeaderLocation == TabHeaderLocation.Left)
    //            {
    //                Point point = new Point((int)SelectedItem.StripRect.Left - num, DEF_TAB_HEADER_HEIGHT);
    //                e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_TAB_HEADER_HEIGHT), point);
    //                point.X += (int)SelectedItem.StripRect.Width + num * 2;
    //                e.Graphics.DrawLine(SystemPens.ControlDark, point, new Point(base.ClientRectangle.Width, DEF_TAB_HEADER_HEIGHT));
    //            }
    //            else if (tabHeaderLocation == TabHeaderLocation.Right)
    //            {
    //                Point point = new Point((int)SelectedItem.StripRect.Left - num, DEF_TAB_HEADER_HEIGHT);
    //                e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_TAB_HEADER_HEIGHT), point);
    //                point.X += (int)SelectedItem.StripRect.Width + num * 2;
    //                e.Graphics.DrawLine(SystemPens.ControlDark, point, new Point(base.ClientRectangle.Width, DEF_TAB_HEADER_HEIGHT));
    //            }
    //            else
    //            {
    //                Point point = new Point((int)SelectedItem.StripRect.Left - num, DEF_TAB_HEADER_HEIGHT);
    //                e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_TAB_HEADER_HEIGHT), point);
    //                point.X += (int)SelectedItem.StripRect.Width + num * 2;
    //                e.Graphics.DrawLine(SystemPens.ControlDark, point, new Point(base.ClientRectangle.Width, DEF_TAB_HEADER_HEIGHT));
    //            }
                
			//}

            // 绘制添加按钮
            OnDrawAddTabButton(e.Graphics);
        }

        private RectangleF OnCalcTabPage(Graphics g, BrowserTabStripItem? currentItem)
        {
            int tabWidth;
            int icoWidth = 24;
            int closeWidth = 17;
            
            if (currentItem == null)
            {
                tabWidth = DEF_ADD_BUTTON_WIDTH;
            }
            else
            {
                currentItem.CutTitle = currentItem.Title;
                if (currentItem.Locked)
                {
                    tabWidth = DEF_TAB_LOCKER_WIDTH;
                    currentItem.CutTitle = currentItem.Title + ".";
                }
                else
                {
                    tabWidth = DEF_MAX_TAB_SIZE;
                }
                if (tabWidth >= DEF_MAX_TAB_SIZE)
                {
                    tabWidth = DEF_MAX_TAB_SIZE;
                    string title = currentItem.Title;
                    SizeF sizeF = g.MeasureString(title, Font);
                    int excludedWith = icoWidth + closeWidth + 8;
                    int fontAreaWidth = tabWidth - excludedWith;
                    if (sizeF.Width <= fontAreaWidth)
                    {
                        tabWidth = Convert.ToInt32(sizeF.Width) + excludedWith;
                    }
                    else
                    {
                        while (sizeF.Width > fontAreaWidth)
                        {
                            title = title.Substring(0, title.Length - 1);
                            sizeF = g.MeasureString(title+"...", Font);
                        }
                        currentItem.CutTitle = title + "...";
                    }
                }
                
            }
            float y = 3f;
            if (tabHeaderLocation == TabHeaderLocation.Bottom)
            {
                y= this.Height - DEF_TAB_HEADER_HEIGHT;
            }
            else if (tabHeaderLocation == TabHeaderLocation.Left)
            {
                y = 3f;
            }
            else if (tabHeaderLocation == TabHeaderLocation.Right)
            {
                y = 3f;
            }
            else
            {
                y = 3f;
            }
            RectangleF rect = new (tabStartPosi, y, tabWidth, DEF_TAB_HEADER_HEIGHT);
            if (currentItem != null)
            {
                currentItem.StripRect = rect;
            }
            tabStartPosi += tabWidth;
            return rect;
        }

        private void OnDrawTabButton(Graphics g, BrowserTabStripItem currentItem)
        {
            Font font = defaultFont;
            RectangleF stripRect = currentItem.StripRect;
            GraphicsPath graphicsPath = new GraphicsPath();
            float tabHeaderLeft = stripRect.Left;
            float tabHeaderRight = stripRect.Right;

            float tabHeaderWidth = stripRect.Width;
            float tabHeaderHeight = stripRect.Height;

            // 斜角
            //float tabHeaderBias = tabHeaderHeight / 6f;
            float tabHeaderBias = 0;

            float titleLocationY = 0f;
            float titleDiffY = 0f;

            float iconLocationY = 0f;

            SolidBrush brush = new ((currentItem == SelectedItem) ? Color.White : SystemColors.GradientInactiveCaption);

            #region 绘制并填充标签
            if (tabHeaderLocation == TabHeaderLocation.Bottom)
            {
                float tabHeaderBottom = stripRect.Bottom-2;
                float tabHeaderY = stripRect.Y;
                titleLocationY = tabHeaderY;
                iconLocationY = tabHeaderY;
                titleDiffY = 5f;
                graphicsPath.AddLine(tabHeaderLeft - tabHeaderBias, tabHeaderY, tabHeaderLeft + tabHeaderBias, tabHeaderBottom);
                graphicsPath.AddLine(tabHeaderRight - tabHeaderBias, tabHeaderBottom, tabHeaderRight + tabHeaderBias, tabHeaderY);
                graphicsPath.CloseFigure();

                g.FillPath(brush, graphicsPath);
                //g.DrawPath(SystemPens.ControlDark, graphicsPath);
                g.DrawLine(SystemPens.ControlDark, tabHeaderLeft, tabHeaderY + 3, tabHeaderLeft, tabHeaderBottom - 3);
                if (currentItem == SelectedItem)
                {
                    float y = tabHeaderY;
                    g.DrawLine(new Pen(brush), tabHeaderLeft - tabHeaderBias, y, tabHeaderLeft + tabHeaderWidth + tabHeaderBias, y);
                }
            }
            else
            {
                float tabHeaderBottom = stripRect.Bottom - 1f;
                float tabHeaderY = 3f;
                titleDiffY = 5f;
                graphicsPath.AddLine(tabHeaderLeft - tabHeaderBias, tabHeaderBottom, tabHeaderLeft + tabHeaderBias, tabHeaderY);
                graphicsPath.AddLine(tabHeaderRight - tabHeaderBias, tabHeaderY, tabHeaderRight + tabHeaderBias, tabHeaderBottom);
                graphicsPath.CloseFigure();

                g.FillPath(brush, graphicsPath);
                g.DrawPath(SystemPens.ControlDark, graphicsPath);
                if (currentItem == SelectedItem)
                {
                    g.DrawLine(new Pen(brush), tabHeaderLeft - 9f, tabHeaderHeight + 2f, tabHeaderLeft + tabHeaderWidth+tabHeaderBias - 1f, tabHeaderHeight + 2f);
                }
            }
            #endregion 绘制并填充标签

            #region 绘制图标
            float sttIcoLocationX = tabHeaderLeft + 5f;
            if (currentItem.Image != null)
            {
                float icoWidth = 16f;
                float sttIcoLocationY = iconLocationY + DEF_TAB_HEADER_HEIGHT/2-icoWidth/2;
                PointF icoLocation = new PointF(sttIcoLocationX, sttIcoLocationY);
                RectangleF iconRectangle = stripRect;
                iconRectangle.Location = icoLocation;
                iconRectangle.Width = icoWidth;
                iconRectangle.Height = iconRectangle.Width;
                sttIcoLocationX = sttIcoLocationX + iconRectangle.Width;

                g.DrawImage(currentItem.Image, iconRectangle);
            }
            #endregion 绘制图标

            #region 绘制标题
            if (currentItem.Locked == false)
            {
                float sttLocationX = sttIcoLocationX + 3f;

                float sttLocationY = titleLocationY + titleDiffY;
                string title = currentItem.Title;
                SizeF sizeF = g.MeasureString(title, font);

                float fontHeight = sizeF.Height;
                if (fontHeight>0)
                {
                    sttLocationY = titleLocationY+tabHeaderHeight/2-fontHeight/2;
                }

                PointF location = new (sttLocationX, sttLocationY);
                RectangleF layoutRectangle = stripRect;
                layoutRectangle.Location = location;
                layoutRectangle.Width = tabHeaderWidth - (layoutRectangle.Left - tabHeaderLeft) - 4f;
                if (currentItem == selectedItem)
                {
                    layoutRectangle.Width -= 12f;
                }
                layoutRectangle.Height = 24f;

                if (currentItem == SelectedItem)
                {
                    g.DrawString(currentItem.CutTitle, font, new SolidBrush(ForeColor), layoutRectangle, sf);
                }
                else
                {
                    g.DrawString(currentItem.CutTitle, font, new SolidBrush(ForeColor), layoutRectangle, sf);
                }
            }
            #endregion 绘制标题

            // 绘制关闭按钮
            if (!currentItem.Locked)
            {
                OnDrowCloseButton(g, currentItem);
            }

            currentItem.IsDrawn = true;
        }

        #region 绘制关闭按钮
        /// <summary>
        /// 绘制关闭按钮
        /// </summary>
        /// <param name="g"></param>
        private void OnDrowCloseButton(Graphics g, BrowserTabStripItem currentItem)
        {
            if (isDisplayCloseButton)
            {
                currentItem.CloseButton!.IsVisible = currentItem.CanClose;
                if (currentItem.CanClose)
                {
                    currentItem.CloseButton.CalcBounds(currentItem);
                    currentItem.CloseButton.Draw(g);
                }
            }
            else
            {
                if (SelectedItem != null && SelectedItem == currentItem && SelectedItem.Locked == false)
                {
                    SelectedItem.CloseButton!.IsVisible = SelectedItem.CanClose;
                    if (SelectedItem.CanClose)
                    {
                        SelectedItem.CloseButton.CalcBounds(selectedItem!);
                        SelectedItem.CloseButton.Draw(g);
                    }
                }
            }
        }
        #endregion 绘制关闭按钮

        /// <summary>
        /// 绘制新增标签按钮
        /// </summary>
        /// <param name="g"></param>
        private void OnDrawAddTabButton(Graphics g)
        {
            addNewTabRect = OnCalcTabPage(g,null);
            GraphicsPath graphicsPath = new GraphicsPath();
            float tabHeaderLeft = addNewTabRect.Left;
            float tabHeaderRight = addNewTabRect.Right;

            float tabHeaderWidth = addNewTabRect.Width;
            float tabHeaderHeight = addNewTabRect.Height;

            // 斜角
            //float tabHeaderBias = tabHeaderHeight / 6f;
            float tabHeaderBias = 0;

            float titleLocationY = 0f;
            float titleDiffY;

            float iconLocationY;

            SolidBrush brush = new SolidBrush(isAddTabMove ? SystemColors.GradientActiveCaption : SystemColors.GradientInactiveCaption);

            #region 绘制并填充标签
            if (tabHeaderLocation == TabHeaderLocation.Bottom)
            {
                float tabHeaderBottom = addNewTabRect.Bottom-1;
                float tabHeaderY = addNewTabRect.Y;
                titleLocationY = tabHeaderY;
                iconLocationY = tabHeaderY;
                titleDiffY = 0f;
                graphicsPath.AddLine(tabHeaderLeft - tabHeaderBias, tabHeaderY, tabHeaderLeft + tabHeaderBias, tabHeaderBottom);
                graphicsPath.AddLine(tabHeaderRight - tabHeaderBias, tabHeaderBottom, tabHeaderRight + tabHeaderBias, tabHeaderY);
                graphicsPath.CloseFigure();

                g.FillPath(brush, graphicsPath);
                //g.DrawPath(SystemPens.ControlDark, graphicsPath);
            }
            else
            {
                float tabHeaderBottom = addNewTabRect.Bottom - 1f;
                float tabHeaderY = 3f;
                titleDiffY = 5f;
                graphicsPath.AddLine(tabHeaderLeft - tabHeaderBias, tabHeaderBottom, tabHeaderLeft + tabHeaderBias, tabHeaderY);
                graphicsPath.AddLine(tabHeaderRight - tabHeaderBias, tabHeaderY, tabHeaderRight + tabHeaderBias, tabHeaderBottom);
                graphicsPath.CloseFigure();

                g.FillPath(brush, graphicsPath);
                //g.DrawPath(SystemPens.ControlDark, graphicsPath);
            }
            #endregion 绘制并填充标签

            #region 绘制图标
            float sttIcoLocationX = tabHeaderLeft + 5f;
            //if (currentItem.Image != null)
            //{
            //    float icoWidth = 16f;
            //    float sttIcoLocationY = iconLocationY + DEF_TAB_HEADER_HEIGHT/2-icoWidth/2;
            //    PointF icoLocation = new PointF(sttIcoLocationX, sttIcoLocationY);
            //    RectangleF iconRectangle = stripRect;
            //    iconRectangle.Location = icoLocation;
            //    iconRectangle.Width = icoWidth;
            //    iconRectangle.Height = iconRectangle.Width;
            //    sttIcoLocationX = sttIcoLocationX + iconRectangle.Width;

            //    g.DrawImage(currentItem.Image, iconRectangle);
            //}
            #endregion 绘制图标

            #region 绘制标题
            string title = "+";
            float sttLocationX = sttIcoLocationX + 3f;

            float sttLocationY = titleLocationY + titleDiffY;
            sttLocationX = tabHeaderLeft + 3f;
            Font font = new("Symbol", 18f, FontStyle.Regular);
            SizeF sizeF = g.MeasureString(title, font);

            float fontHeight = sizeF.Height;
            if (fontHeight>0)
            {
                sttLocationY = titleLocationY+tabHeaderHeight/2-fontHeight/2-2;
            }

            PointF location = new (sttLocationX, sttLocationY);
            RectangleF layoutRectangle = addNewTabRect;
            layoutRectangle.Location = location;
            layoutRectangle.Width = tabHeaderWidth - (layoutRectangle.Left - tabHeaderLeft) - 4f;
   
            layoutRectangle.Height = 24f;

            g.DrawString(title, font, new SolidBrush(ForeColor), layoutRectangle, sf);
            #endregion 绘制标题

        }

        #endregion 标签绘制相关

        #region 鼠标动作

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (addNewTabRect.Contains(e.Location))
            {
                AddTabClicked?.Invoke(this, new EventArgs());
                return;
            }

            BrowserTabStripItem? tabItemByPoint = GetTabItemByPoint(e.Location);
            if (tabItemByPoint == null) return;

            if (this.startDragging && this.dragItem != null && tabItemByPoint != this.dragItem && e.Button == MouseButtons.Left)
            {
                RectangleF stripRect = tabItemByPoint.StripRect;
                RectangleF rect = new RectangleF(stripRect.Left + stripRect.Width/2, stripRect.Top, stripRect.Width, stripRect.Height);
                int currIdx = items.IndexOf(tabItemByPoint);
                int sttIdx = items.IndexOf(this.dragItem);

                if (currIdx > sttIdx) // 向后移动
                {
                    currIdx = currIdx - 1;
                }
                if (rect.Contains(e.Location))
                {
                    currIdx = currIdx + 1;
                }
                items.MoveTo(currIdx, this.dragItem);
                this.AllowDrop = false;
                Invalidate();
                UpdateLayout();
                this.startDragging = false;
                this.dragItem = null;
                return;
            }
            this.startDragging = false;
            this.dragItem = null;

            mouseClickItem = tabItemByPoint;

            HitTestResult hitTestResult = HitTest(tabItemByPoint, e.Location);
            if (hitTestResult == HitTestResult.TabItem)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (tabItemByPoint != null)
                    {
                        SelectedItem = tabItemByPoint;
                        Invalidate();
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    tabContextMenuItemClose!.Enabled = !tabItemByPoint.Locked;
                    tabContextMenuItemLock!.Text = tabItemByPoint.Locked ? "取消锁定" : "锁定";
                    tabContextMenuItemWorkGroup!.Visible = tabItemByPoint.WorkGroup!="work";
                    tabContextMenuItemReadGroup!.Visible = tabItemByPoint.WorkGroup!="read";
                    tabContextMenuItemLock.Image = tabItemByPoint.Locked ? Resources.unlock_32 : Resources.lock_32;

                    tabContextMenu!.Show(e.Location);
                }
            }
            else if (hitTestResult == HitTestResult.CloseButton && e.Button == MouseButtons.Left)
            {
                if (tabItemByPoint != null)
                {
                    TabStripItemClosingEventArgs tabStripItemClosingEventArgs = new TabStripItemClosingEventArgs(tabItemByPoint);
                    this.TabStripItemClosing?.Invoke(tabStripItemClosingEventArgs);

                    if (!tabStripItemClosingEventArgs.Cancel && tabItemByPoint.CanClose)
                    {
                        RemoveTab(tabItemByPoint);
                        this.TabStripItemClosed?.Invoke(this, new BrowserTabStripItemClosedEventArgs(tabItemByPoint));
                        Invalidate();
                    }
                }

            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
            BrowserTabStripItem? tabItemByPoint = GetTabItemByPoint(e.Location);
            if (tabItemByPoint == null) return;
            this.startDragging = true;
            this.dragItem = tabItemByPoint;
            Debug.WriteLine("要拖动的是：" + this.dragItem.Title);
            //this.AllowDrop = true;
            //DoDragDrop(tabItemByPoint, DragDropEffects.Move);
		}

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

        }

        protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);

            #region 鼠标在添加按钮时重绘
            if (addNewTabRect.Contains(e.Location))
            {
                isAddTabMove = true;
                Invalidate();
            }
            else if (isAddTabMove)
            {
                isAddTabMove = false;
                Invalidate();
            }
            #endregion 鼠标在添加按钮时重绘

            if (this.startDragging && e.Button == MouseButtons.Left)
            {
                //Cursor.Current = Cursors.
            }

            //标题超长显示
            BrowserTabStripItem? itm = GetTabItemByPoint(e.Location);
            if (itm == null || toolTip == null) return;

            if (itm.CutTitle != itm.Title)
            {
                toolTip.Show(itm.Title, this);
            }
            else if(itm.CutTitle == itm.Title)
            {
                toolTip.Hide(this);
            }
            //关闭按钮式样变换
            if (itm.CloseButton!.IsVisible) {
				if (itm.CloseButton.Rect.Contains(e.Location)) {
                    itm.CloseButton.IsMouseOver = true;
					Invalidate(itm.CloseButton.RedrawRect);
				}
				else if (itm.CloseButton.IsMouseOver) {
                    itm.CloseButton.IsMouseOver = false;
					Invalidate(itm.CloseButton.RedrawRect);
				}
			}
		}

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave(e);

            toolTip.Hide(this);

   //         closeButton.IsMouseOver = false;
			//if (closeButton.IsVisible) {
			//	Invalidate(closeButton.RedrawRect);
			//}
		}

        #endregion 鼠标动作

        protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			if (!isIniting) {
                UpdateLayout();
			}
            if (ccbWorkSpace !=null)
            {
                ccbWorkSpace.Location=new Point(0, base.ClientRectangle.Height-DEF_TAB_HEADER_HEIGHT+1);
            }
        }
		
		private void UpdateLayout() {
			sf.Trimming = StringTrimming.EllipsisCharacter;
			sf.FormatFlags |= StringFormatFlags.NoWrap;
			sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;
			//stripButtonRect = new Rectangle(0, 0, base.ClientSize.Width - 40 - 2, 10);
			if (tabHeaderLocation == TabHeaderLocation.Bottom)
            {
                base.DockPadding.Top = 1;
                base.DockPadding.Bottom = DEF_TAB_HEADER_HEIGHT;
                base.DockPadding.Right = 1;
                base.DockPadding.Left = 1;
            }
            else if (tabHeaderLocation == TabHeaderLocation.Left)
            {
                base.DockPadding.Top = DEF_TAB_HEADER_HEIGHT;
                base.DockPadding.Bottom = 1;
                base.DockPadding.Right = 1;
                base.DockPadding.Left = 1;
            }
            else if (tabHeaderLocation == TabHeaderLocation.Right)
            {
                base.DockPadding.Top = DEF_TAB_HEADER_HEIGHT;
                base.DockPadding.Bottom = 1;
                base.DockPadding.Right = 1;
                base.DockPadding.Left = 1;
            }
            else
            {
                base.DockPadding.Top = DEF_TAB_HEADER_HEIGHT;
                base.DockPadding.Bottom = 1;
                base.DockPadding.Right = 1;
                base.DockPadding.Left = 1;
            }
            
		}

		private void OnCollectionChanged(object? sender, CollectionChangeEventArgs e)
		{
            if (e == null || e.Element == null) return;

			BrowserTabStripItem? fATabStripItem = (BrowserTabStripItem)e.Element;
			InvokeIfNeeded(() =>
			{
                if (e.Action == CollectionChangeAction.Add)
                {
                    Controls.Add(fATabStripItem);

                    if (selectedItem == null)
                    {
                        SelectedItem = Items.FirstVisible;
                    }
                    this.TabStripItemSelectionChanged?.Invoke(new TabStripItemChangedEventArgs(fATabStripItem!, BrowserTabStripItemChangeTypes.Added));
                    
				}
				else if (e.Action == CollectionChangeAction.Remove)
				{
					Controls.Remove(fATabStripItem);
                    this.TabStripItemSelectionChanged?.Invoke(new TabStripItemChangedEventArgs(fATabStripItem!, BrowserTabStripItemChangeTypes.Removed));
				}
                //else
                //{
                // this.TabStripItemSelectionChanged?.Invoke(new TabStripItemChangedEventArgs(fATabStripItem, BrowserTabStripItemChangeTypes.Changed));
                //}
            });
			UpdateLayout();
			Invalidate();
		}

		public bool ShouldSerializeFont() {
			if (Font != null) {
				return !Font.Equals(defaultFont);
			}
			return false;
		}

		public bool ShouldSerializeSelectedItem() {
			return true;
		}

		public bool ShouldSerializeItems() {
			return items.Count > 0;
		}

		public new void ResetFont() {
			Font = defaultFont;
		}

		public void BeginInit() {
			isIniting = true;
		}

		public void EndInit() {
			isIniting = false;
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				items.CollectionChanged -= OnCollectionChanged;

				foreach (BrowserTabStripItem item in items) {
					if (item != null && !item.IsDisposed) {
						item.Dispose();
					}
				}

				if (sf != null) {
					sf.Dispose();
				}
			}
			base.Dispose(disposing);
		}

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