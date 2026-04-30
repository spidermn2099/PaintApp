using System;
using System.Collections.Generic;
using System.Drawing;

namespace PaintApp.Models
{
    public class Polyline : Shape
    {
        private List<Point> points = new List<Point>();
        private bool isDrawing = true;

        public override void Draw(Graphics g)
        {
            if (points.Count >= 2) g.DrawLines(pen, points.ToArray());
        }

        public override void DrawPreview(Graphics g, Point currentPoint)
        {
            Draw(g);
            if (isDrawing && points.Count > 0)
                g.DrawLine(pen, points[points.Count - 1], currentPoint);
        }

        public override void SetStartPoint(Point point) { points.Clear(); points.Add(point); isDrawing = true; }
        public override void SetEndPoint(Point point) { }
        public void AddPoint(Point point) { if (isDrawing) points.Add(point); }
        public void FinishDrawing() => isDrawing = false;
        public bool IsDrawing => isDrawing;
        public List<Point> Points => points;

        public override bool Contains(Point point)
        {
            const int tol = 8;
            for (int i = 0; i < points.Count - 1; i++)
            {
                var p1 = points[i]; var p2 = points[i + 1];
                double len = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
                if (len < 0.001) continue;
                double t = ((point.X - p1.X) * (p2.X - p1.X) + (point.Y - p1.Y) * (p2.Y - p1.Y)) / (len * len);
                t = Math.Max(0, Math.Min(1, t));
                double cx = p1.X + t * (p2.X - p1.X);
                double cy = p1.Y + t * (p2.Y - p1.Y);
                double dist = Math.Sqrt((point.X - cx) * (point.X - cx) + (point.Y - cy) * (point.Y - cy));
                if (dist <= tol) return true;
            }
            return false;
        }

        public override void Move(int dx, int dy)
        {
            for (int i = 0; i < points.Count; i++)
                points[i] = new Point(points[i].X + dx, points[i].Y + dy);
        }

        public override void Rotate(double angle, Point center)
        {
            for (int i = 0; i < points.Count; i++)
                points[i] = RotatePoint(points[i], center, angle);
        }

        public override void Resize(Point delta, int handle, Rectangle originalRect)
        {
            if (points.Count < 2) return;
            Rectangle rect = originalRect.IsEmpty ? GetBoundingRect() : originalRect;
            if (rect.Width == 0 || rect.Height == 0) return;
            float sx = 1, sy = 1;
            Point newCenter = rect.Center();
            switch (handle)
            {
                case 0: sx = (rect.Width - delta.X) / (float)rect.Width; sy = (rect.Height - delta.Y) / (float)rect.Height; newCenter = new Point(rect.Left + delta.X + (rect.Width - delta.X) / 2, rect.Top + delta.Y + (rect.Height - delta.Y) / 2); break;
                case 1: sx = (rect.Width + delta.X) / (float)rect.Width; sy = (rect.Height - delta.Y) / (float)rect.Height; newCenter = new Point(rect.Left + (rect.Width + delta.X) / 2, rect.Top + delta.Y + (rect.Height - delta.Y) / 2); break;
                case 2: sx = (rect.Width - delta.X) / (float)rect.Width; sy = (rect.Height + delta.Y) / (float)rect.Height; newCenter = new Point(rect.Left + delta.X + (rect.Width - delta.X) / 2, rect.Top + (rect.Height + delta.Y) / 2); break;
                case 3: sx = (rect.Width + delta.X) / (float)rect.Width; sy = (rect.Height + delta.Y) / (float)rect.Height; newCenter = new Point(rect.Left + (rect.Width + delta.X) / 2, rect.Top + (rect.Height + delta.Y) / 2); break;
                default: return;
            }
            if (sx < 0.1f) sx = 0.1f; if (sy < 0.1f) sy = 0.1f;
            for (int i = 0; i < points.Count; i++)
            {
                float dx = points[i].X - newCenter.X;
                float dy = points[i].Y - newCenter.Y;
                points[i] = new Point(newCenter.X + (int)(dx * sx), newCenter.Y + (int)(dy * sy));
            }
        }

        public override Rectangle GetBoundingRect()
        {
            if (points.Count == 0) return Rectangle.Empty;
            int minX = points[0].X, maxX = points[0].X, minY = points[0].Y, maxY = points[0].Y;
            foreach (var p in points) { if (p.X < minX) minX = p.X; if (p.X > maxX) maxX = p.X; if (p.Y < minY) minY = p.Y; if (p.Y > maxY) maxY = p.Y; }
            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        public override string GetTypeName() => "Polyline";
        public override Shape Clone() { var p = new Polyline(); p.points = new List<Point>(points); p.isDrawing = false; p.ApplyPen(pen); p.ApplyBrush(brush); return p; }
        public override Point GetCenter() => GetBoundingRect().Center();
    }
}