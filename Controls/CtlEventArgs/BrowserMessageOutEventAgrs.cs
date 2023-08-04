using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBrowser.Controls.CtlEventArgs
{
    /// <summary>
    /// 做为自定CtlTabItem中的Browser所有事件的统一参数
    /// </summary>
    public class BrowserMessageOutEventAgrs<T> : System.EventArgs
    {

        public BrowserMessageOutEventAgrs() {
            DataType = EnumDataType.No;
        }

        public BrowserMessageOutEventAgrs(EnumDataType dataType, T data) 
        {
            DataType = dataType;
            Data = data;
        }

        public BrowserMessageOutEventAgrs(EnumDataType dataType, T data, T data2)
        {
            DataType = dataType;
            Data = data;
            Data2 = data2;
        }

        public EnumDataType DataType { set; get; }

        public T Data { set; get; }

        public T Data2 { set; get; }
    }

    public enum EnumDataType
    {
        [Description("没有参数")]
        No,
        [Description("浏览器加载完成,date=url")]
        Completed,
        [Description("Url发生变化,date=url")]
        Url,
        [Description("网页Title发生变化,date=title")]
        Title,
        [Description("拦截的URL，或是点击，拖动的,date=url")]
        NewWindow,
        [Description("去调用搜索,date=搜索的内容")]
        Search

    };
}
