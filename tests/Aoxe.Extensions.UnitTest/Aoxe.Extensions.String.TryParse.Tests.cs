namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringTryParseTests
{
    private readonly CultureInfo _originalCulture;
    private readonly CultureInfo _originalUICulture;

    public AoxeExtensionsStringTryParseTests()
    {
        _originalCulture = CultureInfo.CurrentCulture;
        _originalUICulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
    }

    public void Dispose()
    {
        CultureInfo.CurrentCulture = _originalCulture;
        CultureInfo.CurrentUICulture = _originalUICulture;
    }

    #region Numeric Type Tests
    [Theory]
    [InlineData("123", 123)]
    [InlineData("-128", -128)] // sbyte.MinValue
    [InlineData("127", 127)] // sbyte.MaxValue
    [InlineData("invalid", 0)]
    [InlineData(null, -1)]
    public void TryParseSbyte_VariousInputs_ReturnsExpected(string input, sbyte expected)
    {
        var result = input.TryParseSbyte(defaultValue: (sbyte)(input == null ? -1 : 0));
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("FF", NumberStyles.HexNumber, 255)]
    [InlineData("100", NumberStyles.Integer, 100)]
    public void TryParseByte_WithNumberStyles_ReturnsExpected(
        string input,
        NumberStyles styles,
        byte expected
    )
    {
        var result = input.TryParseByte(styles: styles);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseShort_WithCultureFormat_ReturnsParsed()
    {
        var result = "1.234".TryParseShort(
            styles: NumberStyles.AllowThousands,
            provider: CultureInfo.GetCultureInfo("de-DE")
        );
        Assert.Equal(1234, result);
    }

    [Theory]
    [InlineData("65535", ushort.MaxValue)]
    [InlineData("0", 0)]
    public void TryParseUshort_BoundaryValues_ReturnsExpected(string input, ushort expected)
    {
        var result = input.TryParseUshort();
        Assert.Equal(expected, result);
    }
    #endregion

    #region Floating Point Tests
    [Fact]
    public void TryParseFloat_WithScientificNotation_ReturnsCorrect()
    {
        var result = "1.23e-4".TryParseFloat();
        Assert.Equal(0.000123f, result, 6);
    }

    [Fact]
    public void TryParseDouble_WithThousandsSeparator_ReturnsCorrect()
    {
        var result = "1,234.56".TryParseDouble(provider: CultureInfo.InvariantCulture);
        Assert.Equal(1234.56, result);
    }

    [Fact]
    public void TryParseDecimal_WithCurrencySymbol_ReturnsCorrect()
    {
        var result = "$12.34".TryParseDecimal(
            styles: NumberStyles.Currency,
            provider: CultureInfo.GetCultureInfo("en-US")
        );
        Assert.Equal(12.34m, result);
    }
    #endregion

    #region Boolean Tests
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("invalid", true)]
    [InlineData(null, false)]
    public void TryParseBool_VariousInputs_ReturnsExpected(string input, bool expected)
    {
        var result = input.TryParseBool(expected);
        Assert.Equal(expected, result);
    }
    #endregion

    #region DateTime Tests
    [Fact]
    public void TryParseDateTime_WithCustomFormat_ReturnsCorrect()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ja-JP");
        var result = "2023/10/10".TryParseDateTime();
        Assert.Equal(new DateTime(2023, 10, 10), result);
    }

    [Fact]
    public void TryParseDateTimeOffset_WithUtc_ReturnsCorrect()
    {
        var result = "2023-10-10T12:34:56Z".TryParseDateTimeOffset(
            styles: DateTimeStyles.AssumeUniversal
        );
        Assert.Equal(DateTimeOffset.UtcNow.Offset, result.Offset);
    }
    #endregion

    #region Enum Tests
    public enum TestEnum
    {
        Default,
        First,
        Second
    }

    [Theory]
    [InlineData("First", TestEnum.First)]
    [InlineData("FIRST", TestEnum.First, true)]
    [InlineData("Third", TestEnum.Default)]
    public void TryParseEnum_VariousCases_ReturnsExpected(
        string input,
        TestEnum expected,
        bool ignoreCase = false
    )
    {
        var result = input.TryParseEnum(TestEnum.Default, ignoreCase);
        Assert.Equal(expected, result);
    }
    #endregion

    #region Edge Case Tests
    [Fact]
    public void TryParseInt_MaxValue_ReturnsCorrect()
    {
        var result = int.MaxValue.ToString().TryParseInt();
        Assert.Equal(int.MaxValue, result);
    }

    [Fact]
    public void TryParseUlong_WithWrapAround_ReturnsDefault()
    {
        var result = "-1".TryParseUlong();
        Assert.Equal(0UL, result);
    }

    [Fact]
    public void TryParseDateTime_InvalidWithCustomDefault_ReturnsDefault()
    {
        var defaultValue = new DateTime(2000, 1, 1);
        var result = "invalid".TryParseDateTime(defaultValue);
        Assert.Equal(defaultValue, result);
    }
    #endregion
}
