using PaintApp.Models;

namespace PaintApp.Plugins
{
    public interface IShapePlugin
    {
        string ShapeTypeName { get; }
        Shape CreateInstance();
    }
}