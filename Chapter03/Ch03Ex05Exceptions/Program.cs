Write("Enter a number between 0 and 255: ");
string? firstNumberText = ReadLine();

Write("Enter another number between 0 and 255: ");
string? secondNumberText = ReadLine();

try
{
    byte firstNumber = byte.Parse(firstNumberText!);
    byte secondNumber = byte.Parse(secondNumberText!);

    int result = firstNumber / secondNumber;

    WriteLine($"{firstNumberText} divided by {secondNumberText} is {result}");
}
catch (System.Exception ex)
{
    WriteLine($"{ex.GetType()}: {ex.Message}");
}
