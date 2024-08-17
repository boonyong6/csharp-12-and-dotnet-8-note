Console.WriteLine("Enter a name: ");
string? name = Console.ReadLine(); // Must declare the variable as nullable to remove the warning.
if (name is null) return; // Must check for null to remove the warning.
Console.WriteLine($"Hello, {name} has {name.Length} characters!");
