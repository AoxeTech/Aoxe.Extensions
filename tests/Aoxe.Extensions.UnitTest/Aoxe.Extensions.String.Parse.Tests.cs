namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringParseTests
{
    #region Common Test Cases
    [Fact]
    public void ParseMethods_NullInput_ThrowsArgumentNull()
    {
        string? nullString = null;

        Assert.Throws<ArgumentNullException>(() => nullString.ParseSbyte());
        Assert.Throws<ArgumentNullException>(() => nullString.ParseByte());
        Assert.Throws<ArgumentNullException>(() => nullString.ParseDecimal());
        Assert.Throws<ArgumentNullException>(() => nullString.ParseEnum<DayOfWeek>());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid")]
    public void ParseMethods_InvalidFormat_ThrowsFormatException(string invalidValue)
    {
        Assert.Throws<FormatException>(() => invalidValue.ParseInt());
        Assert.Throws<FormatException>(() => invalidValue.ParseDouble());
        Assert.Throws<FormatException>(() => invalidValue.ParseDateTime());
    }
    #endregion

    #region Numeric Type Tests
    [Theory]
    [InlineData("-128", NumberStyles.Integer, -128)] // sbyte min
    [InlineData("127", NumberStyles.Integer, 127)] // sbyte max
    [InlineData("7F", NumberStyles.HexNumber, 127)] // hex format
    public void ParseSbyte_ValidInput_ReturnsCorrectValue(
        string input,
        NumberStyles style,
        sbyte expected
    )
    {
        var result = input.ParseSbyte(style, CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("255", 255)] // byte max
    [InlineData("0", 0)] // byte min
    [InlineData("100", 100)]
    public void ParseByte_ValidInput_ReturnsCorrectValue(string input, byte expected)
    {
        var result = input.ParseByte();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("32767", short.MaxValue)] // short max
    [InlineData("-32768", short.MinValue)] // short min
    public void ParseShort_EdgeCases_ReturnsCorrectValue(string input, short expected)
    {
        var result = input.ParseShort();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("79228162514264337593543950335", NumberStyles.Number, "en-US")] // decimal max
    [InlineData("-79228162514264337593543950335", NumberStyles.Number, "en-US")] // decimal min
    public void ParseDecimal_ExtremeValues_ReturnsCorrect(
        string input,
        NumberStyles style,
        string culture
    )
    {
        var provider = new CultureInfo(culture);
        var result = input.ParseDecimal(style, provider);
        Assert.Equal(decimal.Parse(input, style, provider), result);
    }
    #endregion

    #region Boolean Tests
    [Theory]
    [InlineData("True", true)]
    [InlineData("False", false)]
    [InlineData("true", true)] // case sensitivity
    [InlineData("false", false)]
    public void ParseBool_ValidInput_ReturnsCorrect(string input, bool expected)
    {
        var result = input.ParseBool();
        Assert.Equal(expected, result);
    }
    #endregion

    #region DateTime Tests
    [Theory]
    [InlineData("2023-12-31T23:59:59", "en-US")]
    [InlineData("31/12/2023 23:59:59", "fr-FR")]
    public void ParseDateTime_WithCulture_ReturnsCorrect(string input, string culture)
    {
        var provider = new CultureInfo(culture);
        var result = input.ParseDateTime(provider);
        Assert.Equal(DateTime.Parse(input, provider), result);
    }

    [Fact]
    public void ParseDateTime_WithStyles_HandlesAdjustment()
    {
        var input = "2023-02-29"; // Invalid date
        Assert.Throws<FormatException>(
            () => input.ParseDateTime(styles: DateTimeStyles.AllowInnerWhite)
        );
    }
    #endregion

    #region Enum Tests
    public enum TestEnum
    {
        First,
        Second
    }

    [Theory]
    [InlineData("First", false, TestEnum.First)]
    [InlineData("SECOND", true, TestEnum.Second)]
    public void ParseEnum_ValidValues_ReturnsCorrect(
        string input,
        bool ignoreCase,
        TestEnum expected
    )
    {
        var result = input.ParseEnum<TestEnum>(ignoreCase);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseEnum_InvalidType_Throws()
    {
        Assert.Throws<ArgumentException>(() => "1".ParseEnum(typeof(int)));
    }
    #endregion

    #region Special Number Cases
    [Theory]
    [InlineData("Infinity", NumberStyles.Any, double.PositiveInfinity)]
    [InlineData("-Infinity", NumberStyles.Any, double.NegativeInfinity)]
    [InlineData("NaN", NumberStyles.Any, double.NaN)]
    public void ParseDouble_SpecialValues_HandledCorrectly(
        string input,
        NumberStyles style,
        double expected
    )
    {
        var result = input.ParseDouble(style, CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }
    #endregion

    #region Culture-Specific Tests
    [Theory]
    [InlineData("1.234,56", "de-DE", 1234.56)] // German number format
    [InlineData("1234,56", "fr-FR", 1234.56)] // French number format
    public void ParseDouble_CultureSpecific_ReturnsCorrect(
        string input,
        string culture,
        double expected
    )
    {
        var provider = new CultureInfo(culture);
        var result = input.ParseDouble(NumberStyles.Number, provider);
        Assert.Equal(expected, result);
    }
    #endregion
}
