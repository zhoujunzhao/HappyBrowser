using System;
using System.ComponentModel;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class BookMarkSubMenuClickedEventArgs : EventArgs
    {

        public BookMarkSubMenuClickedEventArgs(BookMarkType bookMarkType,string id, string? url,Image? ico) 
        { 
            this.BookMarkType = bookMarkType;
            this.Id = id;
            this.Url = url;
            this.Ico = ico;
        }

        public BookMarkType BookMarkType { get;}
        
        /// <summary>
        /// 添加时，这里保存的是parentId
        /// </summary>
        public string Id { get; }

        public string? Url { get;}

        public Image? Ico { get;}
    }

    public enum BookMarkType
    {
        [Description("打开URL")]
        Url,
        [Description("添加新的URL到收藏夹")]
        AddUrl

    };
}
