
namespace HappyBrowser.Entity
{
    public class BookMarkEntity
    {
        public BookMarkEntity(string? title, string? url, Image? ico = null)
        {
            Title=title;
            Url=url;
            Ico=ico;
        }

        public string? Title { get; set; }
        public string? Url{get; set; }
        public Image? Ico{get; set; }
    }
}
