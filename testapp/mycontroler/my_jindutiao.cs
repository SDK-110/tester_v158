using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MetroFramework.Drawing.MetroPaint;
using System.Windows.Forms;
using RohdeSchwarz.RsCmwBase;

namespace testapp.Multi_Form
{
    public class PercentageProgressBar : ProgressBar
    {
        public Color frot_color { set; get; }
        public PercentageProgressBar()
        {
            // 设置控件的双缓冲模式以避免闪烁
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.frot_color = this.ForeColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics graphics = e.Graphics;

            // 绘制进度条背景
            using (Brush brush = new SolidBrush(BackColor))
            {
                graphics.FillRectangle(brush, rect);
            }

            // 计算进度条值
            float percentage = (float)Value / Maximum * 100;
            int progressWidth = (int)(rect.Width * percentage / 100);

            // 绘制进度条
            using (Brush brush = new SolidBrush(ForeColor))
            {
                graphics.FillRectangle(brush, new Rectangle(rect.X, rect.Y, progressWidth, rect.Height));
            }

            // 绘制百分比文本
            using (Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold))
            using (Brush brush = new SolidBrush(frot_color))
            {
                string text = $"{percentage:0.00}%";
                SizeF textSize = graphics.MeasureString(text, font);
                graphics.DrawString(text, font, brush, rect.X + (rect.Width - textSize.Width) / 2, rect.Y + (rect.Height - textSize.Height) / 2);
            }

            base.OnPaint(e);
        }
    }
}
