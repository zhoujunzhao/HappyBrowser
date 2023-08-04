using CefSharp;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class FaviconChangedEventArgs
    {
        public FaviconChangedEventArgs(CtlChromiumBrowser browser, Image? favicon)
        {
            this.Browser = browser;
            this.Favicon = favicon;
        }

        public CtlChromiumBrowser Browser { get; }
        public Image? Favicon { get; }
    }
}
