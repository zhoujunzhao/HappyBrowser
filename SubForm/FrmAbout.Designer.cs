
using HappyBrowser.Properties;

namespace HappyBrowser.SubForm
{
    partial class FrmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            lblOs=new Label();
            lblCef=new Label();
            lbaChromium=new Label();
            lblChromiumVer=new Label();
            lblCefVer=new Label();
            lblOsVer=new Label();
            btnClose=new Button();
            SuspendLayout();
            // 
            // lblOs
            // 
            lblOs.AutoSize=true;
            lblOs.Location=new Point(18, 17);
            lblOs.Name="lblOs";
            lblOs.Size=new Size(52, 17);
            lblOs.TabIndex=1;
            lblOs.Text="OS Ver:";
            // 
            // lblCef
            // 
            lblCef.AutoSize=true;
            lblCef.Location=new Point(18, 65);
            lblCef.Name="lblCef";
            lblCef.Size=new Size(88, 17);
            lblCef.TabIndex=2;
            lblCef.Text="CefSharp Ver:";
            // 
            // lbaChromium
            // 
            lbaChromium.AutoSize=true;
            lbaChromium.Location=new Point(18, 113);
            lbaChromium.Name="lbaChromium";
            lbaChromium.Size=new Size(95, 17);
            lbaChromium.TabIndex=3;
            lbaChromium.Text="Chromium Ver:";
            // 
            // lblChromiumVer
            // 
            lblChromiumVer.AutoSize=true;
            lblChromiumVer.Location=new Point(113, 113);
            lblChromiumVer.Name="lblChromiumVer";
            lblChromiumVer.Size=new Size(95, 17);
            lblChromiumVer.TabIndex=6;
            lblChromiumVer.Text="Chromium Ver:";
            // 
            // lblCefVer
            // 
            lblCefVer.AutoSize=true;
            lblCefVer.Location=new Point(113, 65);
            lblCefVer.Name="lblCefVer";
            lblCefVer.Size=new Size(82, 17);
            lblCefVer.TabIndex=5;
            lblCefVer.Text="Cefshap Ver:";
            // 
            // lblOsVer
            // 
            lblOsVer.AutoSize=true;
            lblOsVer.Location=new Point(113, 17);
            lblOsVer.Name="lblOsVer";
            lblOsVer.Size=new Size(52, 17);
            lblOsVer.TabIndex=4;
            lblOsVer.Text="OS Ver:";
            // 
            // btnClose
            // 
            btnClose.Image=Resources.operate_exit_32;
            btnClose.Location=new Point(120, 165);
            btnClose.Name="btnClose";
            btnClose.Size=new Size(75, 23);
            btnClose.TabIndex=7;
            btnClose.Text="关 闭";
            btnClose.Click+=BtnClose_Click;
            // 
            // About
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(352, 215);
            Controls.Add(btnClose);
            Controls.Add(lblChromiumVer);
            Controls.Add(lblCefVer);
            Controls.Add(lblOsVer);
            Controls.Add(lbaChromium);
            Controls.Add(lblCef);
            Controls.Add(lblOs);
            FormBorderStyle=FormBorderStyle.FixedToolWindow;
            Name="About";
            ShowIcon=false;
            ShowInTaskbar=false;
            StartPosition=FormStartPosition.CenterParent;
            Text="关于";
            Load+=About_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblOs;
        private Label lblCef;
        private Label lbaChromium;
        private Label lblChromiumVer;
        private Label lblCefVer;
        private Label lblOsVer;
        private Button btnClose;
    }
}