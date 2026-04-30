using System.Collections.Generic;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class RemoveAllShapesCommand : ICommand
    {
        private List<Shape> allShapes;
        private List<Shape> backup;
        public RemoveAllShapesCommand(List<Shape> allShapes) { this.allShapes = allShapes; backup = new List<Shape>(); foreach (var s in allShapes) backup.Add(s.Clone()); }
        public void Execute() => allShapes.Clear();
        public void Undo() { allShapes.Clear(); foreach (var s in backup) allShapes.Add(s.Clone()); }
        public void Redo() => Execute();
    }
}
