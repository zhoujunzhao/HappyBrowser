using HappyBrowser.Controls;
using HappyBrowser.Properties;

namespace HappyBrowser.SubForm
{
    partial class BrowserSet
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
            btnSave=new CtlButton();
            btnClose=new CtlButton();
            btnSaveClose=new CtlButton();
            labelControl1=new Label();
            txtRootConfigPath=new TextBox();
            btnSelRootConfigPath=new Button();
            fbdSelectPath=new FolderBrowserDialog();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Anchor=AnchorStyles.Bottom|AnchorStyles.Right;
            btnSave.BackgroundImage=Resources.operate_save_32;
            btnSave.BackgroundImageLayout=ImageLayout.Zoom;
            btnSave.ButtonImage=Resources.operate_save_32;
            btnSave.DisplayImage=true;
            btnSave.FlatStyle=FlatStyle.Popup;
            btnSave.ImageAlign=ContentAlignment.MiddleLeft;
            btnSave.Location=new Point(379, 415);
            btnSave.Name="btnSave";
            btnSave.Size=new Size(60, 23);
            btnSave.TabIndex=1;
            btnSave.Text="保 存";
            btnSave.TextAlign=ContentAlignment.MiddleRight;
            btnSave.Click+=BtnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor=AnchorStyles.Bottom|AnchorStyles.Right;
            btnClose.ButtonImage=Resources.operate_exit_32;
            btnClose.DisplayImage=true;
            btnClose.FlatStyle=FlatStyle.Popup;
            btnClose.Image=Resources.operate_exit_32;
            btnClose.Location=new Point(611, 415);
            btnClose.Name="btnClose";
            btnClose.Size=new Size(60, 23);
            btnClose.TabIndex=2;
            btnClose.Text="关 闭";
            btnClose.Click+=BtnClose_Click;
            // 
            // btnSaveClose
            // 
            btnSaveClose.Anchor=AnchorStyles.Bottom|AnchorStyles.Right;
            btnSaveClose.ButtonImage=Resources.operate_save_exit_32;
            btnSaveClose.DisplayImage=true;
            btnSaveClose.FlatStyle=FlatStyle.Popup;
            btnSaveClose.Image=Resources.operate_save_exit_32;
            btnSaveClose.Location=new Point(480, 415);
            btnSaveClose.Name="btnSaveClose";
            btnSaveClose.Size=new Size(90, 23);
            btnSaveClose.TabIndex=3;
            btnSaveClose.Text="保存并关闭";
            btnSaveClose.Click+=BtnSaveClose_Click;
            // 
            // labelControl1
            // 
            labelControl1.Location=new Point(12, 12);
            labelControl1.Name="labelControl1";
            labelControl1.Size=new Size(108, 14);
            labelControl1.TabIndex=4;
            labelControl1.Text="配置文件保存路径：";
            // 
            // txtRootConfigPath
            // 
            txtRootConfigPath.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            txtRootConfigPath.Location=new Point(118, 9);
            txtRootConfigPath.Name="txtRootConfigPath";
            txtRootConfigPath.ReadOnly=true;
            txtRootConfigPath.Size=new Size(530, 23);
            txtRootConfigPath.TabIndex=5;
            // 
            // btnSelRootConfigPath
            // 
            btnSelRootConfigPath.Anchor=AnchorStyles.Top|AnchorStyles.Right;
            btnSelRootConfigPath.FlatStyle=FlatStyle.Popup;
            btnSelRootConfigPath.Location=new Point(647, 9);
            btnSelRootConfigPath.Name="btnSelRootConfigPath";
            btnSelRootConfigPath.Size=new Size(25, 23);
            btnSelRootConfigPath.TabIndex=6;
            btnSelRootConfigPath.Text="...";
            btnSelRootConfigPath.Click+=BtnSelRootConfigPath_Click;
            // 
            // BrowserSet
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(684, 450);
            Controls.Add(btnSelRootConfigPath);
            Controls.Add(txtRootConfigPath);
            Controls.Add(labelControl1);
            Controls.Add(btnSaveClose);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            FormBorderStyle=FormBorderStyle.FixedToolWindow;
            Name="BrowserSet";
            ShowIcon=false;
            ShowInTaskbar=false;
            StartPosition=FormStartPosition.CenterParent;
            Text="浏览器设置";
            TopMost=true;
            Load+=BrowserSet_Load;
            Paint+=BrowserSet_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CtlButton btnSave;
        private CtlButton btnClose;
        private CtlButton btnSaveClose;
        private Label labelControl1;
        private TextBox txtRootConfigPath;
        private Button btnSelRootConfigPath;
        private FolderBrowserDialog fbdSelectPath;
        private Label label1;
    }
}