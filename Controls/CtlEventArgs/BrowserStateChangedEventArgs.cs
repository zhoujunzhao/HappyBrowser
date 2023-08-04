using CefSharp;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class BrowserStateChangedEventArgs : EventArgs
    {
        public BrowserStateChangedEventArgs(bool canGoForward,bool canGoBack,bool canReload,bool isLoading)
        {
            this.CanGoForward = canGoForward;
            this.CanGoBack = canGoBack;
            this.CanReload = canReload;
            this.IsLoading = isLoading;
            this.Browser = null;
        }

        public BrowserStateChangedEventArgs(CtlChromiumBrowser browser)
        {
            this.CanGoForward = browser.CanGoForward;
            this.CanGoBack = browser.CanGoBack;
            this.CanReload = true;
            this.IsLoading = browser.IsLoading;
            this.Browser = null;
        }

        public BrowserStateChangedEventArgs(LoadingStateChangedEventArgs args)
        {
            this.CanGoForward = args.CanGoForward;
            this.CanGoBack = args.CanGoBack;
            this.CanReload = args.CanReload;
            this.IsLoading = args.IsLoading;
            this.Browser = args.Browser;
        }

        public bool CanGoForward { get; }
        public bool CanGoBack { get; }
        public bool CanReload { get; }
        public bool IsLoading { get; }
        public IBrowser? Browser { get; }
    }
}
