namespace HappyBrowser.Controls
{
    partial class CtlTextbox
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
            textBox1=new TextBox();
            button1=new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BorderStyle=BorderStyle.None;
            textBox1.Location=new Point(0, 0);
            textBox1.Name="textBox1";
            textBox1.Size=new Size(100, 16);
            textBox1.TabIndex=0;
            // 
            // button1
            // 
            button1.Location=new Point(100, 0);
            button1.Margin=new Padding(0);
            button1.Name="button1";
            button1.Size=new Size(23, 23);
            button1.TabIndex=1;
            button1.Text="A";
            button1.UseVisualStyleBackColor=true;
            // 
            // CtlTextbox
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            BorderStyle=BorderStyle.FixedSingle;
            Controls.Add(button1);
            Controls.Add(textBox1);
            Margin=new Padding(0);
            Name="CtlTextbox";
            Size=new Size(123, 23);
            Load+=CtlTextbox_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
    }
}
