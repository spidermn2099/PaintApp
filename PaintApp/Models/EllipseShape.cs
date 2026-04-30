using System;
using System.Drawing;

namespace PaintApp.Models
{
    public class EllipseShape : Shape
    {
        private Point topLeft;
        private int width, height;
        private double angle;

        public EllipseShape() { width = 100; height = 100; angle = 0; topLeft = new Point(0, 0); }

        public Point TopLeft { get => topLeft; set => topLeft = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public double Angle { get => angle; set => angle = value; }

        public override void Draw(Graphics g)
        {
            var state = g.Save();
            var rect = GetBoundingRect();
            var center = rect.Center();
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform((float)angle);
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawEllipse(pen, topLeft.X, topLeft.Y, width, height);
            g.FillEllipse(brush, topLeft.X, topLeft.Y, width, height);
            g.Restore(state);
        }

        public override void DrawPreview(Graphics g, Point currentPoint)
        {
            int w = currentPoint.X - topLeft.X;
            int h = currentPoint.Y - topLeft.Y;
            if (w < 5) w = 5; if (h < 5) h = 5;
            g.DrawEllipse(pen, topLeft.X, topLeft.Y, w, h);
            g.FillEllipse(brush, topLeft.X, topLeft.Y, w, h);
        }

        public override void SetStartPoint(Point point) => topLeft = point;
        public override void SetEndPoint(Point point) { width = point.X - topLeft.X; height = point.Y - topLeft.Y; if (width < 5) width = 5; if (height < 5) height = 5; }
        public override bool Contains(Point point)
        {
            var rect = GetBoundingRect();
            var center = rect.Center();
            var p = RotatePoint(point, center, -angle);
            double rx = rect.Width / 2.0, ry = rect.Height / 2.0;
            if (rx == 0 || ry == 0) return false;
            double dx = (p.X - center.X) / rx, dy = (p.Y - center.Y) / ry;
            return dx * dx + dy * dy <= 1.0;
        }
        public override void Move(int dx, int dy) => topLeft.Offset(dx, dy);
        public override void Rotate(double a, Point center) { angle += a; if (angle >= 360) angle -= 360; if (angle < 0) angle += 360; }
        public override void Resize(Point delta, int handle, Rectangle originalRect)
        {
            int nw = width, nh = height;
            Point nt = topLeft;
            switch (handle)
            {
                case 0: nw = originalRect.Width - delta.X; nh = originalRect.Height - delta.Y; nt = new Point(originalRect.Left + delta.X, originalRect.Top + delta.Y); break;
                case 1: nw = originalRect.Width + delta.X; nh = originalRect.Height - delta.Y; nt = new Point(originalRect.Left, originalRect.Top + delta.Y); break;
                case 2: nw = originalRect.Width - delta.X; nh = originalRect.Height + delta.Y; nt = new Point(originalRect.Left + delta.X, originalRect.Top); break;
                case 3: nw = originalRect.Width + delta.X; nh = originalRect.Height + delta.Y; nt = originalRect.Location; break;
                default: return;
            }
            if (nw >= 10 && nh >= 10) { width = nw; height = nh; topLeft = nt; }
        }
        public override Rectangle GetBoundingRect() => new Rectangle(topLeft.X, topLeft.Y, width, height);
        public override string GetTypeName() => "Ellipse";
        public override Shape Clone() { var e = new EllipseShape { topLeft = this.topLeft, width = this.width, height = this.height, angle = this.angle }; e.ApplyPen(pen); e.ApplyBrush(brush); return e; }
        public override Point GetCenter() => GetBoundingRect().Center();
    }
}