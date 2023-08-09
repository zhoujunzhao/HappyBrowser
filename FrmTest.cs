using HappyBrowser.Controls;
using HappyBrowser.Properties;
using HappyBrowser.Services;
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

            imageService = new AnimateImageService(Image.FromFile(@"D:\Downloads\loading.gif"));
            imageService.OnFrameChanged += new EventHandler<EventArgs>(imageService_OnFrameChanged);

            imageService2 = new AnimateImageService(Image.FromFile(@"D:\Downloads\loading.gif"));
            imageService2.OnFrameChanged += new EventHandler<EventArgs>(imageService2_OnFrameChanged);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

        }

        AnimateImageService imageService;

        AnimateImageService imageService2;

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

            imageService.Play();
            imageService2.Play();
        }

        void imageService_OnFrameChanged(object? sender, EventArgs e)
        {
            Invalidate();
        }

        void imageService2_OnFrameChanged(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void CtlComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("当前text：" + ctlComboBox1.SelectedText + "当前value：" + ctlComboBox1.SelectedValue);
        }

        private void FrmTest_Paint(object sender, PaintEventArgs e)
        {
            lock (imageService.Image)
            {
                Rectangle rect = new Rectangle(0,0,16,16);
                e.Graphics.DrawImage(imageService.Image, rect);
            }

            lock (imageService2.Image)
            {
                Rectangle rect = new Rectangle(0, 20, 16, 16);
                e.Graphics.DrawImage(imageService2.Image, rect);
            }
            // Create pen.
            //Pen blackPen = new Pen(Color.Gray, 6);

            //// Create coordinates of rectangle to bound ellipse.
            //int x = 10;//包裹圆弧矩形的左上角x,y 坐标
            //int y = 10;
            //int width = 100;//包裹矩形的大小
            //int height = 100;

            //// Create start and sweep angles on ellipse.
            //int startAngle = 0;//起始角
            //int sweepAngle = 90;//逆时针转过这么多

            //for (int i = 0; i<2000; i++)
            //{
            //    // Draw arc to screen.
            //    e.Graphics.DrawArc(new Pen(SystemColors.Control, 6), x, y, width, height, startAngle, sweepAngle);
            //    startAngle = startAngle + 2;

            //    if (startAngle > 360)
            //        startAngle = 0;
            //    e.Graphics.DrawArc(blackPen, x, y, width, height, startAngle, sweepAngle);
            //    Thread.Sleep(1);
            //}
        }
    }
}
