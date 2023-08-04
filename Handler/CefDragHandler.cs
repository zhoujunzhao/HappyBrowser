using CefSharp.Enums;
using CefSharp;
using HappyBrowser.Controls;
using HappyBrowser.Services;

namespace HappyBrowser.Handler
{
    public class CefDragHandler : IDragHandler
    {

        public bool OnDragEnter(IWebBrowser browserControl, IBrowser browser, IDragData dragData, DragOperationsMask mask)
        {
            CtlChromiumBrowser ctl = (CtlChromiumBrowser)browserControl;
            DragDataService.Add(ctl.Name, new(dragData, Cursor.Position));
            return false;

        }

        public void OnDraggableRegionsChanged(IWebBrowser browserControl, IBrowser browser, IFrame frame, IList<DraggableRegion> regions)
        {
        }
        
    }
}
