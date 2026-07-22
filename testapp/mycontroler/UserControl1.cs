using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            int BorderRadius = 20;
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen borderPen = new Pen(Color.Red, 2);
            Rectangle borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
            int arcWidth = BorderRadius * 2;

            // 绘制圆角边框
            g.DrawArc(borderPen, borderRect.Left, borderRect.Top, arcWidth, arcWidth, 180, 90);
            g.DrawLine(borderPen, borderRect.Left + BorderRadius, borderRect.Top, borderRect.Right - BorderRadius, borderRect.Top);
            g.DrawArc(borderPen, borderRect.Right - arcWidth, borderRect.Top, arcWidth, arcWidth, 270, 90);
            g.DrawLine(borderPen, borderRect.Right, borderRect.Top + BorderRadius, borderRect.Right, borderRect.Bottom - BorderRadius);
            g.DrawArc(borderPen, borderRect.Right - arcWidth, borderRect.Bottom - arcWidth, arcWidth, arcWidth, 0, 90);
            g.DrawLine(borderPen, borderRect.Right - BorderRadius, borderRect.Bottom, borderRect.Left + BorderRadius, borderRect.Bottom);
            g.DrawArc(borderPen, borderRect.Left, borderRect.Bottom - arcWidth, arcWidth, arcWidth, 90, 90);
            g.DrawLine(borderPen, borderRect.Left, borderRect.Bottom - BorderRadius, borderRect.Left, borderRect.Top + BorderRadius);

            // 释放资源
            borderPen.Dispose();
        }

        private void UserControl1_MouseEnter(object sender, EventArgs e)
        {
        
        }

        private void UserControl1_Leave(object sender, EventArgs e)
        {
         
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        
        }
    }
}
