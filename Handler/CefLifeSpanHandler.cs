using CefSharp;
using HappyBrowser.Controls.CtlEventArgs;

namespace HappyBrowser.Handler
{
    public class CefLifeSpanHandler : ILifeSpanHandler
    {
        public event EventHandler<NewWindowEventArgs>? NewWindow;

        public bool DoClose(IWebBrowser browserControl, CefSharp.IBrowser browser)
        {
            return (browser.IsDisposed || browser.IsPopup) ? false : true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser) {
        
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser) { }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            NewWindow?.Invoke(browser, new NewWindowEventArgs(windowInfo, targetUrl));
            newBrowser = null;
            return true;
        }
    }
}
