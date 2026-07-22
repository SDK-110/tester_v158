using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class GaugeControl : Control
{
    private int _minimum = 0;
    private int _maximum = 100;
    private int _value = 0;
    private Color _pointerColor = Color.Red;

    public GaugeControl()
    {
        DoubleBuffered = true;
        Size = new Size(200, 200);
    }

    public int Minimum
    {
        get { return _minimum; }
        set { _minimum = value; Invalidate(); }
    }

    public int Maximum
    {
        get { return _maximum; }
        set { _maximum = value; Invalidate(); }
    }

    public int Value
    {
        get { return _value; }
        set
        {
            if (value < Minimum || value > Maximum)
                throw new ArgumentException("Value is out of range.");
            _value = value;
            Invalidate();
        }
    }

    public Color PointerColor
    {
        get { return _pointerColor; }
        set { _pointerColor = value; Invalidate(); }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // 绘制背景圆弧
        Rectangle arcRect = new Rectangle(10, 10, Width - 20, Height - 20);
        float startAngle = 135;
        float sweepAngle = 270;
        using (GraphicsPath backgroundPath = new GraphicsPath())
        {
            backgroundPath.AddArc(arcRect, startAngle, sweepAngle);
            using (Pen backgroundPen = new Pen(Color.LightGray, 20))
            {
                g.DrawPath(backgroundPen, backgroundPath);
            }
        }

        // 绘制进度圆弧
        using (GraphicsPath progressPath = new GraphicsPath())
        {
            float progressAngle = (float)(_value - _minimum) / (_maximum - _minimum) * sweepAngle;
            progressPath.AddArc(arcRect, startAngle, progressAngle);
            using (Pen progressPen = new Pen(Color.Green, 20))
            {
                g.DrawPath(progressPen, progressPath);
            }
        }

        // 绘制刻度文本
        for (int i = _minimum; i <= _maximum; i += (_maximum - _minimum) / 10)
        {
            float angle = startAngle + (float)(i - _minimum) / (_maximum - _minimum) * sweepAngle;
            PointF textPoint = GetPointOnArc(arcRect, angle, arcRect.Width / 2 - 10);
            string labelText = i.ToString();
            SizeF textSize = g.MeasureString(labelText, Font);
            PointF labelPoint = new PointF(textPoint.X - textSize.Width / 2, textPoint.Y - textSize.Height / 2);
            g.DrawString(labelText, Font, Brushes.Black, labelPoint);
        }

        // 绘制指针
        float pointerAngle = startAngle + (float)(_value - _minimum) / (_maximum - _minimum) * sweepAngle;
        float pointerLength = Width / 2.5f;
        PointF pointerStart = GetPointOnArc(arcRect, pointerAngle, pointerLength);
        PointF pointerEnd = GetPointOnArc(arcRect, pointerAngle, pointerLength - 20);
        using (Pen pointerPen = new Pen(_pointerColor, 5))
        {
            g.DrawLine(pointerPen, pointerStart, pointerEnd);
        }
    }

    private PointF GetPointOnArc(RectangleF arcRect, float angle, float radius)
    {
        float x = arcRect.Width / 2 + arcRect.X + (float)Math.Cos(angle * Math.PI / 180) * radius;
        float y = arcRect.Height / 2 + arcRect.Y + (float)Math.Sin(angle * Math.PI / 180) * radius;
        return new PointF(x, y);
    }
}