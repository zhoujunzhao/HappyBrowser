using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyBrowser.Controls
{
    public partial class CtlTextbox : UserControl
    {
        private Dictionary<string, Button> buttons = new();

        public CtlTextbox()
        {
            InitializeComponent();
        }

        private void CtlTextbox_Load(object sender, EventArgs e)
        {
            this.textBox1.AutoSize = false;
            this.textBox1.Height = this.Height;

        }

        public void AddButton(string name,string text,int width,Image? image=null)
        {
            if(buttons.ContainsKey(name)) return;

            Button button = new();
            button.Name = name;
            button.Text = text;
            if (width <=0)
                width = 23;
            button.Width = width;
            if(image != null)
            {
                button.Text = "";
                button.Image = image;
            }
            button.Click += (object? sender, EventArgs e)=> { 
            
            };
            buttons.Add(name,button);
        }

    }
}
