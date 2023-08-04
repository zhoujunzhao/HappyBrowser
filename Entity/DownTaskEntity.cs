using HappyBrowser.Controls;
using System.ComponentModel;

namespace HappyBrowser.Entity
{
    public class DownTaskEntity
    {
        public DownTaskEntity() { }

        public DownTaskEntity(int? id, CtlChromiumBrowser webBrowser,EnumDownStatus downStatus)
        {
            Id = id;
            WebBrowser = webBrowser;
            DownStatus = downStatus;
        }

        public int? Id { get; set; }

        public CtlChromiumBrowser? WebBrowser { get; set; }
        public EnumDownStatus? DownStatus { get; set; }
    }

    /// <summary>
    /// 下载状态
    /// </summary>
    public enum EnumDownStatus
    {
        [Description("空，无效")]
        Empty,
        [Description("下载前")]
        Before,
        [Description("下载中")]
        InProgress,
        [Description("下载完成")]
        Complete,
        [Description("下载错误")]
        Error,
        [Description("下载取消")]
        Cancel
    };
}
