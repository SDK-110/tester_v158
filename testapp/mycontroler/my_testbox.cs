using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp.mycontroler
{
    public class CustomTextBox : TextBox
    {
        private string hintText;
        private bool showHint;

        public string HintText
        {
            get { return hintText; }
            set
            {
                hintText = value;
                Invalidate();
            }
        }

        public CustomTextBox()
        {
            showHint = true;
            hintText = "请输入内容...";
            this.ForeColor = Color.Black;
            this.BackColor = Color.White;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            showHint = string.IsNullOrEmpty(this.Text);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (showHint)
            {
                using (var brush = new SolidBrush(SystemColors.GrayText))
                {
                    e.Graphics.DrawString(hintText, this.Font, brush, new PointF(0, this.Height - this.Font.Height));
                }
            }
        }
    }



}
