using System;

namespace HappyBrowser.Controls.CtlEventArgs
{
    public class HeaderSearchChangedAgrs : System.EventArgs
    {
        public HeaderSearchChangedAgrs(string url)
        {
            this.Url = url;
        }

        public string Url { get; }

    }

}
