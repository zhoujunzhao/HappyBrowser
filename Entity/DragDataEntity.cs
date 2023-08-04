using CefSharp;

namespace HappyBrowser.Entity
{
    public class DragDataEntity
    {
        private Point startPosition = Point.Empty;
        public DragDataEntity(IDragData data,Point point) 
        {
            if(data != null)
            {
                this.IsLink = data.IsLink;
                this.Link = data.LinkUrl;
                this.IsText = data.IsFragment;
                this.Text = data.FragmentText;
            }
            this.startPosition = point;
        }

        public DragDataEntity(bool isLink,string link,bool isText,string text, Point point) {
            this.IsLink = isLink;
            this.Link = link;
            this.IsText = isText;
            this.Text = text;
            this.startPosition = point;
        }

        public Boolean IsLink { get; set; }
        public String Link { get; set; }
        public Boolean IsText { get; set; }
        public String Text { get; set; }

        public Point StartPosition 
        { 
            get { return startPosition; }
            set { startPosition = value;} 
        }
    }
}
