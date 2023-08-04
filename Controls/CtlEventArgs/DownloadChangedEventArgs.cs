using CefSharp;
using HappyBrowser.Entity;
using System.ComponentModel;
using DragData = HappyBrowser.Entity.DragDataEntity;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class DownloadChangedEventArgs : EventArgs
    {
        public DownloadChangedEventArgs(EnumDownStatus downStatus, int id, string url, string fileName, IBeforeDownloadCallback beforeDownloadCallback)
        {
            DownStatus = downStatus;
            this.Url = url;
            this.FileName = fileName;
            this.BeforeDownloadCallback = beforeDownloadCallback;
        }

        public DownloadChangedEventArgs(EnumDownStatus downStatus,int id,string url, int percentComplete, DateTime? endTime, IDownloadItemCallback downloadItemCallback)
        {
            DownStatus = downStatus;
            this.Id = id;
            this.Url = url;
            this.PercentComplete = percentComplete;
            this.EndTime = endTime;
            this.DownloadItemCallback = downloadItemCallback;
        }

        public EnumDownStatus DownStatus { get; set; }
        
        public int Id { get; set; }
        
        public string Url { get; set; }


        public string? FileName { get; set; }
        public IBeforeDownloadCallback? BeforeDownloadCallback { get; set; }


        public int? PercentComplete {get; set; }
        public DateTime? EndTime { get; set; }

        public IDownloadItemCallback? DownloadItemCallback { get; set; }

    }

}
