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
            linkLabel1=new LinkLabel();
            progressBar1=new ProgressBar();
            SuspendLayout();
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize=true;
            linkLabel1.Location=new Point(48, 0);
            linkLabel1.Name="linkLabel1";
            linkLabel1.Size=new Size(66, 17);
            linkLabel1.TabIndex=0;
            linkLabel1.TabStop=true;
            linkLabel1.Text="linkLabel1";
            // 
            // progressBar1
            // 
            progressBar1.Location=new Point(130, 3);
            progressBar1.Name="progressBar1";
            progressBar1.Size=new Size(242, 23);
            progressBar1.TabIndex=1;
            // 
            // CtlDownTaskItem
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            Controls.Add(progressBar1);
            Controls.Add(linkLabel1);
            Name="CtlDownTaskItem";
            Size=new Size(745, 40);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel linkLabel1;
        private ProgressBar progressBar1;
    }
}
