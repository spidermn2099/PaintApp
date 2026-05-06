using System.Collections.Generic;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class RotateShapeCommand : ICommand
    {
        private List<Shape> shapes;
        private double angle;
        private List<System.Drawing.Point> centers;

        public RotateShapeCommand(List<Shape> shapes, double angle)
        {
            this.shapes = new List<Shape>(shapes);
            this.angle = angle;

            // Сохраняем центры фигур ДО поворота
            centers = new List<System.Drawing.Point>();
            foreach (var shape in shapes)
            {
                centers.Add(shape.GetCenter());
            }
        }

        public void Execute()
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].Rotate(angle, centers[i]);
            }
        }

        public void Undo()
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].Rotate(-angle, centers[i]);
            }
        }

        public void Redo()
        {
            Execute();
        }
    }
}