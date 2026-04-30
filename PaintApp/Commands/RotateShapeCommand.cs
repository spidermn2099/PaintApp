using System.Collections.Generic;
using System.Drawing;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class RotateShapeCommand : ICommand
    {
        private List<Shape> shapes;
        private double angle;
        private Point center;
        public RotateShapeCommand(List<Shape> shapes, double angle)
        {
            this.shapes = shapes; this.angle = angle;
            if (shapes.Count > 0)
            {
                var rect = shapes[0].GetBoundingRect();
                foreach (var s in shapes) rect = Rectangle.Union(rect, s.GetBoundingRect());
                center = rect.Center();
            }
        }
        public void Execute() { foreach (var s in shapes) s.Rotate(angle, center); }
        public void Undo() { foreach (var s in shapes) s.Rotate(-angle, center); }
        public void Redo() => Execute();
    }
}