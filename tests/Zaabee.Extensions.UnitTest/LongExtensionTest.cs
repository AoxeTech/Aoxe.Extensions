namespace Zaabee.Extensions.UnitTest;

public class LongExtensionTest
{
    [Theory]
    [InlineData(long.MaxValue, 32)]
    [InlineData(long.MaxValue, 36)]
    [InlineData(long.MaxValue, 62)]
    [InlineData(long.MinValue + 1, 32)]
    [InlineData(long.MinValue + 1, 36)]
    [InlineData(long.MinValue + 1, 62)]
    public void Test(long value, int radix)
    {
        var str = value.ToString(radix);
        var result = str.ToLong(radix);
        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(long.MaxValue, NumerationSystem.Binary)]
    [InlineData(long.MaxValue, NumerationSystem.Decimalism)]
    [InlineData(long.MaxValue, NumerationSystem.Duotricemary)]
    [InlineData(long.MaxValue, NumerationSystem.Hexadecimal)]
    [InlineData(long.MaxValue, NumerationSystem.Octal)]
    [InlineData(long.MaxValue, NumerationSystem.SixtyTwoAry)]
    [InlineData(long.MaxValue, NumerationSystem.ThirtySixAry)]
    [InlineData(long.MinValue + 1, NumerationSystem.Binary)]
    [InlineData(long.MinValue + 1, NumerationSystem.Decimalism)]
    [InlineData(long.MinValue + 1, NumerationSystem.Duotricemary)]
    [InlineData(long.MinValue + 1, NumerationSystem.Hexadecimal)]
    [InlineData(long.MinValue + 1, NumerationSystem.Octal)]
    [InlineData(long.MinValue + 1, NumerationSystem.SixtyTwoAry)]
    [InlineData(long.MinValue + 1, NumerationSystem.ThirtySixAry)]
    public void TestByNumerationSystem(long value, NumerationSystem numerationSystem)
    {
        var str = value.ToString(numerationSystem);
        var result = str.ToLong(numerationSystem);
        Assert.Equal(value, result);
    }
}