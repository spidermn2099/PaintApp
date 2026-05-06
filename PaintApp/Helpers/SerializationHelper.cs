using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using PaintApp.Models;

namespace PaintApp.Helpers
{
    public class SerializablePoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class SerializableShape
    {
        public string Type { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }

    public class SerializableLayer
    {
        public string Name { get; set; }
        public bool Visible { get; set; }
        public List<SerializableShape> Shapes { get; set; }
    }

    public class SerializableDocument
    {
        public List<SerializableLayer> Layers { get; set; }
    }

    public static class SerializationHelper
    {
        public static Dictionary<string, Func<Dictionary<string, object>, Shape>> TypeResolvers { get; } = new();

        public static void RegisterType(string typeName, Func<Dictionary<string, object>, Shape> creator)
        {
            if (!TypeResolvers.ContainsKey(typeName))
                TypeResolvers[typeName] = creator;
        }

        public static void SaveToFile(string filename, List<Layer> layers)
        {
            var doc = new SerializableDocument { Layers = new List<SerializableLayer>() };

            foreach (var layer in layers)
            {
                var sl = new SerializableLayer
                {
                    Name = layer.Name,
                    Visible = layer.Visible,
                    Shapes = new List<SerializableShape>()
                };

                foreach (var shape in layer.Shapes)
                {
                    var data = new Dictionary<string, object>();
                    shape.SaveToDictionary(data);
                    sl.Shapes.Add(new SerializableShape
                    {
                        Type = shape.GetType().Name,
                        Data = data
                    });
                }
                doc.Layers.Add(sl);
            }

            string json = JsonConvert.SerializeObject(doc, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static List<Layer> LoadFromFile(string filename)
        {
            string json = File.ReadAllText(filename);
            var doc = JsonConvert.DeserializeObject<SerializableDocument>(json);
            var newLayers = new List<Layer>();
            int layerId = 0;

            foreach (var sl in doc.Layers)
            {
                var layer = new Layer(sl.Name, layerId++);
                layer.Visible = sl.Visible;

                foreach (var ss in sl.Shapes)
                {
                    if (!TypeResolvers.ContainsKey(ss.Type))
                        continue; // Неизвестный тип (плагин не загружен)

                    var shape = TypeResolvers[ss.Type](ss.Data);
                    if (shape != null)
                    {
                        shape.LoadFromDictionary(ss.Data);
                        layer.AddShape(shape);
                    }
                }
                newLayers.Add(layer);
            }
            return newLayers;
        }
    }
}