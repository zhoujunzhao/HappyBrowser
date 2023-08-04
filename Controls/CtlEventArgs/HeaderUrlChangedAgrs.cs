using System.ComponentModel;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class HeaderUrlChangedAgrs
    {
        public HeaderUrlChangedAgrs(string? url, EnumNewTab newTab, string bookMarkId)
        {
            this.Url = url;
            this.NewTab = newTab;
            BookMarkId=bookMarkId;
        }

        public HeaderUrlChangedAgrs(string? url,Image? ico, EnumNewTab newTab, string bookMarkId)
        {
            this.Url = url;
            this.Ico = ico;
            this.NewTab = newTab;
            BookMarkId=bookMarkId;
        }

        public string? Url { get; }

        public Image? Ico { get; }

        public string? BookMarkId { get; }

        public EnumNewTab NewTab { get; }
    }

    /// <summary>
    /// 是否打开新窗口
    /// </summary>
    public enum EnumNewTab
    {
        [Description("在当前页打开")]
        Current,
        [Description("在新页打开")]
        New
    };
}
