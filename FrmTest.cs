using HappyBrowser.Controls;
using HappyBrowser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyBrowser
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            ctlComboBox1.DisplayText = true;
            ctlComboBox1.Items.Add(new CtlComboBoxItem(Resources.ws_work_32, "工作", "work"));
            ctlComboBox1.Items.Add(new CtlComboBoxItem(Resources.ws_readbook_32, "阅读", "read"));
            ctlComboBox1.SelectedIndex = 0;

            ctlComboBox2.DisplayText = true;
            ctlComboBox2.Items.Add(new CtlComboBoxItem(Resources.ws_work_32, "工作", "work"));
            ctlComboBox2.Items.Add(new CtlComboBoxItem(Resources.ws_readbook_32, "阅读", "read"));
            ctlComboBox2.SelectedIndex = 0;

        }

        private void CtlComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("当前text：" + ctlComboBox1.SelectedText + "当前value：" + ctlComboBox1.SelectedValue);
        }
    }
}
