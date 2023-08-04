
namespace HappyBrowser.Controls.BrowserTabStrip {
	public class BrowserTabStripCloseButton {
		public Rectangle Rect = Rectangle.Empty;

		public Rectangle RedrawRect = Rectangle.Empty;

		public bool IsMouseOver;

		public bool IsVisible;

        private ToolStripProfessionalRenderer? Renderer;

		private BrowserTabStripItem? currentTabItem;

        internal BrowserTabStripCloseButton()
        {
        }

        internal BrowserTabStripCloseButton(ToolStripProfessionalRenderer renderer) {
			Renderer = renderer;
		}

		public void CalcBounds(BrowserTabStripItem tab) {
			currentTabItem = tab;
			int y = (int)tab.StripRect.Height/2-8;
            Rect = new Rectangle((int)tab.StripRect.Right - 20, (int)tab.StripRect.Top + y, 15, 15);
			// 样式变换区域
			RedrawRect = new Rectangle(Rect.X - 2, Rect.Y - 2, Rect.Width + 4, Rect.Height + 4);
		}

		public void Draw(Graphics g) {
			if (IsVisible) 
			{
				Brush backBrush = currentTabItem!.Selected ? Brushes.White : SystemBrushes.GradientInactiveCaption;

                Color color = (IsMouseOver ? Color.White : Color.DarkGray);
				g.FillRectangle(backBrush, Rect);
				if (IsMouseOver) {
					g.FillEllipse(Brushes.IndianRed, Rect);
				}
				int num = 4;
				Pen pen = new(color, 1.6f);
				g.DrawLine(pen, Rect.Left + num, Rect.Top + num, Rect.Right - num, Rect.Bottom - num);
				g.DrawLine(pen, Rect.Right - num, Rect.Top + num, Rect.Left + num, Rect.Bottom - num);
				pen.Dispose();
			}
		}
	}
}