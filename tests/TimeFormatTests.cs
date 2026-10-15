using GrowTime;
using Xunit;

public class TimeFormatTests
{
    [Theory]
    [InlineData(754, "12m 34s")]
    [InlineData(3900, "1h 05m")]
    [InlineData(34, "0m 34s")]
    [InlineData(0.2, "0m 01s")]  // rounds up while still growing
    [InlineData(3599.5, "1h 00m")]
    [InlineData(-5, "0m 00s")]
    public void Format(double seconds, string expected) => Assert.Equal(expected, TimeFormat.Format(seconds));

    [Theory]
    [InlineData(1500, 3000, 50)]
    [InlineData(2999, 3000, 99)]
    [InlineData(4000, 3000, 100)]
    [InlineData(-10, 3000, 0)]
    public void Percent(double elapsed, double total, int expected) => Assert.Equal(expected, TimeFormat.Percent(elapsed, total));
}
