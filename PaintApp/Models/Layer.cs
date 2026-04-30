using System.Collections.Generic;

namespace PaintApp.Models
{
    public class Layer
    {
        private string name;
        private int id;
        private bool visible;
        private List<Shape> shapes = new List<Shape>();

        public Layer(string name, int id) { this.name = name; this.id = id; visible = true; }
        public string Name => name; public int Id => id; public bool Visible { get => visible; set => visible = value; }
        public List<Shape> Shapes => shapes;
        public void AddShape(Shape shape) { if (shape != null && !shapes.Contains(shape)) { shapes.Add(shape); shape.Layer = id; } }
        public void RemoveShape(Shape shape) => shapes.Remove(shape);
        public void ClearShapes() => shapes.Clear();
    }
}