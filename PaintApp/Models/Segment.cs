using System;
using System.Drawing;

namespace PaintApp.Models
{
    public class Segment : Shape
    {
        private Point start, end;

        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }

        public override void Draw(Graphics g) => g.DrawLine(pen, start, end);
        public override void DrawPreview(Graphics g, Point currentPoint) => g.DrawLine(pen, start, currentPoint);
        public override void SetStartPoint(Point point) => start = point;
        public override void SetEndPoint(Point point) => end = point;

        public override bool Contains(Point point)
        {
            const int tol = 8;
            double len = Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y));
            if (len < 0.001) return false;
            double t = ((point.X - start.X) * (end.X - start.X) + (point.Y - start.Y) * (end.Y - start.Y)) / (len * len);
            t = Math.Max(0, Math.Min(1, t));
            double closestX = start.X + t * (end.X - start.X);
            double closestY = start.Y + t * (end.Y - start.Y);
            double dist = Math.Sqrt((point.X - closestX) * (point.X - closestX) + (point.Y - closestY) * (point.Y - closestY));
            return dist <= tol;
        }

        public override void Move(int dx, int dy) { start.Offset(dx, dy); end.Offset(dx, dy); }
        public override void Rotate(double angle, Point center) { start = RotatePoint(start, center, angle); end = RotatePoint(end, center, angle); }
        public override void Resize(Point delta, int handle, Rectangle originalRect) { if (handle == 0) start.Offset(delta); else end.Offset(delta); }
        public override Rectangle GetBoundingRect() => new Rectangle(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y), Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
        public override string GetTypeName() => "Segment";
        public override Shape Clone() { var s = new Segment { start = this.start, end = this.end }; s.ApplyPen(pen); s.ApplyBrush(brush); return s; }
        public override Point GetCenter() => new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
    }
}