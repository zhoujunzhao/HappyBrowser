using CefSharp;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class NewWindowEventArgs : System.EventArgs
    {
        private IWindowInfo? _windowInfo;

        public IWindowInfo WindowInfo
        {
            get { return _windowInfo; }
            set { value = _windowInfo; }
        }
        public string url { get; set; }
        public NewWindowEventArgs(IWindowInfo? windowInfo, string url)
        {
            _windowInfo = windowInfo;
            this.url = url;
        }
    }
}
