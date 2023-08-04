using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class HeaderForwardOrBackClickAgrs
    {
        public HeaderForwardOrBackClickAgrs(EnumBrowserAction forwardOrBack)
        {
            this.ForwardOrBack = forwardOrBack;
        }

        public EnumBrowserAction ForwardOrBack { get; }
    }

    /// <summary>
    /// 是否打开新窗口
    /// </summary>
    public enum EnumBrowserAction
    {
        [Description("向前")]
        Forward,
        [Description("后退")]
        Back,
        [Description("刷新")]
        Refresh
    };
}
