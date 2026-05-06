using System.Collections.Generic;
using System.Linq;
using PaintApp.Models;

namespace PaintApp.Commands
{
    public class RemoveAllShapesCommand : ICommand
    {
        private List<Shape> allShapes;
        private List<Shape> deletedShapes;
        private List<Layer> layers;
        private Dictionary<Shape, Layer> shapeLayerMap;

        public RemoveAllShapesCommand(List<Shape> allShapes, List<Layer> layers)
        {
            this.allShapes = allShapes;
            this.layers = layers;
            deletedShapes = new List<Shape>();
            shapeLayerMap = new Dictionary<Shape, Layer>();

            // Сохраняем копии фигур и запоминаем, на каком слое какая фигура лежала
            foreach (var layer in layers)
            {
                foreach (var shape in layer.Shapes)
                {
                    var clone = shape.Clone();
                    deletedShapes.Add(clone);
                    shapeLayerMap[clone] = layer;
                }
            }
        }

        public void Execute()
        {
            // Удаляем все фигуры из слоёв и из общего списка
            foreach (var layer in layers)
                layer.ClearShapes();
            allShapes.Clear();
        }

        public void Undo()
        {
            // Восстанавливаем фигуры в правильные слои
            foreach (var clone in deletedShapes)
            {
                if (shapeLayerMap.TryGetValue(clone, out var layer))
                {
                    layer.AddShape(clone);
                    allShapes.Add(clone);
                }
            }
        }

        public void Redo()
        {
            Execute();
        }
    }
}