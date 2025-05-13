namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringParseTests
{
    #region Numeric Parsing Tests

    [Fact]
    public void ParseSbyte_ValidInput_ReturnsParsedValue()
    {
        // Arrange
        const string input = "127";
        const sbyte expected = 127;

        // Act
        var result = input.ParseSbyte();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseByte_WithHexStyle_ParsesHexadecimal()
    {
        // Arrange
        const string input = "FF";
        const byte expected = 255;

        // Act
        var result = input.ParseByte(NumberStyles.HexNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseInt_WithCultureSpecificFormat_ParsesCorrectly()
    {
        // Arrange
        const string input = "1.234";
        var culture = CultureInfo.GetCultureInfo("de-DE");
        const int expected = 1234;

        // Act
        var result = input.ParseInt(NumberStyles.AllowThousands, culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseDecimal_WithCurrencySymbol_ParsesCorrectly()
    {
        // Arrange
        const string input = "$1,234.56";
        var culture = CultureInfo.GetCultureInfo("en-US");
        const decimal expected = 1234.56m;

        // Act
        var result = input.ParseDecimal(NumberStyles.Currency, culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseDouble_WithScientificNotation_ParsesCorrectly()
    {
        // Arrange
        const string input = "1.23e5";
        const double expected = 123000;

        // Act
        var result = input.ParseDouble();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Exception Handling Tests

    [Fact]
    public void ParseUlong_OutOfRangeValue_ThrowsOverflowException()
    {
        // Arrange
        const string input = "-1";

        // Act & Assert
        Assert.Throws<OverflowException>(() => input.ParseUlong());
    }

    [Fact]
    public void ParseFloat_InvalidFormat_ThrowsFormatException()
    {
        // Arrange
        const string input = "12.34.56";

        // Act & Assert
        Assert.Throws<FormatException>(() => input.ParseFloat());
    }

    [Fact]
    public void AllNumericMethods_NullInput_ThrowArgumentNullException()
    {
        // Arrange
        const string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => input!.ParseInt());
        Assert.Throws<ArgumentNullException>(() => input!.ParseDecimal());
        Assert.Throws<ArgumentNullException>(() => input!.ParseDouble());
    }

    #endregion

    #region DateTime Parsing Tests

    [Fact]
    public void ParseDateTime_WithDifferentCulture_ParsesCorrectly()
    {
        // Arrange
        const string input = "15/07/2023 14:30";
        var culture = CultureInfo.GetCultureInfo("en-GB");
        var expected = new DateTime(2023, 7, 15, 14, 30, 0);

        // Act
        var result = input.ParseDateTime(culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseDateTimeOffset_WithUtcFormat_ParsesCorrectly()
    {
        // Arrange
        const string input = "2023-07-15T12:00:00Z";
        var expected = DateTimeOffset.Parse("2023-07-15T12:00:00Z");

        // Act
        var result = input.ParseDateTimeOffset();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Boolean Parsing Tests

    [Fact]
    public void ParseBool_ValidTrueVariations_ReturnTrue()
    {
        // Act & Assert
        Assert.True("True".ParseBool());
        Assert.True("true".ParseBool());
    }

    [Fact]
    public void ParseBool_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        const string input = "Yes";

        // Act & Assert
        Assert.Throws<FormatException>(() => input.ParseBool());
    }

    #endregion

    #region Enum Parsing Tests

    private enum TestEnum
    {
        First,
        Second
    }

    [Fact]
    public void ParseEnum_ValidValue_ReturnsCorrectEnum()
    {
        // Arrange
        const string input = "Second";
        const TestEnum expected = TestEnum.Second;

        // Act
        var result = input.ParseEnum<TestEnum>();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseEnum_CaseInsensitive_ReturnsCorrectValue()
    {
        // Arrange
        const string input = "first";

        // Act
        var result = input.ParseEnum<TestEnum>(ignoreCase: true);

        // Assert
        Assert.Equal(TestEnum.First, result);
    }

    [Fact]
    public void ParseEnum_InvalidType_ThrowsArgumentException()
    {
        // Arrange
        const string input = "Value";
        Type nonEnumType = typeof(string);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => input.ParseEnum(nonEnumType));
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void ParseShort_MinValue_ParsesCorrectly()
    {
        // Arrange
        string input = short.MinValue.ToString();
        short expected = short.MinValue;

        // Act
        var result = input.ParseShort();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseDateTime_WithCustomStyles_AdjustsCorrectly()
    {
        // Arrange
        const string input = "15 July 2023";
        var culture = CultureInfo.InvariantCulture;
        var expectedUtc = new DateTime(2023, 7, 15, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = input.ParseDateTime(
            provider: culture,
            styles: DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
        );

        // Assert
        Assert.Equal(expectedUtc, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    #endregion
}
