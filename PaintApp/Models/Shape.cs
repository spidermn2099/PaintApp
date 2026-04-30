using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintApp.Models
{
    public abstract class Shape
    {
        protected Pen pen;
        protected Brush brush;
        protected bool selected;
        protected int layer;

        public Shape()
        {
            pen = new Pen(Color.Black, 2);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;
            brush = new SolidBrush(Color.White);
            selected = false;
            layer = 0;
        }

        public virtual void Draw(Graphics g) { }
        public virtual void DrawPreview(Graphics g, Point currentPoint) { }
        public abstract void SetStartPoint(Point point);
        public abstract void SetEndPoint(Point point);
        public abstract bool Contains(Point point);
        public abstract void Move(int dx, int dy);
        public abstract void Rotate(double angle, Point center);
        public abstract void Resize(Point delta, int handle, Rectangle originalRect);
        public abstract Rectangle GetBoundingRect();
        public abstract string GetTypeName();
        public abstract Shape Clone();
        public abstract Point GetCenter();

        public Pen Pen => pen;
        public Brush Brush => brush;

        public void ApplyPen(Pen source)
        {
            if (source == null) return;
            try
            {
                float w = (source.Width > 0 && !float.IsNaN(source.Width)) ? source.Width : 2f;
                var newPen = new Pen(source.Color, w);
                newPen.StartCap = source.StartCap;
                newPen.EndCap = source.EndCap;
                newPen.LineJoin = source.LineJoin;
                pen = newPen;
            }
            catch
            {
                pen = new Pen(Color.Black, 2);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
            }
        }

        public void ApplyBrush(Brush source)
        {
            if (source == null) return;
            try
            {
                if (source is SolidBrush sb)
                    brush = new SolidBrush(sb.Color);
                else
                    brush = new SolidBrush(Color.White);
            }
            catch
            {
                brush = new SolidBrush(Color.White);
            }
        }

        public bool Selected { get => selected; set => selected = value; }
        public int Layer { get => layer; set => layer = value; }

        protected Point RotatePoint(Point p, Point center, double angleDeg)
        {
            double rad = angleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);
            double dx = p.X - center.X;
            double dy = p.Y - center.Y;
            double newX = dx * cos - dy * sin;
            double newY = dx * sin + dy * cos;
            return new Point(center.X + (int)Math.Round(newX), center.Y + (int)Math.Round(newY));
        }
    }

    public static class RectangleExtensions
    {
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }
    }
}