using CefSharp;
using HappyBrowser.Controls;

namespace HappyBrowser.SubForm
{
    public partial class FindWebContent : Form
    {
        CtlChromiumBrowser chromiumBrowser;
        public FindWebContent(CtlChromiumBrowser browser)
        {
            InitializeComponent();
            chromiumBrowser = browser;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (chromiumBrowser == null || string.IsNullOrEmpty(this.txtSearch.Text)) return;
            chromiumBrowser.Find(this.txtSearch.Text, true, false, true);
        }

        private void TsbFindDown_Click(object sender, EventArgs e)
        {
            if (chromiumBrowser == null || string.IsNullOrEmpty(this.txtSearch.Text)) return;
            chromiumBrowser.Find(this.txtSearch.Text, true, false, true);
        }

        private void TsbFindUp_Click(object sender, EventArgs e)
        {
            if (chromiumBrowser == null || string.IsNullOrEmpty(this.txtSearch.Text)) return;
            chromiumBrowser.Find(this.txtSearch.Text, false, false, true);
        }

        private void TsbClose_Click(object sender, EventArgs e)
        {
            chromiumBrowser.StopFinding(true);
            this.Close();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (chromiumBrowser == null || string.IsNullOrEmpty(this.txtSearch.Text)) return;
            if (e.KeyCode == Keys.Enter)
            {
                chromiumBrowser.Find(this.txtSearch.Text, true, false, true);
            }
        }
    }
}
