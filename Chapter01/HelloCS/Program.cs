// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, C#!");
string name = typeof(Program).Namespace ?? "None!";
Console.WriteLine($"Namespace: {name}");

Console.WriteLine("Value is {0}.", 19.8);
Console.WriteLine(args[0]);

throw new Exception();

// int z;
