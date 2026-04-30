using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using PaintApp.Models;

namespace PaintApp.Helpers
{
    public class SerializablePoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class SerializableShape
    {
        public string Type { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }

    public class SerializableLayer
    {
        public string Name { get; set; }
        public bool Visible { get; set; }
        public List<SerializableShape> Shapes { get; set; }
    }

    public class SerializableDocument
    {
        public List<SerializableLayer> Layers { get; set; }
    }

    public static class SerializationHelper
    {
        public static void SaveToFile(string filename, List<Layer> layers)
        {
            var doc = new SerializableDocument();
            doc.Layers = new List<SerializableLayer>();

            foreach (var layer in layers)
            {
                var sl = new SerializableLayer
                {
                    Name = layer.Name,
                    Visible = layer.Visible,
                    Shapes = new List<SerializableShape>()
                };

                foreach (var shape in layer.Shapes)
                {
                    var ss = new SerializableShape();
                    ss.Type = shape.GetType().Name;
                    var data = new Dictionary<string, object>();

                    data["PenColor"] = shape.Pen.Color.ToArgb();
                    data["PenWidth"] = shape.Pen.Width;
                    if (shape.Brush is SolidBrush sb)
                        data["BrushColor"] = sb.Color.ToArgb();
                    else
                        data["BrushColor"] = Color.Transparent.ToArgb();

                    switch (shape)
                    {
                        case Segment seg:
                            data["StartX"] = seg.Start.X;
                            data["StartY"] = seg.Start.Y;
                            data["EndX"] = seg.End.X;
                            data["EndY"] = seg.End.Y;
                            break;
                        case Polyline poly:
                            var pts = new List<SerializablePoint>();
                            foreach (var p in poly.Points)
                                pts.Add(new SerializablePoint { X = p.X, Y = p.Y });
                            data["Points"] = pts;
                            break;
                        case RectangleShape rect:
                            data["TopLeftX"] = rect.TopLeft.X;
                            data["TopLeftY"] = rect.TopLeft.Y;
                            data["Width"] = rect.Width;
                            data["Height"] = rect.Height;
                            data["Angle"] = rect.Angle;
                            break;
                        case EllipseShape ell:
                            data["TopLeftX"] = ell.TopLeft.X;
                            data["TopLeftY"] = ell.TopLeft.Y;
                            data["Width"] = ell.Width;
                            data["Height"] = ell.Height;
                            data["Angle"] = ell.Angle;
                            break;
                        case PolygonShape polyg:
                            var pts2 = new List<SerializablePoint>();
                            foreach (var p in polyg.Points)
                                pts2.Add(new SerializablePoint { X = p.X, Y = p.Y });
                            data["Points"] = pts2;
                            data["Radius"] = polyg.Radius;
                            data["Angle"] = polyg.Angle;
                            break;
                    }

                    ss.Data = data;
                    sl.Shapes.Add(ss);
                }
                doc.Layers.Add(sl);
            }

            string json = JsonConvert.SerializeObject(doc, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static List<Layer> LoadFromFile(string filename)
        {
            string json = File.ReadAllText(filename);
            var doc = JsonConvert.DeserializeObject<SerializableDocument>(json);
            var newLayers = new List<Layer>();
            int layerId = 0;

            foreach (var sl in doc.Layers)
            {
                var layer = new Layer(sl.Name, layerId++);
                layer.Visible = sl.Visible;

                foreach (var ss in sl.Shapes)
                {
                    Shape shape = null;
                    var data = ss.Data;

                    switch (ss.Type)
                    {
                        case "Segment":
                            var seg = new Segment();
                            seg.Start = new Point(Convert.ToInt32(data["StartX"]), Convert.ToInt32(data["StartY"]));
                            seg.End = new Point(Convert.ToInt32(data["EndX"]), Convert.ToInt32(data["EndY"]));
                            shape = seg;
                            break;
                        case "Polyline":
                            var poly = new Polyline();
                            var pts = (Newtonsoft.Json.Linq.JArray)data["Points"];
                            bool first = true;
                            foreach (var pt in pts)
                            {
                                int x = Convert.ToInt32(pt["X"]);
                                int y = Convert.ToInt32(pt["Y"]);
                                if (first)
                                {
                                    poly.SetStartPoint(new Point(x, y));
                                    first = false;
                                }
                                else
                                    poly.AddPoint(new Point(x, y));
                            }
                            poly.FinishDrawing();
                            shape = poly;
                            break;
                        case "RectangleShape":
                            var rect = new RectangleShape();
                            rect.TopLeft = new Point(Convert.ToInt32(data["TopLeftX"]), Convert.ToInt32(data["TopLeftY"]));
                            rect.Width = Convert.ToInt32(data["Width"]);
                            rect.Height = Convert.ToInt32(data["Height"]);
                            rect.Angle = Convert.ToDouble(data["Angle"]);
                            shape = rect;
                            break;
                        case "EllipseShape":
                            var ell = new EllipseShape();
                            ell.TopLeft = new Point(Convert.ToInt32(data["TopLeftX"]), Convert.ToInt32(data["TopLeftY"]));
                            ell.Width = Convert.ToInt32(data["Width"]);
                            ell.Height = Convert.ToInt32(data["Height"]);
                            ell.Angle = Convert.ToDouble(data["Angle"]);
                            shape = ell;
                            break;
                        case "PolygonShape":
                            var polyg = new PolygonShape();
                            var pts2 = (Newtonsoft.Json.Linq.JArray)data["Points"];
                            foreach (var pt in pts2)
                            {
                                int x = Convert.ToInt32(pt["X"]);
                                int y = Convert.ToInt32(pt["Y"]);
                                polyg.AddPoint(new Point(x, y));
                            }
                            polyg.Radius = Convert.ToInt32(data["Radius"]);
                            polyg.Angle = Convert.ToDouble(data["Angle"]);
                            shape = polyg;
                            break;
                    }

                    if (shape != null)
                    {
                        Color penColor = Color.FromArgb(Convert.ToInt32(data["PenColor"]));
                        float penWidth = Convert.ToSingle(data["PenWidth"]);
                        Color brushColor = Color.FromArgb(Convert.ToInt32(data["BrushColor"]));

                        var newPen = new Pen(penColor, penWidth);
                        newPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        newPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        newPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        shape.ApplyPen(newPen);

                        if (brushColor != Color.Transparent)
                            shape.ApplyBrush(new SolidBrush(brushColor));

                        layer.AddShape(shape);
                    }
                }
                newLayers.Add(layer);
            }
            return newLayers;
        }
    }
}