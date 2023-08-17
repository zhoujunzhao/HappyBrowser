using HappyBrowser.Controls;
using HappyBrowser.Controls.BrowserTabStrip;

namespace HappyBrowser
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            ctlHeader=new CtlHeader();
            browserTabStrip=new BrowserTabStrip();
            tlpTopContainer=new TableLayoutPanel();
            ssTopStatus=new StatusStrip();
            ((System.ComponentModel.ISupportInitialize)browserTabStrip).BeginInit();
            tlpTopContainer.SuspendLayout();
            SuspendLayout();
            // 
            // ctlHeader
            // 
            ctlHeader.Dock=DockStyle.Fill;
            ctlHeader.Location=new Point(0, 1);
            ctlHeader.Margin=new Padding(0, 1, 0, 0);
            ctlHeader.Name="ctlHeader";
            ctlHeader.Size=new Size(803, 55);
            ctlHeader.TabIndex=1;
            ctlHeader.ForwardOrBackClick+=CtlHeader_ForwardOrBackClick;
            ctlHeader.UrlChanged+=Header_UrlChanged;
            ctlHeader.SearchChanged+=CtlHeader_SearchChanged;
            ctlHeader.OpenDownloadWindow+=CtlHeader_OpenDownloadWindow;
            // 
            // browserTabStrip
            // 
            browserTabStrip.Dock=DockStyle.Fill;
            browserTabStrip.Font=new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            browserTabStrip.Location=new Point(0, 56);
            browserTabStrip.Margin=new Padding(0);
            browserTabStrip.Name="browserTabStrip";
            browserTabStrip.Padding=new Padding(1, 1, 1, 30);
            browserTabStrip.Size=new Size(803, 519);
            browserTabStrip.TabIndex=2;
            browserTabStrip.Text="browserTabStrip1";
            browserTabStrip.TabStripItemSelectionChanged+=BrowserTabStrip_TabStripItemSelectionChanged;
            browserTabStrip.TabStripItemClosed+=BrowserTabStrip_TabStripItemClosed;
            browserTabStrip.AddTabClicked+=BrowserTabStrip_AddTabClicked;
            // 
            // tlpTopContainer
            // 
            tlpTopContainer.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            tlpTopContainer.ColumnCount=1;
            tlpTopContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTopContainer.Controls.Add(ctlHeader, 0, 0);
            tlpTopContainer.Controls.Add(browserTabStrip, 0, 1);
            tlpTopContainer.Location=new Point(0, 0);
            tlpTopContainer.Margin=new Padding(0);
            tlpTopContainer.Name="tlpTopContainer";
            tlpTopContainer.RowCount=2;
            tlpTopContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpTopContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTopContainer.Size=new Size(803, 575);
            tlpTopContainer.TabIndex=1;
            // 
            // ssTopStatus
            // 
            ssTopStatus.GripMargin=new Padding(1);
            ssTopStatus.Location=new Point(0, 571);
            ssTopStatus.Name="ssTopStatus";
            ssTopStatus.Size=new Size(803, 22);
            ssTopStatus.TabIndex=2;
            ssTopStatus.Text="statusStrip1";
            // 
            // FrmMain
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(803, 593);
            Controls.Add(ssTopStatus);
            Controls.Add(tlpTopContainer);
            Icon=(Icon)resources.GetObject("$this.Icon");
            Name="FrmMain";
            StartPosition=FormStartPosition.CenterScreen;
            Text="爱好者浏览器";
            WindowState=FormWindowState.Maximized;
            Load+=BrowserMain_Load;
            ((System.ComponentModel.ISupportInitialize)browserTabStrip).EndInit();
            tlpTopContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controls.CtlHeader ctlHeader;
        private Controls.BrowserTabStrip.BrowserTabStrip browserTabStrip;
        private TableLayoutPanel tlpTopContainer;
        private StatusStrip ssTopStatus;
    }
}