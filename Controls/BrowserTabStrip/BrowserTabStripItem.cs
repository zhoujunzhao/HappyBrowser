using System.ComponentModel;

namespace HappyBrowser.Controls.BrowserTabStrip {
	[ToolboxItem(false)]
	[DefaultProperty("Title")]
	[DefaultEvent("Changed")]
	public class BrowserTabStripItem : Panel {
        
		public event EventHandler<EventArgs>? Changed;

        private RectangleF stripRect = Rectangle.Empty;

		private Image? image;

		private bool canClose = true;

		private bool selected;

		private bool visible = true;

		private bool isDrawn;

		private bool locked = false;
		private string workGroup;

		private string title = string.Empty;

		private string cutTitle = string.Empty;

		private CtlChromiumBrowser? chromiumBrowser;

        private BrowserTabStripCloseButton? closeButton;

        public BrowserTabStripItem() : this(string.Empty, null)
        {
        }

        public BrowserTabStripItem(Control displayControl)
            : this(string.Empty, displayControl)
        {
        }

        public BrowserTabStripItem(string caption, Control? displayControl)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
            SetStyle(ControlStyles.ResizeRedraw, value: true);
            SetStyle(ControlStyles.UserPaint, value: true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
            SetStyle(ControlStyles.ContainerControl, value: true);
            selected = false;
            Visible = true;
            UpdateText(caption, displayControl);
            if (displayControl != null)
            {
                base.Controls.Add(displayControl);
            }
            closeButton = new();
        }

		public CtlChromiumBrowser? Browser
		{
			get 
			{
				if(chromiumBrowser != null) return chromiumBrowser;
				foreach (Control c in this.Controls)
				{
					if (c is CtlChromiumBrowser)
					{
						chromiumBrowser = (CtlChromiumBrowser)c;
						return chromiumBrowser;

                    }
				}
				return null;
			}
		}

        [Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size Size {
			get {
				return base.Size;
			}
			set {
				base.Size = value;
			}
		}

		[DefaultValue(true)]
		public new bool Visible {
			get {
				return visible;
			}
			set {
				if (visible != value) {
					visible = value;
					OnChanged();
				}
			}
		}

        // 关闭按钮
        public BrowserTabStripCloseButton? CloseButton 
		{ 
			get { return closeButton; }

			set {
				if (value != null)
				{
					closeButton = value;
				}
			}
		}

        /// <summary>
        /// 是否是锁定标签
        /// </summary>
        [DefaultValue(true)]
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                if (locked != value)
                {
                    locked = value;
                }
            }
        }

        /// <summary>
        /// 标签所属组
        /// </summary>
        [DefaultValue("")]
        public string WorkGroup
        {
            get
            {
                return workGroup;
            }
            set
            {
                if (workGroup != value)
                {
                    workGroup = value;
                }
            }
        }

        internal RectangleF StripRect {
			get {
				return stripRect;
			}
			set {
				stripRect = value;
			}
		}

		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool IsDrawn {
			get {
				return isDrawn;
			}
			set {
				if (isDrawn != value) {
					isDrawn = value;
				}
			}
		}

		/// <summary>
		/// 网站图标
		/// </summary>
		[DefaultValue(null)]
		public Image? Image {
			get {
				return image;
			}
			set {
				image = value;
			}
		}

		[DefaultValue(true)]
		public bool CanClose {
			get {
				return canClose;
			}
			set {
				canClose = value;
			}
		}

		[DefaultValue("Name")]
		public string Title {
			get {
				return title;
			}
			set {
				if (!(title == value)) {
					title = value;
					OnChanged();
				}
			}
		}

		/// <summary>
		/// 截取的标题
		/// </summary>
        [DefaultValue("Title")]
        public string CutTitle
        {
            get
            {
                return cutTitle;
            }
            set
            {
                cutTitle = value;
            }
        }

        [DefaultValue(false)]
		[Browsable(false)]
		public bool Selected {
			get {
				return selected;
			}
			set {
				if (selected != value) {
					selected = value;
				}
			}
		}

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			if (disposing && image != null) {
				image.Dispose();
			}
		}

		public bool ShouldSerializeIsDrawn() {
			return false;
		}

		public bool ShouldSerializeDock() {
			return false;
		}

		public bool ShouldSerializeControls() {
			if (base.Controls != null) {
				return base.Controls.Count > 0;
			}
			return false;
		}

		public bool ShouldSerializeVisible() {
			return true;
		}

		private void UpdateText(string caption, Control? displayControl) {
			if (caption.Length <= 0 && displayControl != null) {
				Title = displayControl.Text;
			}
			else if (caption != null) {
				Title = caption;
			}
			else {
				Title = string.Empty;
			}
		}

		public void Assign(BrowserTabStripItem item) {
			Visible = item.Visible;
			Text = item.Text;
			CanClose = item.CanClose;
			base.Tag = item.Tag;
		}

		protected internal virtual void OnChanged() {
			if (this.Changed != null) {
				this.Changed(this, EventArgs.Empty);
			}
		}

		public override string ToString() {
			return Title;
		}
	}
}