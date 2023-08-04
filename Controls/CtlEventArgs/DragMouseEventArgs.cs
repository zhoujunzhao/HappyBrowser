using CefSharp;
using System.ComponentModel;
using DragDataEntity = HappyBrowser.Entity.DragDataEntity;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class DragMouseEventArgs : MouseEventArgs
    {
        public DragMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        : base(button, clicks, x, y, delta)
        {
            this.DragData=null;
            this.DragDirection = EnumDragDirection.No;
        }

        public DragMouseEventArgs(MouseEventArgs mouseEventArgs, DragDataEntity? dragData,EnumDragDirection dragDirection)
        : base(mouseEventArgs.Button, mouseEventArgs.Clicks, mouseEventArgs.X, mouseEventArgs.Y, mouseEventArgs.Delta)
        {
            this.DragData=dragData;
            this.DragDirection = dragDirection;
        }

        public EnumDragDirection DragDirection { get; set; }
        public DragDataEntity? DragData { get; set; }

    }
    /// <summary>
    /// 拖动方向
    /// </summary>
    public enum EnumDragDirection
    {
        [Description("没动位置")]
        No,
        [Description("左")]
        Left,
        [Description("左上")]
        LeftUp,
        [Description("上")]
        Up,
        [Description("右上")]
        RightUp,
        [Description("右")]
        Right,
        [Description("右下")]
        RightDown,
        [Description("下")]
        Down,
        [Description("左下")]
        LeftDown

    };
}
