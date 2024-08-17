using System.Xml; // To use XmlDocument.

object height = 1.88; // Storing a double in an object.
object name = "Amir"; // Storing a string in an object.
Console.WriteLine($"{name} is {height} metres tail.");

// int length1 = name.Length; // This gives a compile error!
int length2 = ((string)name).Length; // Cast name to a string.
Console.WriteLine($"{name} has {length2} characters.");

dynamic something;

// Storing an array of int values in a dynamic object.
// An array of any type has a Length property.
something = new[] { 3, 4, 7 };

// Storing an int in a dynamic object.
// int does not have a Length property.
// something = 12;

// Storing a string in a dynamic object.
// string has a Length property.
// something = "Ahmed";

// This compiles but might throw exception at run-time.
Console.WriteLine($"The length of something is {something.Length}");

// Output the type of the something variable.
Console.WriteLine($"something is a {something.GetType()}");

var population = 67_000_000; // 67 milion in UK.
var weight = 1.88; // in kilograms.
var price = 4.99M; // in pounds sterling.
var fruit = "Apples"; // string values use double-quotes.
var letter = 'Z'; // char values use single-quotes.
var happy = true; // Booleans can only be true or false.

// Good use of var because it avoids the repeated type
// as shown in the more verbose second statement.
var xml1 = new XmlDocument(); // Works with C# 3 and later.
XmlDocument xml2 = new XmlDocument(); // Works with all C# versions.
XmlDocument xml3 = new(); // Target-typed new in C# 9 or later.

// Bad use of var because we cannot tell the type, so we
// should use a specific type declaration as shown in
// the second statement.
var file1 = File.CreateText("something1.txt");
StreamWriter file2 = File.CreateText("something2.txt");

Console.WriteLine($"default(int) = {default(int)}");
Console.WriteLine($"default(bool) = {default(bool)}");
Console.WriteLine($"default(DateTime) = {default(DateTime)}");
Console.WriteLine($"default(string) = {default(string)}");

int number = 13;
Console.WriteLine($"number set to: {number}");
number = default;
Console.WriteLine($"number reset to its default: {number}");

