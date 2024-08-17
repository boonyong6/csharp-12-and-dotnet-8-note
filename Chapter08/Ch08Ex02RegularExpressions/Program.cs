using System.Text.RegularExpressions;

Regex regex = ContainsDigit();

do
{
    WriteLine("The default regular expression checks for at least one digit.");
    
    Write("Enter a regular expression (or press ENTER to use the default): ");
    string regexText = ReadLine()!;
    if (!string.IsNullOrWhiteSpace(regexText))
    {
        regex = new(regexText);
    }
    
    Write("Enter some input: ");
    string input = ReadLine()!;
    
    bool isMatch = regex.IsMatch(input);
    WriteLine($"\"{input}\" matches \"{regex}\"? {isMatch}");
    
    WriteLine("Press ESC to end or any key to try again.");

} while (ReadKey(intercept: true).Key is not ConsoleKey.Escape);
