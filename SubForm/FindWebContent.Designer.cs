namespace HappyBrowser.SubForm
{
    partial class FindWebContent
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
            ToolStripSeparator toolStripSeparator2;
            txtSearch=new TextBox();
            toolStrip1=new ToolStrip();
            tsbFindDown=new ToolStripButton();
            tsbFindUp=new ToolStripButton();
            toolStripSeparator1=new ToolStripSeparator();
            tsbClose=new ToolStripButton();
            toolStripSeparator2=new ToolStripSeparator();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.AutoSize=false;
            toolStripSeparator2.Name="toolStripSeparator2";
            toolStripSeparator2.Overflow=ToolStripItemOverflow.Never;
            toolStripSeparator2.Size=new Size(6, 35);
            // 
            // txtAddress
            // 
            txtSearch.Location=new Point(0, 0);
            txtSearch.Name="txtAddress";
            txtSearch.AutoSize=false;
            txtSearch.Size=new Size(200, 30);
            txtSearch.TabIndex=0;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.KeyDown+=TxtSearch_KeyDown;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize=false;
            toolStrip1.Dock=DockStyle.None;
            toolStrip1.GripMargin=new Padding(0);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripSeparator2, tsbFindDown, tsbFindUp, toolStripSeparator1, tsbClose });
            toolStrip1.LayoutStyle=ToolStripLayoutStyle.Flow;
            toolStrip1.Location=new Point(200, 0);
            toolStrip1.Name="toolStrip1";
            toolStrip1.Size=new Size(110, 30);
            toolStrip1.TabIndex=1;
            toolStrip1.Text="toolStrip1";
            // 
            // tsbFindDown
            // 
            tsbFindDown.AutoSize=false;
            tsbFindDown.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbFindDown.Image=Properties.Resources.arrow_down_32;
            tsbFindDown.ImageTransparentColor=Color.Magenta;
            tsbFindDown.Name="tsbFindDown";
            tsbFindDown.Size=new Size(30, 30);
            tsbFindDown.Text="向下查找";
            tsbFindDown.Click+=TsbFindDown_Click;
            // 
            // tsbFindUp
            // 
            tsbFindUp.AutoSize=false;
            tsbFindUp.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbFindUp.Image=Properties.Resources.arrow_up_32;
            tsbFindUp.ImageTransparentColor=Color.Magenta;
            tsbFindUp.Name="tsbFindUp";
            tsbFindUp.Size=new Size(30, 30);
            tsbFindUp.Text="向上查找";
            tsbFindUp.Click+=TsbFindUp_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.AutoSize=false;
            toolStripSeparator1.Name="toolStripSeparator1";
            toolStripSeparator1.Size=new Size(6, 35);
            // 
            // tsbClose
            // 
            tsbClose.AutoSize=false;
            tsbClose.DisplayStyle=ToolStripItemDisplayStyle.Image;
            tsbClose.Image=Properties.Resources.arrow_close_32;
            tsbClose.ImageTransparentColor=Color.Magenta;
            tsbClose.Name="tsbClose";
            tsbClose.Size=new Size(30, 30);
            tsbClose.Text="关闭";
            tsbClose.ToolTipText="关闭查找窗口";
            tsbClose.Click+=TsbClose_Click;
            // 
            // FindWebContent
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(310, 30);
            ControlBox=false;
            Controls.Add(toolStrip1);
            Controls.Add(txtSearch);
            FormBorderStyle=FormBorderStyle.None;
            Name="FindWebContent";
            Opacity=0.8D;
            ShowIcon=false;
            ShowInTaskbar=false;
            StartPosition=FormStartPosition.Manual;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private TextBox txtSearch;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbFindDown;
        private ToolStripButton tsbFindUp;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbClose;
        private ToolStripSeparator toolStripSeparator2;
    }
}