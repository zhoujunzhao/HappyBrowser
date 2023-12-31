﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HappyBrowser.Controls
{
    internal class CtlProgressBarText : NativeWindow
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


        private Control? control;
        private string text = "null";
        private Color foreColor = SystemColors.ControlText;
        private Font? font = SystemFonts.MenuFont;

        public Font Font
        {
            set
            {
                font = value;
                if (control == null)
                {
                    return;
                }

                control.Invalidate();
            }
        }

        public Color ForeColor
        {
            set
            {
                foreColor = value;

                if (control == null)
                {
                    return;
                }

                control.Invalidate();
            }
        }

        public string Text
        {
            set
            {
                text = value;

                if (control == null)
                {
                    return;
                }

                control.Invalidate();
            }
        }

        public Control Control
        {
            set
            {
                control = value;
                if (control == null)
                {
                    return;
                }

                AssignHandle(control.Handle);
                control.Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            base.WndProc(ref m);
            if (control == null)
            {
                return;
            }

            switch (m.Msg)
            {
                case WM_PAINT:
                    IntPtr vDC = GetWindowDC(m.HWnd);
                    Graphics vGraphics = Graphics.FromHdc(vDC);
                    StringFormat vStringFormat = new();
                    vStringFormat.Alignment = StringAlignment.Center;
                    vStringFormat.LineAlignment = StringAlignment.Center;
                    vGraphics.DrawString(text, font, new SolidBrush(foreColor),
                        new Rectangle(0, 0, control.Width, control.Height), vStringFormat);
                    ReleaseDC(m.HWnd, vDC);
                    break;
            }
        }
    }
}
