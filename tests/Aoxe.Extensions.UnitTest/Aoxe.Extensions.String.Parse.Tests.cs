namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringParseTests
{
    #region Common Test Scenarios

    [Theory]
    [InlineData(null)]
    public void AllParseMethods_ShouldThrowArgumentNull_WhenInputIsNull(string? nullInput)
    {
        // Test all parse methods with null input
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseSbyte());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseByte());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseShort());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseUshort());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseInt());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseUint());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseLong());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseUlong());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseFloat());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseDouble());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseDecimal());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseBool());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseDateTime());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseDateTimeOffset());
        Assert.Throws<ArgumentNullException>(() => nullInput.ParseEnum<DayOfWeek>());
    }

    #endregion

    #region Numeric Type Tests

    // Test data for sbyte (-128 to 127)
    [Theory]
    [InlineData("-128", -128)]
    [InlineData("127", 127)]
    [InlineData("0", 0)]
    public void ParseSbyte_ValidInput_ReturnsCorrectValue(string input, sbyte expected)
    {
        var result = input.ParseSbyte();
        Assert.Equal(expected, result);
    }

    // Test data for byte (0 to 255)
    [Theory]
    [InlineData("0", 0)]
    [InlineData("255", 255)]
    [InlineData("128", 128)]
    public void ParseByte_ValidInput_ReturnsCorrectValue(string input, byte expected)
    {
        var result = input.ParseByte();
        Assert.Equal(expected, result);
    }

    // Test different number styles for int
    [Theory]
    [InlineData("(100)", NumberStyles.AllowParentheses, -100)]
    [InlineData("$1,000", NumberStyles.AllowCurrencySymbol | NumberStyles.Number, 1000)]
    public void ParseInt_WithNumberStyles_ReturnsCorrectValue(
        string input,
        NumberStyles style,
        int expected
    )
    {
        // Use explicit culture that matches the test data format
        var culture = CultureInfo.GetCultureInfo("en-US");
        var result = input.ParseInt(style, culture);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("invalid")]
    public void ParseLong_InvalidFormat_ThrowsFormatException(string input)
    {
        Assert.Throws<FormatException>(() => input.ParseLong());
    }

    [Theory]
    [InlineData("99999999999999999999")] // 20 digits (max long is 9,223,372,036,854,775,807)
    [InlineData("-99999999999999999999")]
    public void ParseLong_OverflowValues_ThrowsOverflowException(string input)
    {
        Assert.Throws<OverflowException>(() => input.ParseLong());
    }

    #endregion

    #region Floating Point Tests

    [Theory]
    // Regular value with precision check
    [InlineData("1.23", 1.23f, 2)]
    // Use string representations of actual min/max values
    [InlineData("-3.40282347E+38", float.MinValue, 0)]
    [InlineData("3.40282347E+38", float.MaxValue, 0)]
    public void ParseFloat_ValidInput_ReturnsCorrectValue(
        string input,
        float expected,
        int precision
    )
    {
        var result = input.ParseFloat();

        if (precision > 0)
        {
            // For normal values, check with limited precision
            Assert.Equal(expected, result, precision);
        }
        else
        {
            // For min/max values, verify exact match
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData("1.7976931348623157E+308", double.MaxValue)]
    [InlineData("-1.7976931348623157E+308", double.MinValue)]
    public void ParseDouble_ExtremeValues_ReturnsCorrectValue(string input, double expected)
    {
        var result = input.ParseDouble();
        Assert.Equal(expected, result);
    }

    #endregion

    #region Boolean Tests

    [Theory]
    [InlineData("True", true)]
    [InlineData("False", false)]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void ParseBool_ValidInput_ReturnsCorrectValue(string input, bool expected)
    {
        var result = input.ParseBool();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Yes")]
    [InlineData("No")]
    [InlineData("1")]
    [InlineData("0")]
    public void ParseBool_InvalidInput_ThrowsFormatException(string input)
    {
        Assert.Throws<FormatException>(() => input.ParseBool());
    }

    #endregion

    #region DateTime Tests

    [Fact]
    public void ParseDateTime_WithCustomCulture_ReturnsCorrectValue()
    {
        var culture = new CultureInfo("fr-FR");
        var dateStr = "12/07/2023 14:30:00";

        var result = dateStr.ParseDateTime(culture);

        Assert.Equal(new DateTime(2023, 7, 12, 14, 30, 0), result);
    }

    [Theory]
    [InlineData("2023-12-31T23:59:59.9999999Z")]
    [InlineData("2023-12-31T23:59:59+08:00")]
    public void ParseDateTimeOffset_ValidFormats_ReturnsCorrectValue(string input)
    {
        var result = input.ParseDateTimeOffset();
        Assert.IsType<DateTimeOffset>(result);
    }

    #endregion

    #region Enum Tests

    public enum TestEnum
    {
        First,
        Second,
        Third
    }

    [Theory]
    [InlineData("First", TestEnum.First)]
    [InlineData("SECOND", TestEnum.Second, true)]
    [InlineData("third", TestEnum.Third, true)]
    public void ParseEnum_ValidValues_ReturnsCorrectValue(
        string input,
        TestEnum expected,
        bool ignoreCase = false
    )
    {
        var result = input.ParseEnum<TestEnum>(ignoreCase);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseEnum_NonEnumType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => "123".ParseEnum(typeof(int)));
    }

    #endregion

    #region Culture-Specific Tests

    [Fact]
    public void ParseDecimal_WithGermanCulture_ReturnsCorrectValue()
    {
        var culture = new CultureInfo("de-DE");
        var value = "1.234,56"; // German uses . as thousand separator and , as decimal

        var result = value.ParseDecimal(provider: culture);

        Assert.Equal(1234.56m, result);
    }

    [Fact]
    public void ParseDouble_WithInvariantCulture_ReturnsCorrectValue()
    {
        var value = "1234567.89";

        var result = value.ParseDouble(provider: CultureInfo.InvariantCulture);

        Assert.Equal(1234567.89, result);
    }

    #endregion

    #region Edge Case Tests

    [Theory]
    [InlineData("2147483647", int.MaxValue)]
    [InlineData("-2147483648", int.MinValue)]
    public void ParseInt_EdgeCases_ReturnsCorrectValue(string input, int expected)
    {
        var result = input.ParseInt();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("18446744073709551615", ulong.MaxValue)]
    public void ParseUlong_MaxValue_ReturnsCorrectValue(string input, ulong expected)
    {
        var result = input.ParseUlong();
        Assert.Equal(expected, result);
    }

    #endregion
}
