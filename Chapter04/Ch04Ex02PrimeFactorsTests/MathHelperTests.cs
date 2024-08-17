using Ch04Ex02PrimeFactorsLib;

namespace Ch04Ex02PrimeFactorsTests;

public class MathHelperTests
{
    [Theory]
    [InlineData(4, "2 x 2")]
    [InlineData(7, "7")]
    [InlineData(30, "5 x 3 x 2")]
    [InlineData(40, "5 x 2 x 2 x 2")]
    [InlineData(50, "5 x 5 x 2")]
    public void TestPrimeFactors(int number, string expected)
    {
        string actual = MathHelper.GetPrimeFactors(number);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OneHasNoPrimeFactor() {
        int number = 1;

        Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.GetPrimeFactors(number));
    }
}