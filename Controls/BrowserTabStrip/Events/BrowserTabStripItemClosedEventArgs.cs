using System;

namespace HappyBrowser.Controls.BrowserTabStrip {
	public class BrowserTabStripItemClosedEventArgs : EventArgs {

		private BrowserTabStripItem _item;

		public BrowserTabStripItem Item {
			get {
				return _item;
			}
			set {
				_item = value;
			}
		}

		public BrowserTabStripItemClosedEventArgs(BrowserTabStripItem item) {
			_item = item;
		}
	}
}