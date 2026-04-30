using System.Collections.Generic;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class RemoveShapeCommand : ICommand
    {
        private List<Shape> toRemove;
        private List<Shape> allShapes;
        public RemoveShapeCommand(List<Shape> toRemove, List<Shape> allShapes) { this.toRemove = new List<Shape>(toRemove); this.allShapes = allShapes; }
        public void Execute() { foreach (var s in toRemove) allShapes.Remove(s); }
        public void Undo() { foreach (var s in toRemove) allShapes.Add(s); }
        public void Redo() => Execute();
    }
}