using System.Numerics;
using Ch08Ex03NumbersAsWordsLib;

namespace Ch08Ex03NumbersAsWordsTests;

public class NumberExtensionsTests
{
    [Theory]
    [InlineData(5, "five")]
    [InlineData(6, "six")]
    [InlineData(0, "zero")]
    public void ToWords_OnesDigit_ReturnNumberText(int number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(10, "ten")]
    [InlineData(11, "eleven")]
    [InlineData(16, "sixteen")]
    [InlineData(12, "twelve")]
    public void ToWords_TenToNineteen_ReturnNumberText(int number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(66, "sixty-six")]
    [InlineData(56, "fifty-six")]
    [InlineData(21, "twenty-one")]
    [InlineData(50, "fifty")]
    public void ToWords_TensDigit_ReturnNumberText(int number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(666, "six hundred and sixty-six")]
    [InlineData(100, "one hundred")]
    [InlineData(106, "one hundred and six")]
    [InlineData(160, "one hundred and sixty")]
    [InlineData(616, "six hundred and sixteen")]
    public void ToWords_HundredsDigit_ReturnNumberText(int number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(6000, "six thousand")]
    [InlineData(6_006_611, "six million, six thousand, six hundred and eleven")]
    [InlineData(18_000_000, "eighteen million")]
    [InlineData(int.MaxValue, "two billion, one hundred and forty-seven million, four hundred and eighty-three thousand, six hundred and forty-seven")]
    public void ToWords_ThousandAndAbove_ReturnNumberText(int number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-6_006_612, "negative six million, six thousand, six hundred and twelve")]
    [InlineData(-6, "negative six")]
    public void ToWords_NegativeNumber_ReturnNumberText(int number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(18_446_002_032_011_000_007, "eighteen quintillion, four hundred and forty-six quadrillion, two trillion, thirty-two billion, eleven million, seven")]
    public void ToWords_BigInteger_ReturnNumberText(BigInteger number, string expected)
    {
        string actual = number.ToWords();
 
        Assert.Equal(expected, actual);
    }
}