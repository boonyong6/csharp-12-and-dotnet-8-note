using Ch04Ex02PrimeFactorsLib;

Write("Enter a number to get its prime factors: ");
string? numberText = ReadLine();

if (!int.TryParse(numberText, out int number)) {
    WriteLine($"{numberText} is not a number.");
    return;
}

string result = MathHelper.GetPrimeFactors(number);

WriteLine($"Prime factor(s) of {numberText}: {result}");
