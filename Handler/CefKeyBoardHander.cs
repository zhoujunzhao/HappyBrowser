using CefSharp;

namespace HappyBrowser.Handler
{
    public class CefKeyBoardHander : IKeyboardHandler
    {
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            return false;
        }

        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            const int VK_F5 = 0x74;
            const int VK_F11 = 0x7A;
            const int VK_F12 = 0x7B;
            if (windowsKeyCode == VK_F5)
            {
                browserControl.Reload(true);
            }
            else if (windowsKeyCode == VK_F11)
            {
                //browserControl.
                //browser.
            }
            else if (windowsKeyCode == VK_F12)
            {
                browserControl.ShowDevTools();
            }

            return false;
        }

    }
}
