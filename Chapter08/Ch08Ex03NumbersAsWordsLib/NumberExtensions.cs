using System.Numerics;
using System.Text;

namespace Ch08Ex03NumbersAsWordsLib;

public static class NumberExtensions
{
    private static readonly string[] _onesDigitWords =
        [
            "zero", "one", "two", "three", "four", "five",
            "six", "seven", "eight", "nine"
        ];

    private static readonly string[] _tenToNineteenWords =
        [
            "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen",
            "sixteen", "seventeen", "eighteen", "nineteen"
        ];

    private static readonly string[] _tensDigitPrefixes =
        [
            string.Empty, string.Empty, "twenty", "thirty", "forty", "fifty",
            "sixty", "seventy", "eighty", "ninety"
        ];

    private static readonly string[] _thousandSeparatedNumberNames =
        [
            string.Empty, "thousand", "million", "billion", "trillion", "quadrillion", 
            "quintillion"
        ];

    public static string ToWords(this BigInteger number)
    {
        if (number == 0)
        {
            return _onesDigitWords[0];
        }

        if (number < 0)
        {
            return $"negative {ToWords(-number)}";
        }

        if (number <= 999)
        {
            return ThreeDigitsToWords((int)number);
        }

        int[] numberGroups = new int[_thousandSeparatedNumberNames.Length];
        int groupCount = 0;

        while (number > 0)
        {
            numberGroups[groupCount] = (int)(number % 1000);
            number /= 1000;
            groupCount++;      
        }

        StringBuilder sb = new();

        for (int i = 0; i < groupCount; i++)
        {
            if (numberGroups[i] == 0)
            {
                continue;
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, ", ");
            }

            if (i > 0)
            {
                sb.Insert(0, $" {_thousandSeparatedNumberNames[i]}");
            }

            sb.Insert(0, ThreeDigitsToWords(numberGroups[i]));
        }

        return sb.ToString();
    }

    public static string ToWords(this long number)
    {
        return ((BigInteger)number).ToWords();
    }

    public static string ToWords(this int number)
    {
        return ((BigInteger)number).ToWords();
    }

    private static string ThreeDigitsToWords(int number)
    {
        if (number <= 99)
        {
            return TwoDigitsToWords(number);
        }

        StringBuilder sb = new($"{_onesDigitWords[number / 100]} hundred");

        if (number % 100 > 0)
        {
            sb.Append($" and {TwoDigitsToWords(number % 100)}");
        }

        return sb.ToString();
    }

    private static string TwoDigitsToWords(int number)
    {
        if (number <= 9)
        {
            return _onesDigitWords[number];
        }

        if (number <= 19)
        {
            return _tenToNineteenWords[number % 10];
        }

        return TwentyToNinetyNineToWords(number);
    }

    private static string TwentyToNinetyNineToWords(int number)
    {
        StringBuilder sb = new($"{_tensDigitPrefixes[number / 10]}");

        if (number % 10 > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append('-');
            }

            sb.Append($"{_onesDigitWords[number % 10]}");
        }

        return sb.ToString();
    }
}
