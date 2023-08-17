using HappyBrowser.Services;

namespace HappyBrowser.SubForm
{
    public partial class FrmDownloadTask : Form
    {
        public FrmDownloadTask()
        {
            InitializeComponent();
        }

        public FrmDownloadTask(string downUrl, string fileName)
        {
            InitializeComponent();
            this.txtDownUrl.Text = downUrl;
            this.txtFileName.Text = fileName;
        }

        public string FileName
        {
            get
            {
                return this.txtFileName.Text;
            }
        }

        public string SavePath
        {
            get
            {
                return this.txtSavePath.Text;
            }
        }

        private void DownloadTask_Load(object sender, EventArgs e)
        {
            string hisSavePath = ConfigService.GetDownloadPath();
            if (string.IsNullOrEmpty(hisSavePath))
            {
                hisSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            this.txtSavePath.Text= hisSavePath;
            this.fbdSelectPath.SelectedPath = hisSavePath;
        }

        private void BtnOpenDialog_Click(object sender, EventArgs e)
        {
            if (fbdSelectPath.ShowDialog(this) == DialogResult.OK)
            {
                this.txtSavePath.Text = this.fbdSelectPath.SelectedPath;
            }

        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFileName.Text))
            {
                NotifyUtil.Warn("文件名不能为空");
                this.txtFileName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.txtSavePath.Text))
            {
                NotifyUtil.Warn("请选择保存路径。");
                this.btnDownload.Focus();
                return;
            }
            if (!System.IO.Directory.Exists(this.txtSavePath.Text))
            {
                NotifyUtil.Warn("保存路径不正确，请重新选择。");
                this.btnDownload.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
