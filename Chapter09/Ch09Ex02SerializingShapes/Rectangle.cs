namespace Ch09Ex02SerializingShapes;

public class Rectangle : Shape
{
    public Rectangle() { }

    public required double Height { get; init; }
    public required double Width { get; init; }
    public override double Area => Height * Width;
}
