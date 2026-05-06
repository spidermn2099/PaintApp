using System;
using PaintApp.Models;
using PaintApp.Plugins;
using PaintApp.Helpers;

namespace TrapezoidPlugin
{
    public class TrapezoidPlugin : IShapePlugin
    {
        public string ShapeTypeName => "Трапеция";

        public Shape CreateInstance()
        {
            var t = new Trapezoid();
            t.TopWidth = 80;
            t.BottomWidth = 120;
            t.Height = 60;
            return t;
        }

        static TrapezoidPlugin()
        {
            SerializationHelper.RegisterType("Trapezoid", data => new Trapezoid());
        }
    }
}