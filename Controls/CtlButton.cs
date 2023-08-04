using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace HappyBrowser.Controls
{
    public class CtlButton: Button
    {
        private float paddingWidth = 4f;
        /// <summary>
        /// 按钮图片
        /// </summary>
        private Image? buttonImage;

        private Size buttonImageSize = new (16,16);

        private EnumButtonImagePosition buttonImagePosition = EnumButtonImagePosition.Left;

        /// <summary>
        /// 是否显示按钮图片
        /// </summary>
        private bool displayImage = false;

        /// <summary>
        /// 当前是否为下沉状态
        /// </summary>
        private bool isDownning = false;
        /// <summary>
        /// 鼠标是否指在控件上
        /// </summary>
        private bool isEnter = false;
        /// <summary>
        /// 可用按钮图片
        /// </summary>
        //private Image enabledImage = null;
        /// <summary>
        /// 不可用按钮图片
        /// </summary>
        //private Image noEnabledImage = null;

        public CtlButton()
        {
            
        }

        /// <summary>
        /// 按钮图片
        /// 不能使用原来的Image，因为无法阻止Image的原来输出
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(null)]
        public Image? ButtonImage 
        { 
            get { return buttonImage; }
            set { buttonImage = value; }
        }

        /// <summary>
        /// 指定按钮图片尺寸
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(Size), "16,16")]
        public Size ButtonImageSize
        {
            get { return buttonImageSize; }
            set { buttonImageSize = value; }
        }

        /// <summary>
        /// 按钮图片位置
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(EnumButtonImagePosition.Left)]
        public EnumButtonImagePosition ButtonImagePosition
        {
            get { return buttonImagePosition; }
            set { buttonImagePosition = value; }
        }

        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        public bool DisplayImage
        {
            get { return displayImage; }
            set { displayImage = value; }
        }

        #region 重绘
        /// <summary>
        /// 重绘事件
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //enabledImage = new Bitmap(this.Width, this.Height);
            //Graphics g = Graphics.FromImage(enabledImage);
            DrawBackGroundImg(e.Graphics);
            //noEnabledImage = enabledImage;
            //if (this.Enabled)
            //    pevent.Graphics.DrawImage(enabledImage, 0, 0, this.Width, this.Height);
            //else
            //    pevent.Graphics.DrawImage(noEnabledImage, 0, 0, this.Width, this.Height);

            this.Cursor = Cursors.Hand;
        }
        /// <summary>
        /// 绘制基本图片
        /// </summary>
        /// <param name="g">画布</param>
        private void DrawBackGroundImg(Graphics g)
        {
            g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            g.FillRectangle(new SolidBrush(Color.Black), rect);
            DrawHighlight(g, isDownning);

            DrawImageAndText(g, isDownning);
        }
        /// <summary>
        /// 绘制渐变背景
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="xiacen">是否下沉</param>
        private void DrawHighlight(Graphics g, bool isdownning)
        {
            try
            {
                RectangleF rect = this.ClientRectangle;
                rect.Height = rect.Height - 1.0f;
                rect.Width = rect.Width - 1.0f;
                g.FillRectangle(new LinearGradientBrush(rect, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, this.BackColor), LinearGradientMode.Vertical), rect);
                if (isEnter)
                {
                    g.Clear(Color.Black);
                    int a = (this.BackColor.A - 5) > 255 ? 255 : (this.BackColor.A - 5);
                    int green = (this.BackColor.G - 5) > 255 ? 255 : (this.BackColor.G - 5);
                    int b = (this.BackColor.B - 5) > 255 ? 255 : (this.BackColor.B - 5);
                    int r = (this.BackColor.R - 5) > 255 ? 255 : (this.BackColor.R - 5);
                    Color heightColor = Color.FromArgb(a, r, green, b);
                    g.FillRectangle(new LinearGradientBrush(rect, Color.FromArgb(255, 255, 255, 255), heightColor, LinearGradientMode.Vertical), rect);
                }
                if (isDownning)
                {
                    g.Clear(Color.Black);
                    int a = (this.BackColor.A - 10) < 0 ? 0 : (this.BackColor.A - 10);
                    int green = (this.BackColor.G - 10) < 0 ? 0 : (this.BackColor.G - 10);
                    int b = (this.BackColor.B - 10) < 0 ? 0 : (this.BackColor.B - 10);
                    int r = (this.BackColor.R - 10) < 0 ? 0 : (this.BackColor.R - 10);
                    Color heightColor = Color.FromArgb(a, r, green, b);
                    g.FillRectangle(new LinearGradientBrush(rect, Color.FromArgb(255, 255, 255, 255), heightColor, LinearGradientMode.Vertical), rect);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取高亮绘制区
        /// </summary>
        /// <param name="rect">基础区</param>
        /// <returns>高亮占区</returns>
        protected GraphicsPath GetDrawHighlightRect(Rectangle rect)
        {
            GraphicsPath ClientPath = new GraphicsPath();
            if (rect.Width <= 0)
            {
                rect.Width = 1;
            }
            if (rect.Height <= 0)
            {
                rect.Height = 1;
            }
            ClientPath.AddRectangle(rect);
            ClientPath.CloseFigure();
            return ClientPath;
        }

        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="isDownning">当前是否下沉</param>
        private void DrawImageAndText(Graphics g, bool isDownning)
        {
            RectangleF rectIcon = new(paddingWidth, ((this.Height - this.buttonImageSize.Height) / 2) + 0.5f, buttonImageSize.Width, buttonImageSize.Height);
            RectangleF rectText = this.ClientRectangle;
            if (this.displayImage && buttonImage != null)
            {
                if (this.buttonImagePosition == EnumButtonImagePosition.Right)
                {
                    rectIcon.X = this.Width - paddingWidth - buttonImageSize.Width;
                    rectText.X = paddingWidth;
                    rectText.Width = rectText.Width - rectIcon.Width;
                }
                else
                {
                    rectText.X = paddingWidth + rectIcon.Width;
                    rectText.Width = rectText.Width - rectText.X;
                }
                g.DrawImage(buttonImage, rectIcon);
            }
            else
            {
                rectText.X = paddingWidth;
                rectText.Width = rectText.Width - rectText.X;
            }

            StringFormat stringFormat = new();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            Font font = this.Font;
            g.DrawString(this.Text, font, new SolidBrush(this.ForeColor), rectText, stringFormat);
        }

        #endregion 重绘

        #region 重写各个事件
        protected override void OnMouseEnter(EventArgs e)
        {
            isEnter = true;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            isEnter = false;
            isDownning = false;
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            isDownning = true;
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            isDownning = false;
            base.OnMouseUp(mevent);
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            //if (this.Enabled)
            //{
            //    this.BackgroundImage = enabledImage;
            //}
            //else
            //{
            //    this.BackgroundImage = noEnabledImage;
            //}
        }
        #endregion 重写各个事件

        #region 辅助方法
        /// <summary> 
        /// 彩色图片转化为黑白 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        //public static Bitmap ConvertToGrayscale(Bitmap bitmap)
        //{
        //    Bitmap bm = new Bitmap(bitmap.Width, bitmap.Height);
        //    for (int y = 0; y < bm.Height; y)
        //    {
        //        for (int x = 0; x < bm.Width; x)
        //        {
        //            Color c = bitmap.GetPixel(x, y);
        //            int rgb = (int)(c.R * 0.3   c.G * 0.59   c.B * 0.11);
        //             bm.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
        //         }
        //    }
        //    return bm;
        //}

        #endregion
    }

    /// <summary>
    /// 按钮图片显示位置
    /// </summary>
    public enum EnumButtonImagePosition
    {
        Left,
        Right
    }
}
