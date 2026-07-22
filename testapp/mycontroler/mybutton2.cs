using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp.mycontroler
{


    class RoundedButton : Control
    {
        private bool isMouseOver;
        private bool isMouseDown;
        private int cornerRadius = 10;
        private Color hoverColor = Color.LightGray;
        private Color clickColor = Color.DarkGray;
        private Color mouseLeaveColor = Color.Gray;

        public RoundedButton()
        {
            // 设置控件的样式
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            UpdateStyles();

            // 设置默认大小
            Size = new Size(100, 30);

            // 订阅鼠标事件
            MouseEnter += RoundedButton_MouseEnter;
            MouseLeave += RoundedButton_MouseLeave;
            MouseDown += RoundedButton_MouseDown;
            MouseUp += RoundedButton_MouseUp;
        }

        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                if (cornerRadius != value)
                {
                    cornerRadius = value;
                    Invalidate();
                }
            }
        }

        public Color HoverColor
        {
            get { return hoverColor; }
            set
            {
                if (hoverColor != value)
                {
                    hoverColor = value;
                    Invalidate();
                }
            }
        }

        public Color ClickColor
        {
            get { return clickColor; }
            set
            {
                if (clickColor != value)
                {
                    clickColor = value;
                    Invalidate();
                }
            }
        }

        public Color MouseLeaveColor
        {
            get { return mouseLeaveColor; }
            set
            {
                if (mouseLeaveColor != value)
                {
                    mouseLeaveColor = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
           // Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            // 根据鼠标状态设置按钮的背景色
            Color backColor = isMouseDown ? clickColor : isMouseOver ? hoverColor : mouseLeaveColor;

            // 创建一个圆角矩形
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // 使用 GraphicsPath 对象绘制圆角矩形
            using (GraphicsPath path = GetRoundedRectangle(rect, cornerRadius))
            {
                // 填充圆角矩形作为按钮的背景
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    g.FillPath(brush, path);
                }
            }

            // 绘制按钮的文本
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void RoundedButton_MouseEnter(object sender, EventArgs e)
        {
            isMouseOver = true;
            Invalidate();
        }

        private void RoundedButton_MouseLeave(object sender, EventArgs e)
        {
            isMouseOver = false;
            isMouseDown = false;
            Invalidate();
        }

        private void RoundedButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                Invalidate();
            }
        }

        private void RoundedButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                Invalidate();
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // 左上角
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90); // 右上角
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90); // 右下角
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90); // 左下角
            path.CloseFigure();
            return path;
        }
    }
}
