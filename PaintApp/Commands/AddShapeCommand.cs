using PaintApp.Models;

namespace PaintApp.Commands
{
    public class AddShapeCommand : ICommand
    {
        private Shape shape;
        private Layer layer;
        private List<Shape> allShapes;

        public AddShapeCommand(Shape shape, Layer layer, List<Shape> allShapes)
        {
            this.shape = shape;
            this.layer = layer;
            this.allShapes = allShapes;
        }

        public void Execute()
        {
            layer.AddShape(shape);
            allShapes.Add(shape);
        }

        public void Undo()
        {
            layer.RemoveShape(shape);
            allShapes.Remove(shape);
        }

        public void Redo() => Execute();
    }
}