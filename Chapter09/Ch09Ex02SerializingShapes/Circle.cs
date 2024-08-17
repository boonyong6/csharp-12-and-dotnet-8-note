namespace Ch09Ex02SerializingShapes;

public class Circle : Shape
{
    public Circle() { }

    public required double Radius { get; init; }
    public override double Area => Math.PI * Math.Pow(Radius, 2);
}
