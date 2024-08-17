#region Equality of record types

namespace Packt.Shared;

public class AnimalClass
{
    public string? Name { get; set; }
}

public record AnimalRecord
{
    public string? Name { get; set; }
}

#endregion