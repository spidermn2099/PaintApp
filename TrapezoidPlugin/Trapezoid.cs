using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using PaintApp.Models;

namespace TrapezoidPlugin
{
    public class Trapezoid : Shape
    {
        private Point topLeft;
        private int topWidth = 80;
        private int bottomWidth = 120;
        private int height = 60;
        private double angle = 0;

        public Point TopLeft { get => topLeft; set => topLeft = value; }
        public int TopWidth { get => topWidth; set => topWidth = value; }
        public int BottomWidth { get => bottomWidth; set => bottomWidth = value; }
        public int Height { get => height; set => height = value; }
        public double Angle { get => angle; set => angle = value; }

        public override void Draw(Graphics g)
        {
            GraphicsState state = g.Save();
            Point center = GetCenter();
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform((float)angle);
            g.TranslateTransform(-center.X, -center.Y);

            Point[] points = new Point[4];
            points[0] = topLeft;
            points[1] = new Point(topLeft.X + topWidth, topLeft.Y);
            points[2] = new Point(topLeft.X + ((topWidth - bottomWidth) / 2) + bottomWidth, topLeft.Y + height);
            points[3] = new Point(topLeft.X + ((topWidth - bottomWidth) / 2), topLeft.Y + height);

            using (var pen = new Pen(Pen.Color, Pen.Width))
            {
                Color fillColor = Color.Transparent;
                if (Brush is SolidBrush sb) fillColor = sb.Color;
                if (fillColor != Color.Transparent)
                {
                    using (var brush = new SolidBrush(fillColor))
                        g.FillPolygon(brush, points);
                }
                g.DrawPolygon(pen, points);
            }
            g.Restore(state);
        }

        public override void DrawPreview(Graphics g, Point currentPoint)
        {
            int w = currentPoint.X - topLeft.X;
            int h = currentPoint.Y - topLeft.Y;
            if (w < 10) w = 10;
            if (h < 10) h = 10;

            Point[] points = new Point[4];
            points[0] = topLeft;
            points[1] = new Point(topLeft.X + w, topLeft.Y);
            points[2] = new Point(topLeft.X + ((w - (w + 40)) / 2) + (w + 40), topLeft.Y + h);
            points[3] = new Point(topLeft.X + ((w - (w + 40)) / 2), topLeft.Y + h);

            using (var pen = new Pen(Pen.Color, Pen.Width))
            {
                Color fillColor = Color.Transparent;
                if (Brush is SolidBrush sb) fillColor = sb.Color;
                if (fillColor != Color.Transparent)
                {
                    using (var brush = new SolidBrush(fillColor))
                        g.FillPolygon(brush, points);
                }
                g.DrawPolygon(pen, points);
            }
        }

        public override void SetStartPoint(Point point) => topLeft = point;
        public override void SetEndPoint(Point point)
        {
            topWidth = point.X - topLeft.X;
            height = point.Y - topLeft.Y;
            bottomWidth = topWidth + 40;
            if (topWidth < 10) topWidth = 10;
            if (height < 10) height = 10;
        }
        public override bool Contains(Point point) => GetBoundingRect().Contains(point);
        public override void Move(int dx, int dy) => topLeft = new Point(topLeft.X + dx, topLeft.Y + dy);
        public override void Rotate(double a, Point center) { angle += a; if (angle >= 360) angle -= 360; if (angle < 0) angle += 360; }
        public override void Resize(Point delta, int handle, Rectangle originalRect)
        {
            if (handle == 0) { topWidth = originalRect.Width - delta.X; height = originalRect.Height - delta.Y; bottomWidth = topWidth + 40; topLeft = new Point(originalRect.Left + delta.X, originalRect.Top + delta.Y); }
            else if (handle == 3) { topWidth = originalRect.Width + delta.X; height = originalRect.Height + delta.Y; bottomWidth = topWidth + 40; }
            if (topWidth < 10) topWidth = 10; if (height < 10) height = 10;
        }
        public override Rectangle GetBoundingRect() => new Rectangle(topLeft.X, topLeft.Y, Math.Max(topWidth, bottomWidth), height);
        public override string GetTypeName() => "Trapezoid";
        public override Shape Clone() { var t = new Trapezoid(); t.topLeft = topLeft; t.topWidth = topWidth; t.bottomWidth = bottomWidth; t.height = height; t.angle = angle; t.ApplyPen(Pen); t.ApplyBrush(Brush); return t; }
        public override Point GetCenter() => new Point(topLeft.X + (topWidth + bottomWidth) / 4, topLeft.Y + height / 2);
    }
}