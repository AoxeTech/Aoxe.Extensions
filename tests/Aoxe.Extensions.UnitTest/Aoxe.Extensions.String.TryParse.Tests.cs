namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringTryParseTests
{
    #region Numeric Type Tests

    [Fact]
    public void TryParseInt_ValidInput_ReturnsParsedValue()
    {
        // Arrange
        const string input = "12345";
        const int expected = 12345;

        // Act
        var result = input.TryParseInt();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseInt_InvalidInput_ReturnsDefault()
    {
        // Arrange
        const string input = "notanumber";

        // Act
        var result = input.TryParseInt();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void TryParseInt_CustomDefault_ReturnsDefaultOnFailure()
    {
        // Arrange
        const string input = "invalid";
        const int expectedDefault = -1;

        // Act
        var result = input.TryParseInt(expectedDefault);

        // Assert
        Assert.Equal(expectedDefault, result);
    }

    [Fact]
    public void TryParseInt_WithCultureSpecificFormat_ParsesCorrectly()
    {
        // Arrange
        const string input = "1.234"; // German thousand separator
        var culture = CultureInfo.GetCultureInfo("de-DE");
        const int expected = 1234;

        // Act
        var result = input.TryParseInt(styles: NumberStyles.AllowThousands, provider: culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseByte_WithHexStyle_ParsesHexadecimal()
    {
        // Arrange
        const string input = "FF";
        const byte expected = 255;

        // Act
        var result = input.TryParseByte(styles: NumberStyles.HexNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDecimal_WithCurrencyStyle_ParsesCorrectly()
    {
        // Arrange
        const string input = "$1,234.56";
        var culture = CultureInfo.GetCultureInfo("en-US");
        const decimal expected = 1234.56m;

        // Act
        var result = input.TryParseDecimal(styles: NumberStyles.Currency, provider: culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDouble_ScientificNotation_ParsesCorrectly()
    {
        // Arrange
        const string input = "1.23e5";
        const double expected = 123000;

        // Act
        var result = input.TryParseDouble();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Boolean Tests

    [Fact]
    public void TryParseBool_ValidTrue_ReturnsTrue()
    {
        // Arrange
        const string input = "True";

        // Act
        var result = input.TryParseBool();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TryParseBool_ValidFalse_ReturnsFalse()
    {
        // Arrange
        const string input = "False";

        // Act
        var result = input.TryParseBool();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryParseBool_InvalidInput_ReturnsCustomDefault()
    {
        // Arrange
        const string input = "Yes";
        const bool expectedDefault = true;

        // Act
        var result = input.TryParseBool(expectedDefault);

        // Assert
        Assert.Equal(expectedDefault, result);
    }

    #endregion

    #region DateTime Tests

    [Fact]
    public void TryParseDateTime_ValidInput_ReturnsParsedValue()
    {
        // Arrange
        const string input = "2023-07-15T14:30:00";
        var expected = DateTime.Parse("2023-07-15T14:30:00");

        // Act
        var result = input.TryParseDateTime();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDateTime_WithCultureSpecificFormat_ParsesCorrectly()
    {
        // Arrange
        const string input = "15/07/2023 14:30";
        var culture = CultureInfo.GetCultureInfo("en-GB");
        var expected = new DateTime(2023, 7, 15, 14, 30, 0);

        // Act
        var result = input.TryParseDateTime(provider: culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDateTime_WithAssumeUniversal_SetsUtcKind()
    {
        // Arrange
        const string input = "2023-07-15 12:00:00";
        var expected = new DateTime(2023, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var result = input.TryParseDateTime(
            styles: DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
        );

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    #endregion

    #region Enum Tests

    private enum TestEnum
    {
        First,
        Second
    }

    [Fact]
    public void TryParseEnum_ValidValue_ReturnsParsedEnum()
    {
        // Arrange
        const string input = "Second";
        const TestEnum expected = TestEnum.Second;

        // Act
        var result = input.TryParseEnum<TestEnum>();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseEnum_CaseInsensitive_ReturnsParsedValue()
    {
        // Arrange
        const string input = "first";

        // Act
        var result = input.TryParseEnum<TestEnum>(ignoreCase: true);

        // Assert
        Assert.Equal(TestEnum.First, result);
    }

    [Fact]
    public void TryParseEnum_InvalidValue_ReturnsDefault()
    {
        // Arrange
        const string input = "Third";
        const TestEnum expectedDefault = TestEnum.Second;

        // Act
        var result = input.TryParseEnum<TestEnum>(expectedDefault);

        // Assert
        Assert.Equal(expectedDefault, result);
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void TryParseSbyte_MinValue_ReturnsCorrectValue()
    {
        // Arrange
        var input = sbyte.MinValue.ToString();
        var expected = sbyte.MinValue;

        // Act
        var result = input.TryParseSbyte();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDateTimeOffset_ValidInput_ReturnsParsedValue()
    {
        // Arrange
        const string input = "2023-07-15T12:00:00+02:00";
        var expected = DateTimeOffset.Parse("2023-07-15T12:00:00+02:00");

        // Act
        var result = input.TryParseDateTimeOffset();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AllMethods_NullInput_ReturnsDefault()
    {
        // Arrange
        const string? input = null;

        // Act & Assert
        Assert.Equal(-5, input.TryParseSbyte(-5));
        Assert.Equal(100, input.TryParseByte(100));
        Assert.Equal(default(DateTime), input.TryParseDateTime());
        Assert.False(input.TryParseBool(true));
    }

    [Fact]
    public void TryParseUlong_MaxValue_ReturnsCorrectValue()
    {
        // Arrange
        var input = ulong.MaxValue.ToString();
        var expected = ulong.MaxValue;

        // Act
        var result = input.TryParseUlong();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Culture-Specific Tests

    [Fact]
    public void TryParseDecimal_GermanCulture_ParsesCorrectly()
    {
        // Arrange
        const string input = "1.234,56"; // German decimal format
        var culture = CultureInfo.GetCultureInfo("de-DE");
        const decimal expected = 1234.56m;

        // Act
        var result = input.TryParseDecimal(provider: culture);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDouble_FrenchCulture_ParsesCorrectly()
    {
        // Arrange
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var input =
            $"1{culture.NumberFormat.NumberGroupSeparator}234{culture.NumberFormat.NumberDecimalSeparator}56";
        const double expected = 1234.56;

        // Act
        var result = input.TryParseDouble(
            provider: culture,
            styles: NumberStyles.Number // Use Number instead of Float|AllowThousands
        );

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion
}
