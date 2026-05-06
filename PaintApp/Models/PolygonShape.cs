using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintApp.Models
{
    public class PolygonShape : Shape
    {
        private List<Point> points = new List<Point>();
        private Point center;
        private int radius;
        private double angle;
        private bool isDrawing;
        private Point startPoint;

        public PolygonShape() { radius = 50; angle = 0; isDrawing = false; center = new Point(0, 0); MakeHexagon(center, radius); }

        public List<Point> Points { get => points; set => points = value; }
        public Point Center { get => center; set => center = value; }
        public int Radius { get => radius; set => radius = value; }
        public double Angle { get => angle; set => angle = value; }

        private void MakeHexagon(Point c, int r)
        {
            points.Clear();
            center = c;
            radius = r;
            for (int i = 0; i < 6; i++)
            {
                double rad = i * 60 * Math.PI / 180;
                int x = c.X + (int)(r * Math.Cos(rad));
                int y = c.Y + (int)(r * Math.Sin(rad));
                points.Add(new Point(x, y));
            }
            isDrawing = false;
        }

        public override void Draw(Graphics g)
        {
            if (points.Count < 3) return;
            var state = g.Save();
            var c = GetCenter();
            g.TranslateTransform(c.X, c.Y);
            g.RotateTransform((float)angle);
            g.TranslateTransform(-c.X, -c.Y);
            g.DrawPolygon(pen, points.ToArray());
            g.FillPolygon(brush, points.ToArray());
            g.Restore(state);
        }

        public override void DrawPreview(Graphics g, Point currentPoint)
        {
            if (!isDrawing) return;
            int r = (int)Math.Sqrt((currentPoint.X - startPoint.X) * (currentPoint.X - startPoint.X) + (currentPoint.Y - startPoint.Y) * (currentPoint.Y - startPoint.Y));
            if (r < 10) r = 10;
            List<Point> hex = new List<Point>();
            for (int i = 0; i < 6; i++)
            {
                double rad = i * 60 * Math.PI / 180;
                int x = startPoint.X + (int)(r * Math.Cos(rad));
                int y = startPoint.Y + (int)(r * Math.Sin(rad));
                hex.Add(new Point(x, y));
            }
            g.DrawPolygon(pen, hex.ToArray());
            g.FillPolygon(brush, hex.ToArray());
        }

        public override void SetStartPoint(Point point) { startPoint = point; isDrawing = true; }
        public override void SetEndPoint(Point point)
        {
            if (isDrawing)
            {
                int r = (int)Math.Sqrt((point.X - startPoint.X) * (point.X - startPoint.X) + (point.Y - startPoint.Y) * (point.Y - startPoint.Y));
                if (r < 10) r = 10;
                center = startPoint;
                radius = r;
                angle = 0;
                MakeHexagon(center, radius);
                isDrawing = false;
            }
        }

        public void AddPoint(Point point) { points.Add(point); }

        public override bool Contains(Point point)
        {
            if (points.Count < 3) return false;
            var c = GetCenter();
            var p = RotatePoint(point, c, -angle);
            using (var path = new GraphicsPath())
            {
                path.AddPolygon(points.ToArray());
                return path.IsVisible(p);
            }
        }

        public override void Move(int dx, int dy)
        {
            center.Offset(dx, dy);
            for (int i = 0; i < points.Count; i++)
                points[i] = new Point(points[i].X + dx, points[i].Y + dy);
        }

        public override void Rotate(double a, Point _) { angle += a; if (angle >= 360) angle -= 360; if (angle < 0) angle += 360; }

        public override void Resize(Point delta, int handle, Rectangle _)
        {
            int nr = radius;
            switch (handle)
            {
                case 0: nr = radius - (delta.X + delta.Y) / 2; break;
                case 1: nr = radius + (delta.X - delta.Y) / 2; break;
                case 2: nr = radius - (delta.X - delta.Y) / 2; break;
                case 3: nr = radius + (delta.X + delta.Y) / 2; break;
                default: nr = radius + (delta.X + delta.Y) / 2; break;
            }
            if (nr >= 10) { radius = nr; MakeHexagon(center, radius); }
        }

        public override Rectangle GetBoundingRect()
        {
            if (points.Count == 0) return Rectangle.Empty;
            int minX = points[0].X, maxX = points[0].X, minY = points[0].Y, maxY = points[0].Y;
            foreach (var p in points)
            {
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }
            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        public override string GetTypeName() => "PolygonShape";

        public override Shape Clone()
        {
            var p = new PolygonShape();
            p.points = new List<Point>(points);
            p.center = center;
            p.radius = radius;
            p.angle = angle;
            p.isDrawing = false;
            p.ApplyPen(pen);
            p.ApplyBrush(brush);
            return p;
        }

        public override Point GetCenter() => GetBoundingRect().Center();

        // Полиморфная сериализация
        public override void SaveToDictionary(Dictionary<string, object> data)
        {
            base.SaveToDictionary(data);
            var ptList = new List<SerializablePoint>();
            foreach (var pt in points)
                ptList.Add(new SerializablePoint { X = pt.X, Y = pt.Y });
            data["Points"] = ptList;
            data["CenterX"] = center.X;
            data["CenterY"] = center.Y;
            data["Radius"] = radius;
            data["Angle"] = angle;
            data["IsDrawing"] = isDrawing;
        }

        public override void LoadFromDictionary(Dictionary<string, object> data)
        {
            base.LoadFromDictionary(data);
            var ptList = (List<SerializablePoint>)data["Points"];
            points.Clear();
            foreach (var sp in ptList)
                points.Add(new Point(sp.X, sp.Y));
            center = new Point(Convert.ToInt32(data["CenterX"]), Convert.ToInt32(data["CenterY"]));
            radius = Convert.ToInt32(data["Radius"]);
            angle = Convert.ToDouble(data["Angle"]);
            isDrawing = Convert.ToBoolean(data["IsDrawing"]);
        }
    }
}