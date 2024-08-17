namespace Ch06Ex02Inheritance;

public class Circle : Shape
{
    private readonly double _radius;

    public override double Area => Math.PI * Math.Pow(_radius, 2);

    public Circle(double radius)
    {
        _radius = radius;
        Height = radius * 2;
        Width = Height;
    }
}
