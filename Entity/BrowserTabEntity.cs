using HappyBrowser.Controls;
using HappyBrowser.Controls.BrowserTabStrip;

namespace HappyBrowser.Entity
{
    public class BrowserTabEntity
    {
        public bool? IsOpen;

        public string? OrigURL;
        public string? CurURL;
        public string? Title;
        public Image? Ico;

        public string? RefererURL;

        public bool IsBookMarkOpen=false;
        public string? BookMarkId;

        public DateTime? DateCreated;

        public BrowserTabStripItem? Tab;
        public CtlChromiumBrowser? Browser;
    }
}
