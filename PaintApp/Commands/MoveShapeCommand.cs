using System.Collections.Generic;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class MoveShapeCommand : ICommand
    {
        private List<Shape> shapes;
        private int dx, dy;

        public MoveShapeCommand(List<Shape> shapes, int dx, int dy)
        {
            this.shapes = new List<Shape>(shapes);
            this.dx = dx;
            this.dy = dy;
        }

        public void Execute()
        {
            foreach (var shape in shapes)
                shape.Move(dx, dy);
        }

        public void Undo()
        {
            foreach (var shape in shapes)
                shape.Move(-dx, -dy);
        }

        public void Redo() => Execute();
    }
}