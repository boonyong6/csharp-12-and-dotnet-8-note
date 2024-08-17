using System.Xml.Serialization;

namespace Ch09Ex02SerializingShapes;

[XmlInclude(typeof(Circle))]
[XmlInclude(typeof(Rectangle))]
public abstract class Shape
{
    public required string Color { get; set; }
    public abstract double Area { get; }
}
