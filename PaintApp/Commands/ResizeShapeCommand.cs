using System.Drawing;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class ResizeShapeCommand : ICommand
    {
        private Shape shape;
        private Point delta;
        private int handle;
        private Rectangle originalRect, oldRect, newRect;

        public ResizeShapeCommand(Shape shape, Point delta, int handle, Rectangle originalRect)
        {
            this.shape = shape; this.delta = delta; this.handle = handle; this.originalRect = originalRect;
            oldRect = shape.GetBoundingRect();
        }
        public void Execute() { shape.Resize(delta, handle, originalRect); newRect = shape.GetBoundingRect(); }
        public void Undo() { shape.Resize(new Point(oldRect.Width - newRect.Width, oldRect.Height - newRect.Height), handle, newRect); }
        public void Redo() => Execute();
    }
}