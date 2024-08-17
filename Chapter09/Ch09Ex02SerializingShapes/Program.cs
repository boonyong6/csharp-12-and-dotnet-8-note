using System.Xml.Serialization;
using Ch09Ex02SerializingShapes;

// Create a list of Shapes to serialize.
List<Shape> listOfShapes = new()
{
    new Circle { Color = "Red", Radius = 2.5 },
    new Rectangle { Color = "Blue", Height = 20.0, Width = 10.0 },
    new Circle { Color = "Green", Radius = 8.0 },
    new Circle { Color = "Purple", Radius = 12.3 },
    new Rectangle { Color = "Blue", Height = 45.0, Width = 18.0 },
};

string path = Combine(CurrentDirectory, "shapes.xml");
XmlSerializer serializerXml = new(listOfShapes.GetType());

await using (FileStream fileXml = File.Create(path))
{
    serializerXml.Serialize(fileXml, listOfShapes);
}

await using (FileStream fileXml = File.Open(path, FileMode.Open))
{
    List<Shape> loadedShapesXml = serializerXml.Deserialize(fileXml)
        as List<Shape> ?? new();

    WriteLine("Loading shapes from XML:");
    foreach (Shape item in loadedShapesXml)
    {
        WriteLine("{0} is {1} and has an area of {2:N2}",
            item.GetType().Name, item.Color, item.Area);
    }
}
