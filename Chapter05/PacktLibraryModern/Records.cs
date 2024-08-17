#region Init-only properties

namespace Packt.Shared;

public class ImmutablePerson
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}

#endregion

#region Defining record types

public record ImmutableVehicle
{
    public int Wheels { get; init; }
    public string? Color { get; init; }
    public string? Brand { get; init; }
}

#endregion

#region Positional data members in records

// Simpler syntax to define a record that auto-generates the 
// properties, constructor, and deconstructor.
public record ImmutableAnimal(string Name, string Species);

#endregion