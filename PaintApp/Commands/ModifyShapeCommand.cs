using System.Drawing;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class ModifyShapeCommand : ICommand
    {
        private Shape shape;
        private Pen oldPen, newPen;
        private Brush oldBrush, newBrush;

        public ModifyShapeCommand(Shape shape, Pen newPen, Brush newBrush)
        {
            this.shape = shape;

            oldPen = new Pen(shape.Pen.Color, shape.Pen.Width);
            oldBrush = new SolidBrush(((SolidBrush)shape.Brush).Color);

            this.newPen = new Pen(newPen.Color, newPen.Width);
            this.newBrush = new SolidBrush(((SolidBrush)newBrush).Color);
        }

        public void Execute()
        {
            shape.ApplyPen(newPen);
            shape.ApplyBrush(newBrush);
        }

        public void Undo()
        {
            shape.ApplyPen(oldPen);
            shape.ApplyBrush(oldBrush);
        }

        public void Redo() => Execute();
    }
}