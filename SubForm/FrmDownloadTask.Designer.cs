namespace HappyBrowser.SubForm
{
    partial class FrmDownloadTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelControl1=new Label();
            labelControl2=new Label();
            labelControl3=new Label();
            btnDownload=new Button();
            btnCancel=new Button();
            txtFileName=new TextBox();
            txtDownUrl=new TextBox();
            txtSavePath=new TextBox();
            btnOpenDialog=new Button();
            fbdSelectPath=new FolderBrowserDialog();
            SuspendLayout();
            // 
            // labelControl1
            // 
            labelControl1.Font=new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl1.Location=new Point(8, 23);
            labelControl1.Name="labelControl1";
            labelControl1.Size=new Size(45, 18);
            labelControl1.TabIndex=0;
            labelControl1.Text="网址：";
            // 
            // labelControl2
            // 
            labelControl2.Font=new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl2.Location=new Point(8, 71);
            labelControl2.Name="labelControl2";
            labelControl2.Size=new Size(60, 18);
            labelControl2.TabIndex=1;
            labelControl2.Text="文件名：";
            // 
            // labelControl3
            // 
            labelControl3.Font=new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl3.Location=new Point(8, 118);
            labelControl3.Name="labelControl3";
            labelControl3.Size=new Size(60, 18);
            labelControl3.TabIndex=2;
            labelControl3.Text="下载到：";
            // 
            // btnDownload
            // 
            btnDownload.FlatStyle=FlatStyle.Popup;
            btnDownload.Location=new Point(283, 159);
            btnDownload.Name="btnDownload";
            btnDownload.Size=new Size(75, 30);
            btnDownload.TabIndex=3;
            btnDownload.Text="下 载";
            btnDownload.Click+=BtnDownload_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle=FlatStyle.Popup;
            btnCancel.Location=new Point(438, 159);
            btnCancel.Name="btnCancel";
            btnCancel.Size=new Size(75, 30);
            btnCancel.TabIndex=4;
            btnCancel.Text="取 消";
            btnCancel.Click+=BtnCancel_Click;
            // 
            // txtFileName
            // 
            txtFileName.Font=new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtFileName.Location=new Point(63, 68);
            txtFileName.Name="txtFileName";
            txtFileName.Size=new Size(490, 25);
            txtFileName.TabIndex=5;
            // 
            // txtDownUrl
            // 
            txtDownUrl.Font=new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtDownUrl.Location=new Point(63, 20);
            txtDownUrl.Name="txtDownUrl";
            txtDownUrl.ReadOnly=true;
            txtDownUrl.Size=new Size(490, 25);
            txtDownUrl.TabIndex=6;
            // 
            // txtSavePath
            // 
            txtSavePath.Font=new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtSavePath.Location=new Point(63, 115);
            txtSavePath.Name="txtSavePath";
            txtSavePath.ReadOnly=true;
            txtSavePath.Size=new Size(463, 25);
            txtSavePath.TabIndex=7;
            // 
            // btnOpenDialog
            // 
            btnOpenDialog.FlatStyle=FlatStyle.Popup;
            btnOpenDialog.Location=new Point(527, 115);
            btnOpenDialog.Name="btnOpenDialog";
            btnOpenDialog.Size=new Size(26, 24);
            btnOpenDialog.TabIndex=8;
            btnOpenDialog.Text="...";
            btnOpenDialog.Click+=BtnOpenDialog_Click;
            // 
            // DownloadTask
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(559, 201);
            Controls.Add(btnOpenDialog);
            Controls.Add(txtSavePath);
            Controls.Add(txtDownUrl);
            Controls.Add(txtFileName);
            Controls.Add(btnCancel);
            Controls.Add(btnDownload);
            Controls.Add(labelControl3);
            Controls.Add(labelControl2);
            Controls.Add(labelControl1);
            FormBorderStyle=FormBorderStyle.FixedToolWindow;
            Name="DownloadTask";
            ShowInTaskbar=false;
            StartPosition=FormStartPosition.CenterParent;
            Text="新建下载任务";
            TopMost=true;
            Load+=DownloadTask_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelControl1;
        private Label labelControl2;
        private Label labelControl3;
        private Button btnDownload;
        private Button btnCancel;
        private TextBox txtFileName;
        private TextBox txtDownUrl;
        private TextBox txtSavePath;
        private Button btnOpenDialog;
        private FolderBrowserDialog fbdSelectPath;
    }
}