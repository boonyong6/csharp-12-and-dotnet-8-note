#region Wrapping error-prone code in a try block

// WriteLine("Before parsing");
// Write("What is your age? ");
// string? input = ReadLine();

// try
// {
//     int age = int.Parse(input!);
//     WriteLine($"You are {age} years old.");
// }
// catch (OverflowException)
// {
//     WriteLine("Your age is a valid number format but it is either too big or small.");  
// }
// catch (FormatException)
// {
//     WriteLine("The age you entered is not a valid number format.");
// }
// catch (Exception ex)
// {
//     WriteLine($"{ex.GetType()} says {ex.Message}");
// }
// WriteLine("After parsing");

#endregion

#region Throwing overflow exceptions with the checked statement

WriteLine();

try
{    
    checked
    {
        int x = int.MaxValue - 1;
        WriteLine($"Initial value: {x}");
        x++;
        WriteLine($"After incrementing: {x}");
        x++;
        WriteLine($"After incrementing: {x}");
        x++;
        WriteLine($"After incrementing: {x}");
    }
}
catch (OverflowException)
{
    WriteLine("The code overflowed but I caught the exception.");
}

#endregion

#region Disabling compiler overflow checks with the unchecked statement

WriteLine();

unchecked
{
    int y = int.MaxValue + 1;
    WriteLine($"Initial value: {y}");
    y--;
    WriteLine($"After decrementing: {y}");
    y--;
    WriteLine($"After decrementing: {y}");
}

#endregion

