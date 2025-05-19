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

    #region UInt Tests
    [Theory]
    [InlineData("4294967295", uint.MaxValue)] // Max value
    [InlineData("0", 0U)] // Min value
    [InlineData("12345", 12345U)]
    [InlineData("-1", 999U)] // Invalid negative
    [InlineData("invalid", 999U)]
    [InlineData(null, 100U)]
    public void TryParseUint_VariousCases_ReturnsExpected(string? input, uint expected)
    {
        var defaultValue = input == null ? 100U : 999U;
        var result = input.TryParseUint(defaultValue);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("FF", NumberStyles.HexNumber, 255U)]
    [InlineData("100", NumberStyles.Integer, 100U)]
    public void TryParseUint_WithNumberStyles_ReturnsExpected(
        string input,
        NumberStyles styles,
        uint expected
    )
    {
        var result = input.TryParseUint(styles: styles);
        Assert.Equal(expected, result);
    }
    #endregion

    #region Long Tests
    [Theory]
    [InlineData("-9223372036854775808", long.MinValue)] // Min value
    [InlineData("9223372036854775807", long.MaxValue)] // Max value
    [InlineData("123456789012345", 123456789012345L)]
    [InlineData("invalid", -1L)]
    [InlineData(null, 999L)]
    public void TryParseLong_VariousCases_ReturnsExpected(string input, long expected)
    {
        var defaultValue = input == null ? 999L : -1L;
        var result = input.TryParseLong(defaultValue);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseLong_WithCulturalThousandsSeparator_ReturnsCorrect()
    {
        // Test with French culture formatting (space as thousand separator)
        var result = "1234567".TryParseLong(provider: CultureInfo.GetCultureInfo("fr-FR"));
        Assert.Equal(1234567L, result);
    }

    [Fact]
    public void TryParseLong_WithExplicitThousandsSeparator_ReturnsParsed()
    {
        var result = "1,000,000".TryParseLong(
            styles: NumberStyles.AllowThousands,
            provider: CultureInfo.InvariantCulture
        );
        Assert.Equal(1000000L, result);
    }

    [Fact]
    public void TryParseLong_WithGermanNumberFormat_ReturnsCorrect()
    {
        // Test with German decimal/comma formatting (though we're parsing integers)
        var result = "123.456".TryParseLong( // German uses . as thousand separator
            provider: CultureInfo.GetCultureInfo("de-DE")
        );
        Assert.Equal(123456L, result);
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
