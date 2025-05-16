namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringTryParseTests
{
    #region Numeric Parsing Tests

    [Theory]
    [InlineData("123", 123)]
    [InlineData("-42", -42)]
    [InlineData("invalid", 5)]
    [InlineData(null, 5)]
    public void TryParseInt_HandlesVariousCases(string? input, int expected)
    {
        var result = input.TryParseInt(defaultValue: 5);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDecimal_WithCurrencySymbol()
    {
        var culture = CultureInfo.CreateSpecificCulture("en-US");
        var result = "$1,234.56".TryParseDecimal(styles: NumberStyles.Currency, provider: culture);
        Assert.Equal(1234.56m, result);
    }

    [Fact]
    public void TryParseDouble_ScientificNotation()
    {
        var result = "1.23e5".TryParseDouble();
        Assert.Equal(123000, result);
    }

    #endregion

    #region Boolean Parsing Tests

    [Theory]
    [InlineData("True", true)]
    [InlineData("false", false)]
    [InlineData(null, false)]
    [InlineData("Invalid", false)]
    public void TryParseBool_HandlesCases(string? input, bool expected)
    {
        var result = input.TryParseBool(defaultValue: expected);

        Assert.Equal(expected, result);
    }

    #endregion

    #region DateTime Parsing Tests

    [Fact]
    public void TryParseDateTime_WithCultureSpecificFormat()
    {
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var result = "15/07/2023 14:30".TryParseDateTime(provider: culture);
        Assert.Equal(new DateTime(2023, 7, 15, 14, 30, 0), result);
    }

    [Fact]
    public void TryParseDateTime_HandlesUtc()
    {
        var result = "2023-07-15T12:00:00Z".TryParseDateTime(
            styles: DateTimeStyles.AdjustToUniversal
        );
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    #endregion

    #region Enum Parsing Tests

    public enum TestEnum
    {
        Value1,
        Value2
    }

    [Theory]
    [InlineData("Value1", TestEnum.Value1)]
    [InlineData("value2", TestEnum.Value2, true)]
    [InlineData("Invalid", TestEnum.Value1)]
    public void TryParseEnum_HandlesCases(string input, TestEnum expected, bool ignoreCase = false)
    {
        var result = input.TryParseEnum(defaultValue: TestEnum.Value1, ignoreCase: ignoreCase);
        Assert.Equal(expected, result);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void TryParseByte_WithHexNumber()
    {
        var result = "FF".TryParseByte(styles: NumberStyles.HexNumber);
        Assert.Equal(255, result);
    }

    [Fact]
    public void TryParseDateTimeOffset_WithOffset()
    {
        var result = "2023-07-15T12:00:00+02:00".TryParseDateTimeOffset();
        Assert.Equal(TimeSpan.FromHours(2), result.Offset);
    }

    [Fact]
    public void TryParseDecimal_WithWhiteSpace()
    {
        var result = " 123.45 ".TryParseDecimal();
        Assert.Equal(123.45m, result);
    }

    #endregion

    #region Null Handling Tests

    [Fact]
    public void AllMethods_NullInputReturnsDefault()
    {
        string? nullInput = null;
        Assert.Equal(-5, nullInput.TryParseSbyte(-5));
        Assert.Equal(100, nullInput.TryParseByte(100));
        Assert.Equal(default(DateTime), nullInput.TryParseDateTime());
        Assert.False(nullInput.TryParseBool());
        Assert.True("a".TryParseBool(true));
    }

    #endregion

    #region Culture-Specific Number Tests

    [Fact]
    public void TryParseDouble_GermanCulture()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var result = "1.234,56".TryParseDouble(provider: culture);
        Assert.Equal(1234.56, result);
    }

    #endregion
}
