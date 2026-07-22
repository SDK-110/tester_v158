using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static MetroFramework.Drawing.MetroPaint;
using System.Windows.Forms;

namespace testapp.mycontroler
{
 




        public class RoundedBorderTextBox : Control
        {
            private Color borderColor = Color.Gray;
            private int borderRadius = 10;

            public RoundedBorderTextBox()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            }

            public Color BorderColor
            {
                get { return borderColor; }
                set
                {
                    borderColor = value;
                    Refresh();
                }
            }

            public int BorderRadius
            {
                get { return borderRadius; }
                set
                {
                    borderRadius = value;
                    Refresh();
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Graphics g = e.Graphics;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                Pen borderPen = new Pen(BorderColor, 1);
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
        }



        public partial class TransTextbox : RichTextBox//如果继承TextBox,字体的颜色是黑色，改不了颜色
        {
            [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr LoadLibrary(string lpFileName);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern Int32 SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

            private const int EM_SETCUEBANNER = 0x1501;
            private string text = "";
            private Font font = new Font("Arial", 12);
            private StringFormat stringFormat = new StringFormat();
            public TransTextbox()
            {
                BorderStyle = BorderStyle.None;
                SendMessage(this.Handle, EM_SETCUEBANNER, IntPtr.Zero, "fffffffffffff");
            }
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams prams = base.CreateParams;
                    if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                    {
                        prams.ExStyle |= 0x020;
                        prams.ClassName = "RICHEDIT50W";
                    }
                    return prams;
                }
            }



            public override string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    Invalidate(); // 重绘控件
                }
            }


        }

        public class CircularProgressBar : Control
        {
            private Timer timer;
            private int progress;
            private int angle;

            public CircularProgressBar()
            {
                timer = new Timer();
                timer.Interval = 50; // 每50毫秒刷新一次进度条
                timer.Tick += Timer_Tick;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int diameter = Math.Min(Width, Height) - 10;
                int radius = diameter / 2;
                int x = (Width - diameter) / 2;
                int y = (Height - diameter) / 2;

                // 画背景圆
                g.DrawEllipse(Pens.LightGray, x, y, diameter, diameter);

                // 计算进度条的角度
                int startAngle = -90;
                int sweepAngle = (int)(360 * ((float)progress / 100));
                angle = sweepAngle;

                // 画进度条
                using (SolidBrush brush = new SolidBrush(Color.Blue))
                {
                    g.FillPie(brush, x, y, diameter, diameter, startAngle, sweepAngle);
                }

                // 画进度文本
                using (Font font = new Font(Font.FontFamily, 14))
                {
                    string progressText = $"{progress}%";
                    SizeF textSize = g.MeasureString(progressText, font);
                    float textX = x + radius - textSize.Width / 2;
                    float textY = y + radius - textSize.Height / 2;
                    g.DrawString(progressText, font, Brushes.Black, textX, textY);
                }
            }

            private void Timer_Tick(object sender, EventArgs e)
            {
                progress++;
                if (progress > 100)
                    progress = 0;

                Invalidate(); // 刷新控件，触发OnPaint事件
            }

            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                timer.Start(); // 启动定时器
            }

            protected override void OnHandleDestroyed(EventArgs e)
            {
                base.OnHandleDestroyed(e);
                timer.Stop(); // 停止定时器
            }
        }




        public class HollowCircularProgressControl : Control
        {
            private int _progress;
            private Timer _timer;

            public HollowCircularProgressControl()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
                _timer = new Timer { Interval = 100 }; // 100 毫秒更新一次
                _timer.Tick += Timer_Tick;
            }

            public int Progress
            {
                get { return _progress; }
                set
                {
                    if (value < 0 || value > 100)
                        value = 0;

                    _progress = value;
                    Invalidate(); // 触发重绘
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                DrawHollowCircularProgress(e.Graphics);
            }

            private void DrawHollowCircularProgress(Graphics g)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int size = Math.Min(Width, Height);

                int x = (Width) / 2;
                int y = (Height) / 2;
                using (Pen pen = new Pen(ForeColor, size / 10))
                using (Pen bgPen = new Pen(Color.LightGray, size / 20))
                {
                    // 绘制背景圆
                    // g.DrawEllipse(bgPen, size / 20, size / 20, size - size / 10, size - size / 10);

                    // 绘制进度弧
                    Rectangle rect = new Rectangle(size / 10, size / 10, size - size / 5, size - size / 5);
                    float startAngle = -90;
                    float sweepAngle = (360 * _progress) / 100;
                    g.DrawArc(pen, rect, startAngle, sweepAngle);
                    using (Font font = new Font(Font.FontFamily, 14))
                    {
                        string progressText = $"{_progress}%";
                        SizeF textSize = g.MeasureString(progressText, font);
                        float textX = x - textSize.Width / 2;
                        float textY = y - textSize.Height / 2;
                        g.DrawString(progressText, font, Brushes.Black, textX, textY);
                    }
                }
            }

            public void StartProgress()
            {
                _timer.Start();
            }

            public void StopProgress()
            {
                _timer.Stop();
            }

            private void Timer_Tick(object sender, EventArgs e)
            {
                Progress = (_progress + 1) % 101; // 更新进度值
            }
        }
        public class CustomInputBox : Control
        {



            private string text = "";
            private Font font = new Font("Arial", 12);
            private StringFormat stringFormat = new StringFormat();
            private bool isActive = false; // 跟踪控件是否被激活
            [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr LoadLibrary(string lpFileName);
            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);


            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams prams = base.CreateParams;
                    if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                    {
                        prams.ExStyle |= 0x020;
                        prams.ClassName = "RICHEDIT50W";
                    }
                    return prams;
                }
            }

            public CustomInputBox()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

                BackColor = Color.Transparent;
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
            }

            public override string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    Invalidate(); // 重绘控件
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // 绘制下划线
                int lineY = Height - 2;
                e.Graphics.DrawLine(Pens.Black, 0, lineY, Width, lineY);

                // 绘制文本
                e.Graphics.DrawString(text, font, Brushes.Black, 0, 0, stringFormat);




                Rectangle rect = ClientRectangle;
                int radius = 30; // 圆角半径
                IntPtr hRgn = CreateRoundRectRgn(rect.Left, rect.Top, rect.Right, rect.Bottom, radius * 2, radius * 2);
                Region region = Region.FromHrgn(hRgn);
                e.Graphics.SetClip(region, System.Drawing.Drawing2D.CombineMode.Replace);
                e.Graphics.DrawRectangle(Pens.Black, rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);








            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);

            }

            private void Activate()
            {
                isActive = true;

            }






            private void InputTextBox_TextChanged(object sender, EventArgs e)
            {

                Invalidate(); // 重绘控件
            }

            private void InputTextBox_LostFocus(object sender, EventArgs e)
            {

            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                // 在鼠标进入时不做任何操作
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);


            }
        }
   
}
