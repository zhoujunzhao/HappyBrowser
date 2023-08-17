using HappyBrowser.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyBrowser.SubForm
{
    public partial class FrmDownloadTaskList : Form
    {
        private Dictionary<string, CtlDownTaskItem> downTasks;
        public FrmDownloadTaskList(Dictionary<string, CtlDownTaskItem> downTasks)
        {
            InitializeComponent();
            this.downTasks = downTasks;
        }

        private void DownloadTaskList_Load(object sender, EventArgs e)
        {
            this.RefreshList();
        }

        /// <summary>
        /// 刷新全部下载列表
        /// </summary>
        public void RefreshList()
        {
            this.pnlContainer.Controls.Clear();
            int posiY = 1;
            foreach (CtlDownTaskItem itm in downTasks.Values)
            {
                itm.Location = new Point(1, posiY);
                itm.Size = new Size(this.pnlContainer.Width, 50);
                itm.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                this.pnlContainer.Controls.Add(itm);
                posiY = posiY + itm.Size.Height + 1;
            }
        }
    }
}
