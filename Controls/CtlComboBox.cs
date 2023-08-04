using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace HappyBrowser.Controls
{
    public class CtlComboBox : ComboBox
    {
        private bool displayText = true;
        private Size imageSize = new Size(32, 32);
        private Color backgroundColor = SystemColors.Control;

        public CtlComboBox() : base() 
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            //WM_PAINT = 0xf; 要求一个窗口重画自己,即Paint事件时
            //WM_CTLCOLOREDIT = 0x133;当一个编辑型控件将要被绘制时发送此消息给它的父窗口；
            //通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
            //windows消息值表,可参考:
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                IntPtr hDC = GetWindowDC(m.HWnd);
                if (hDC.ToInt32() == 0) //如果取设备上下文失败则返回
                {
                    return;
                }

                //建立Graphics对像
                Graphics g = Graphics.FromHdc(hDC);
                g.FillRectangle(new SolidBrush(backgroundColor), new Rectangle(0, 0, Width, Height));
                //g.Clear(Color.Transparent);

                //画下拉按钮
                //Point pA = new Point(Width - 20, Height / 2 - 3);
                //Point pB = pA + new Size(6, 6);
                //Point pC = pA + new Size(12, 0);
                //g.DrawLine(new Pen(Color.Red, 2), pA, pB);
                //g.DrawLine(new Pen(Color.Blue, 2), pB, pC);

                if (this.SelectedIndex > -1)
                {
                    //获得项图片,绘制图片
                    CtlComboBoxItem item = (CtlComboBoxItem)SelectedItem;
                    Image img = item.Img;

                    //图片绘制的区域
                    
                    int imgW = imageSize.Width > Width ? Width : imageSize.Width;
                    int imgH = imageSize.Height > Height ? Height : imageSize.Height;
                    int imgPosiX = 2;
                    int imgPosiY = (Height/2-imgH/2);
                    if (!this.displayText) 
                    {
                        imgPosiX = Width/2-imgW/2;
                    }
                    Rectangle imgRect = new Rectangle(imgPosiX, imgPosiY, imgW, imgH);
                    g.DrawImage(img, imgRect);
                    if (this.displayText)
                    {
                        Rectangle textRect = new Rectangle(imgRect.Right, imgRect.Top, Width - imgRect.Right, imgRect.Height + 2);
                        StringFormat strFormat = new StringFormat();
                        strFormat.LineAlignment = StringAlignment.Center;
                        g.DrawString(item.Text, this.Font, new SolidBrush(ForeColor), textRect, strFormat);
                    }
                }

                //g.DrawLine(new Pen(Brushes.Blue, 2), new PointF(this.Width - this.Height, 0), new PointF(this.Width - this.Height, this.Height));
                //释放DC 
                ReleaseDC(m.HWnd, hDC);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //base.OnDrawItem(e);
            if (e.Index<0) return;
            Rectangle borderRect = new Rectangle(1, e.Bounds.Y+1, e.Bounds.Width-2, e.Bounds.Height-2);
            //鼠标选中在这个项上
            if ((e.State & DrawItemState.Selected) != 0)
            {
                //渐变画刷
                LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(255, 251, 237),
                                                 Color.FromArgb(255, 236, 181), LinearGradientMode.Vertical);

                //SystemBrushes.Highlight
                //填充区域
                e.Graphics.FillRectangle(SystemBrushes.GradientActiveCaption, borderRect);

                //画边框
                Pen pen = new Pen(Color.FromArgb(229, 195, 101));
                //e.Graphics.DrawRectangle(pen, borderRect);
            }
            else
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                //e.Graphics.FillRectangle(brush, borderRect);
            }

            //获得项图片,绘制图片
            CtlComboBoxItem item = (CtlComboBoxItem)this.Items[e.Index];
            Image img = item.Img;

            
            int imgW = imageSize.Width > borderRect.Width ? borderRect.Width : imageSize.Width;
            int imgH = imageSize.Height > borderRect.Height ? borderRect.Height : imageSize.Height;
            int imgPosiY = (borderRect.Height/2-imgH/2);
            //图片绘制的区域
            Rectangle imgRect = new Rectangle(1, borderRect.Y + imgPosiY, imgW, imgH);
            e.Graphics.DrawImage(img, imgRect);

            if (this.displayText)
            {
                //获得项文本内容,绘制文本
                String itemText = this.Items[e.Index].ToString()!;
                //文本内容显示区域
                Rectangle textRect = new Rectangle(imgRect.Right + 1, imgRect.Top, e.Bounds.Width - imgRect.Width, e.Bounds.Height - 2);

                //文本格式垂直居中
                StringFormat strFormat = new StringFormat();
                strFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(itemText, this.Font, Brushes.Black, textRect, strFormat);
            }
            //e.DrawFocusRectangle();
        }

        [DefaultValue("")]
        public new string SelectedText
        {
            get
            {
                if (this.SelectedItem != null)
                {
                    CtlComboBoxItem item = (CtlComboBoxItem)this.SelectedItem;
                    return item.Text;
                }
                return "";
            }
        }

        [DefaultValue("")]
        public new string SelectedValue
        {
            get
            {
                if (this.SelectedItem != null)
                {
                    CtlComboBoxItem item = (CtlComboBoxItem)this.SelectedItem;
                    return item.Value;
                }
                return "";
            }
        }


        /// <summary>
		/// 是否显示文本
		/// true 始终显示
		/// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        public bool DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
            }
        }

        /// <summary>
		/// 是否显示文本
		/// true 始终显示
		/// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(Size), "32,32")]
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;
            }
        }

        public override Color BackColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
            }
        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }
}
