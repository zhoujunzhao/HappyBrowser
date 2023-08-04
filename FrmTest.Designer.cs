using HappyBrowser.Controls;

namespace HappyBrowser
{
    partial class FrmTest
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
            ctlComboBox1=new CtlComboBox();
            comboBox1=new ComboBox();
            ctlTextbox1=new CtlTextbox();
            textBox1=new TextBox();
            ctlComboBox2=new CtlComboBox();
            ctlSearchBox1=new CtlSearchBox();
            button1=new Button();
            button2=new Button();
            ctlButton1=new CtlButton();
            button3=new Button();
            ctlButton2=new CtlButton();
            ctlButton3=new CtlButton();
            SuspendLayout();
            // 
            // ctlComboBox1
            // 
            ctlComboBox1.DrawMode=DrawMode.OwnerDrawFixed;
            ctlComboBox1.DropDownStyle=ComboBoxStyle.DropDownList;
            ctlComboBox1.FormattingEnabled=true;
            ctlComboBox1.ImageSize=new Size(18, 18);
            ctlComboBox1.ItemHeight=22;
            ctlComboBox1.Location=new Point(201, 54);
            ctlComboBox1.Margin=new Padding(0);
            ctlComboBox1.Name="ctlComboBox1";
            ctlComboBox1.Size=new Size(60, 28);
            ctlComboBox1.TabIndex=0;
            ctlComboBox1.SelectedValueChanged+=CtlComboBox1_SelectedValueChanged;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle=ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled=true;
            comboBox1.Items.AddRange(new object[] { "aaaa", "bbbb", "cccccc" });
            comboBox1.Location=new Point(150, 170);
            comboBox1.Name="comboBox1";
            comboBox1.Size=new Size(121, 25);
            comboBox1.TabIndex=1;
            // 
            // ctlTextbox1
            // 
            ctlTextbox1.BorderStyle=BorderStyle.FixedSingle;
            ctlTextbox1.Location=new Point(329, 300);
            ctlTextbox1.Margin=new Padding(0);
            ctlTextbox1.Name="ctlTextbox1";
            ctlTextbox1.Size=new Size(123, 23);
            ctlTextbox1.TabIndex=2;
            // 
            // textBox1
            // 
            textBox1.Font=new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location=new Point(323, 261);
            textBox1.Name="textBox1";
            textBox1.Size=new Size(100, 24);
            textBox1.TabIndex=3;
            // 
            // ctlComboBox2
            // 
            ctlComboBox2.DrawMode=DrawMode.OwnerDrawFixed;
            ctlComboBox2.DropDownStyle=ComboBoxStyle.DropDownList;
            ctlComboBox2.FormattingEnabled=true;
            ctlComboBox2.ItemHeight=32;
            ctlComboBox2.Location=new Point(302, 54);
            ctlComboBox2.Margin=new Padding(0);
            ctlComboBox2.Name="ctlComboBox2";
            ctlComboBox2.Size=new Size(80, 38);
            ctlComboBox2.TabIndex=4;
            // 
            // ctlSearchBox1
            // 
            ctlSearchBox1.BorderStyle=BorderStyle.FixedSingle;
            ctlSearchBox1.Location=new Point(403, 158);
            ctlSearchBox1.Margin=new Padding(0);
            ctlSearchBox1.Name="ctlSearchBox1";
            ctlSearchBox1.Size=new Size(200, 26);
            ctlSearchBox1.TabIndex=5;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize=0;
            button1.FlatStyle=FlatStyle.Flat;
            button1.Image=Properties.Resources.search_clean_16;
            button1.Location=new Point(41, 356);
            button1.Margin=new Padding(0);
            button1.Name="button1";
            button1.Size=new Size(18, 23);
            button1.TabIndex=6;
            button1.UseVisualStyleBackColor=true;
            // 
            // button2
            // 
            button2.FlatAppearance.BorderSize=0;
            button2.FlatStyle=FlatStyle.Flat;
            button2.Image=Properties.Resources.search_find_16;
            button2.Location=new Point(58, 356);
            button2.Margin=new Padding(0);
            button2.Name="button2";
            button2.Size=new Size(18, 23);
            button2.TabIndex=7;
            button2.UseVisualStyleBackColor=true;
            // 
            // ctlButton1
            // 
            ctlButton1.ButtonImage=Properties.Resources.operate_save_32;
            ctlButton1.DisplayImage=true;
            ctlButton1.Image=Properties.Resources.folder_16;
            ctlButton1.Location=new Point(586, 261);
            ctlButton1.Name="ctlButton1";
            ctlButton1.Size=new Size(72, 23);
            ctlButton1.TabIndex=8;
            ctlButton1.Text="Button3";
            ctlButton1.UseVisualStyleBackColor=true;
            // 
            // button3
            // 
            button3.Location=new Point(575, 376);
            button3.Name="button3";
            button3.Size=new Size(75, 23);
            button3.TabIndex=9;
            button3.Text="button3";
            button3.UseVisualStyleBackColor=true;
            // 
            // ctlButton2
            // 
            ctlButton2.ButtonImage=Properties.Resources.operate_save_32;
            ctlButton2.ButtonImagePosition=EnumButtonImagePosition.Right;
            ctlButton2.DisplayImage=true;
            ctlButton2.Location=new Point(586, 300);
            ctlButton2.Name="ctlButton2";
            ctlButton2.Size=new Size(72, 23);
            ctlButton2.TabIndex=10;
            ctlButton2.Text="Button2";
            ctlButton2.UseVisualStyleBackColor=true;
            // 
            // ctlButton3
            // 
            ctlButton3.Location=new Point(586, 232);
            ctlButton3.Name="ctlButton3";
            ctlButton3.Size=new Size(80, 23);
            ctlButton3.TabIndex=11;
            ctlButton3.Text="ctlButton3";
            ctlButton3.UseVisualStyleBackColor=true;
            // 
            // FrmTest
            // 
            AutoScaleDimensions=new SizeF(7F, 17F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(800, 450);
            Controls.Add(ctlButton3);
            Controls.Add(ctlButton2);
            Controls.Add(button3);
            Controls.Add(ctlButton1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(ctlSearchBox1);
            Controls.Add(ctlComboBox2);
            Controls.Add(textBox1);
            Controls.Add(ctlTextbox1);
            Controls.Add(comboBox1);
            Controls.Add(ctlComboBox1);
            Name="FrmTest";
            Text="FrmTest";
            Load+=FrmTest_Load;
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Controls.CtlComboBox ctlComboBox1;
        private ComboBox comboBox1;
        private CtlTextbox ctlTextbox1;
        private TextBox textBox1;
        private CtlComboBox ctlComboBox2;
        private CtlSearchBox ctlSearchBox1;
        private Button button1;
        private Button button2;
        private CtlButton ctlButton1;
        private Button button3;
        private CtlButton ctlButton2;
        private CtlButton ctlButton3;
    }
}