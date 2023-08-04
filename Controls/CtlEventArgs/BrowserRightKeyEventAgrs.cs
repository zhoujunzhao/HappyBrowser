using System.ComponentModel;

namespace HappyBrowser.Controls.CtlEventArgs
{
    /// <summary>
    /// 做为自定CtlTabItem中的Browser所有事件的统一参数
    /// </summary>
    public class BrowserRightKeyEventAgrs : EventArgs
    {

        public BrowserRightKeyEventAgrs(RightKeyType rightKeyType,object data=null) 
        {
            RightKeyType = rightKeyType;
            Data = data;
        }


        public RightKeyType RightKeyType { set; get; }

        public object? Data { set; get; }
    }

    public enum RightKeyType
    {
        [Description("空值")]
        No,
        [Description("打开查找窗口")]
        FindWindow,
        [Description("打开新网页")]
        NewWindow
    };
}
