namespace HappyBrowser.Controls
{
    partial class CtlDownTaskItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlDownTaskItem));
            lblFileName=new LinkLabel();
            progressBar1=new ProgressBar();
            toolStrip1=new ToolStrip();
            tsbStart=new ToolStripButton();
            tsbCancel=new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblFileName
            // 
            lblFileName.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            lblFileName.Location=new Point(1, 0);
            lblFileName.Name="lblFileName";
            lblFileName.Size=new Size(400, 25);
            lblFileName.TabIndex=0;
            lblFileName.TabStop=true;
            lblFileName.Text="linkLabel1";
            lblFileName.TextAlign=ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            progressBar1.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            progressBar1.Location=new Point(1, 25);
            progressBar1.Name="progressBar1";
            progressBar1.Size=new Size(400, 25);
            progressBar1.Style=ProgressBarStyle.Continuous;
            progressBar1.TabIndex=1;
            // 
            // toolStrip1
            // 
            toolStrip1.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Right;
            toolStrip1.AutoSize=false;
            toolStrip1.Dock=DockStyle.None;
            toolStrip1.GripMargin=new Padding(0);
            toolStrip1.GripStyle=ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbStart, tsbCancel });
            toolStrip1.Location=new Point(400, 0);
            toolStrip1.Name="toolStrip1";
            toolStrip1.Size=new Size(110, 50);
            toolStrip1.TabIndex=2;
            toolStrip1.Text="toolStrip1";
            // 
            // tsbStart
            // 
            tsbStart.DisplayStyle=ToolStripItemDisplayStyle.Text;
            tsbStart.Image=(Image)resources.GetObject("tsbStart.Image");
            tsbStart.ImageTransparentColor=Color.Magenta;
            tsbStart.Name="tsbStart";
            tsbStart.Size=new Size(36, 47);
            tsbStart.Tag="1";
            tsbStart.Text="暂停";
            tsbStart.Click+=TsbStart_Click;
            // 
            // tsbCancel
            // 
            tsbCancel.DisplayStyle=ToolStripItemDisplayStyle.Text;
            tsbCancel.Image=(Image)resources.GetObject("tsbCancel.Image");
            tsbCancel.ImageTransparentColor=Color.Magenta;
            tsbCancel.Name="tsbCancel";
            tsbCancel.Size=new Size(36, 47);
            tsbCancel.Text="取消";
            tsbCancel.Click+=TsbCancel_Click;
            // 
            // CtlDownTaskItem
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            Controls.Add(toolStrip1);
            Controls.Add(progressBar1);
            Controls.Add(lblFileName);
            Margin=new Padding(1);
            Name="CtlDownTaskItem";
            Size=new Size(510, 50);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private LinkLabel lblFileName;
        private ProgressBar progressBar1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbStart;
        private ToolStripButton tsbCancel;
    }
}
