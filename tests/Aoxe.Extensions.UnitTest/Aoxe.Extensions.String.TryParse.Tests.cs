namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringTryParseTests
{
    #region Common Null/Empty Cases
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void TryParse_NullOrEmptyInput_ReturnsDefault(string input)
    {
        Assert.Equal(10, input.TryParseSbyte(10));
        Assert.Equal(20u, input.TryParseUint(20));
        Assert.Equal(3.14m, input.TryParseDecimal(3.14m));
        Assert.True(input.TryParseBool(true)); // 测试bool的特殊默认值
    }
    #endregion

    #region Numeric Type Tests
    [Theory]
    [InlineData("127", 0, NumberStyles.Integer, (sbyte)127)] // sbyte边界
    [InlineData("128", 100, NumberStyles.Integer, (sbyte)100)] // 超出sbyte范围
    [InlineData("0xFF", -1, NumberStyles.HexNumber, (sbyte)-1)] // 十六进制
    public void TryParseSbyte_EdgeCases_HandlesCorrectly(
        string input,
        sbyte defaultValue,
        NumberStyles style = NumberStyles.Integer,
        sbyte expected = 0
    )
    {
        var result = input.TryParseSbyte(defaultValue, style);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("255", 0, NumberStyles.AllowThousands, 255u)]
    [InlineData("256", 100u, NumberStyles.AllowThousands, 100u)]
    [InlineData("1,000", 50u, NumberStyles.AllowThousands, 1000u)] // 千位分隔符
    public void TryParseUint_ValidInput_ReturnsParsedValue(
        string input,
        uint defaultValue,
        NumberStyles style,
        uint expected
    )
    {
        var result = input.TryParseUint(defaultValue, style, CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("79228162514264337593543950335", "en-US", 0, 79228162514264337593543950335m)] // decimal最大值
    [InlineData("7,89", "fr-FR", 0m, 7.89m)] // 法语小数点
    public void TryParseDecimal_CultureSpecific(
        string input,
        string culture,
        decimal defaultValue,
        decimal expected
    )
    {
        using (new CultureScope(culture))
        {
            var result = input.TryParseDecimal(defaultValue);
            Assert.Equal(expected, result);
        }
    }
    #endregion

    #region Floating-Point Special Values
    [Theory]
    [InlineData("NaN", 0f, float.NaN)]
    [InlineData("Infinity", -1f, float.PositiveInfinity)]
    [InlineData("-Infinity", 0f, float.NegativeInfinity)]
    public void TryParseFloat_SpecialValues(string input, float defaultValue, float expected)
    {
        var result = input.TryParseFloat(defaultValue);
        Assert.Equal(expected, result);
    }
    #endregion

    #region DateTime Tests
    [Theory]
    [InlineData("2023-02-29", "yyyy-MM-dd", default, "2023-03-01")] // 自动调整
    [InlineData("InvalidDate", "yyyy-MM-dd", default, "2023-01-01")] // 无效日期
    public void TryParseDateTime_HandlesDifferentScenarios(
        string input,
        string format,
        DateTime defaultValue,
        string expectedStr
    )
    {
        var expected = DateTime.Parse(expectedStr);
        var result = input.TryParseDateTime(defaultValue, DateTimeStyles.AllowInnerWhite);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseDateTimeOffset_WithTimeZone()
    {
        var input = "2023-01-01T12:00:00+08:00";
        var result = input.TryParseDateTimeOffset();
        Assert.Equal(DateTimeOffset.Parse(input), result);
    }
    #endregion

    #region Enum Tests
    public enum TestEnum
    {
        Default,
        Value1,
        Value2
    }

    [Theory]
    [InlineData("VALUE1", true, TestEnum.Value1)] // 忽略大小写
    [InlineData("value2", true, TestEnum.Value2)]
    [InlineData("invalid", false, TestEnum.Default)] // 无效枚举值
    public void TryParseEnum_CaseHandling(string input, bool ignoreCase, TestEnum expected)
    {
        var result = input.TryParseEnum(TestEnum.Default, ignoreCase);
        Assert.Equal(expected, result);
    }
    #endregion

    #region Boolean Tests
    [Theory]
    [InlineData("TRUE", false, true)] // 默认值false但解析成功
    [InlineData("FALSE", true, false)] // 默认值true但解析成功
    [InlineData("Invalid", true, true)] // 解析失败返回默认值
    public void TryParseBool_SpecialCases(string input, bool defaultValue, bool expected)
    {
        var result = input.TryParseBool(defaultValue);
        Assert.Equal(expected, result);
    }
    #endregion

    #region Culture Helper
    private class CultureScope : IDisposable
    {
        private readonly CultureInfo _originalCulture;

        public CultureScope(string culture)
        {
            _originalCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo(culture);
        }

        public void Dispose()
        {
            CultureInfo.CurrentCulture = _originalCulture;
        }
    }
    #endregion
}
