using System.Collections.Generic;
using System.Linq;

namespace PaintApp.Models
{
    public class LayerManager
    {
        private List<Layer> layers = new List<Layer>();
        private Layer currentLayer;
        private int nextId = 0;

        public LayerManager() { AddLayer("Слой 1"); }
        public List<Layer> Layers => layers;
        public Layer CurrentLayer => currentLayer;
        public void AddLayer(string name) { var l = new Layer(name, nextId++); layers.Add(l); if (currentLayer == null) currentLayer = l; }
        public void RemoveLayer(int id)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Id == id)
                {
                    var shapesToMove = layers[i].Shapes.ToList();
                    var target = layers.FirstOrDefault(l => l.Id != id);
                    if (target != null) foreach (var s in shapesToMove) target.AddShape(s);
                    layers[i].ClearShapes();
                    if (currentLayer == layers[i]) currentLayer = target;
                    layers.RemoveAt(i);
                    break;
                }
            }
        }
        public void SetCurrentLayer(int id) => currentLayer = layers.FirstOrDefault(l => l.Id == id);
        public void MoveShapeToLayer(Shape shape, int targetId)
        {
            if (shape == null) return;
            foreach (var l in layers) l.RemoveShape(shape);
            foreach (var l in layers) if (l.Id == targetId) { l.AddShape(shape); break; }
        }
        public void MoveLayerUp(int id)
        {
            for (int i = 0; i < layers.Count - 1; i++)
                if (layers[i].Id == id) { var t = layers[i]; layers[i] = layers[i + 1]; layers[i + 1] = t; break; }
        }
        public void MoveLayerDown(int id)
        {
            for (int i = 1; i < layers.Count; i++)
                if (layers[i].Id == id) { var t = layers[i]; layers[i] = layers[i - 1]; layers[i - 1] = t; break; }
        }
    public void ReplaceLayers(List<Layer> newLayers)
        {
            foreach (var l in layers)
                l.ClearShapes();
            layers.Clear();
            layers.AddRange(newLayers);
            if (layers.Count > 0)
                currentLayer = layers[0];
            else
                currentLayer = null;
            nextId = layers.Count;
        }
    }

}
