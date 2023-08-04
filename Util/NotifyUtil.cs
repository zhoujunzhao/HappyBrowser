
namespace HappyBrowser
{
    public class NotifyUtil
    {
        public static void Success(string content, string title = "提示", IWin32Window? owner=null, int autoClose = 3000)
        {
            MessageBox.Show(owner, content, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static void Warn(string content, string title = "提示", IWin32Window? owner = null, int autoClose = 3000)
        {
            MessageBox.Show(owner, content, title, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        public static void Error(string content, string title = "提示", IWin32Window? owner = null, int autoClose = 3000)
        {
            MessageBox.Show(owner, content, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Confirm(string content, string title = "确认", IWin32Window? owner = null, int autoClose = 6000)
        {
            return MessageBox.Show(owner, content, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

    }
}
