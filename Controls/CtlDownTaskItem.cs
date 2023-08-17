using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace HappyBrowser.Controls
{
    public partial class CtlDownTaskItem : UserControl
    {
        private string taskId;
        private string savePath;
        private CtlChromiumBrowser webBrowser;
        private CtlProgressBarText progressBarText;
        private IDownloadItemCallback? downloadItemCallback;

        public CtlDownTaskItem(string taskId, CtlChromiumBrowser browser)
        {
            progressBarText = new();

            this.taskId = taskId;
            this.savePath = "";
            this.webBrowser = browser;

            InitializeComponent();

            progressBarText.Font = Font;
            progressBarText.Text = "0%";
            progressBarText.ForeColor = Color.Blue;
            progressBarText.Control = progressBar1;
        }

        public int ProgressValue
        {
            set
            {
                this.Invoke(new Action(() =>
                {
                    this.progressBar1.Value = value;
                    progressBarText.Text = ((float)((float)progressBar1.Value / (float)progressBar1.Maximum) * 100).ToString() + "%";
                    Debug.WriteLine("界面更新进度：" + this.progressBar1.Value);
                    Application.DoEvents();
                }));
            }
        }

        public IDownloadItemCallback? DownloadItemCallback {
            get { return this.downloadItemCallback; }
            set {
                this.downloadItemCallback ??= value;
            }
        }

        public string FileName
        {
            set
            {
                this.lblFileName.Text = value;
            }
        }

        public string SavePath
        {
            set
            {
                this.savePath = value;
            }
        }

        public CtlChromiumBrowser WebBrowser
        { get { return webBrowser; } }

        private void TsbStart_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            if (btn.Tag.ToString() == "1")//下载中，切换为暂停
            {
                btn.Text = "开始";
                btn.Tag="0";

                this.downloadItemCallback?.Pause();
            }
            else if (btn.Tag.ToString() == "0")//暂停中，切换为下载中
            {
                btn.Text = "继续";
                btn.Tag="1";
                this.downloadItemCallback?.Resume();
            }

        }

        private void TsbCancel_Click(object sender, EventArgs e)
        {
            if (NotifyUtil.Confirm("确实要取消正在下载的任务吗？") == DialogResult.OK)
            {
                this.downloadItemCallback?.Cancel();
            }
        }
    }
}
