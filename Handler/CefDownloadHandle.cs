using CefSharp;
using HappyBrowser.Controls.CtlEventArgs;
using HappyBrowser.Entity;
using System.Diagnostics;
using System.Windows.Forms.Design;

namespace HappyBrowser.Handler
{
    public class CefDownloadHandle : IDownloadHandler
    {
        public event EventHandler<DownloadChangedEventArgs>? DownloadChanged;

        public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        {
            if (requestMethod.ToLower() == "get")
            {
                return true;
            }
            return false;
        }

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem dItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                DownloadChangedEventArgs args = new (EnumDownStatus.Before, dItem.Id, dItem.Url, dItem.SuggestedFileName, callback);

                DownloadChanged?.Invoke(browser, args);
            }
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem dItem, IDownloadItemCallback callback)
        {
            EnumDownStatus downStatus = EnumDownStatus.Empty;
            if (dItem.IsInProgress)
            {
                downStatus = EnumDownStatus.InProgress;
            }
            else if (dItem.IsComplete)
            {
                downStatus = EnumDownStatus.Complete;
            }
            else if (dItem.IsCancelled)
            {
                downStatus=EnumDownStatus.Cancel;
            }
            else if (dItem.IsValid)
            {
                downStatus=EnumDownStatus.Error;
            }
            if (downStatus != EnumDownStatus.Empty)
            {
                DownloadChangedEventArgs args = new(downStatus, dItem.Id, dItem.Url, dItem.PercentComplete, dItem.EndTime, callback);
                DownloadChanged?.Invoke(browser, args);
            }
            Debug.WriteLine(dItem.OriginalUrl);
        }
    }
}
