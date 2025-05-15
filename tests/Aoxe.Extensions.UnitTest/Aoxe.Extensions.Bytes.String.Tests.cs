using System.Reflection;

namespace Aoxe.Extensions.UnitTest;

public class EncodingTests
{
    private readonly byte[] _testBytes = Encoding.UTF8.GetBytes("Hello World!");
    private readonly byte[] _multiByteBytes = Encoding.UTF8.GetBytes("café 🚀");
    private readonly byte[] _emptyBytes = [];
    private readonly byte[] _invalidAsciiBytes = [0x80, 0xFF];

    #region Argument Validation Tests

    [Theory]
    [InlineData(typeof(ArgumentNullException), nameof(AoxeExtension.GetStringByUtf8), null)]
    [InlineData(typeof(ArgumentNullException), nameof(AoxeExtension.GetStringByAscii), null)]
    [InlineData(typeof(ArgumentNullException), nameof(AoxeExtension.GetStringByUnicode), null)]
    [InlineData(typeof(ArgumentNullException), nameof(AoxeExtension.GetStringByUtf32), null)]
    [InlineData(
        typeof(ArgumentNullException),
        nameof(AoxeExtension.GetStringByBigEndianUnicode),
        null
    )]
    [InlineData(typeof(ArgumentNullException), nameof(AoxeExtension.GetStringByDefault), null)]
    public void NullInput_ThrowsArgumentNullException(
        Type exceptionType,
        string methodName,
        byte[]? input
    )
    {
        // Arrange
        var method = typeof(AoxeExtension).GetMethod(methodName, [typeof(byte[])]);

        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() => method!.Invoke(null, [input]));
        Assert.IsType(exceptionType, ex.InnerException);
        Assert.Contains("bytes", ex.InnerException!.Message);
    }

    [Fact]
    public void NullInput_ThrowsArgumentNullException2()
    {
        byte[]? bytes = null;
        Assert.Throws<ArgumentNullException>(() => bytes.GetString());
    }

    #endregion

    #region Encoding-Specific Tests

    [Fact]
    public void GetStringByUtf8_ReturnsCorrectString()
    {
        // Act
        var result = _testBytes.GetStringByUtf8();

        // Assert
        Assert.Equal("Hello World!", result);
    }

    [Fact]
    public void GetStringByAscii_HandlesAsciiRange()
    {
        // Arrange
        var asciiBytes = Encoding.ASCII.GetBytes("ASCII test");

        // Act
        var result = asciiBytes.GetStringByAscii();

        // Assert
        Assert.Equal("ASCII test", result);
    }

    [Fact]
    public void GetStringByUnicode_HandlesUtf16()
    {
        // Arrange
        var unicodeBytes = Encoding.Unicode.GetBytes("Unicode✓");

        // Act
        var result = unicodeBytes.GetStringByUnicode();

        // Assert
        Assert.Equal("Unicode✓", result);
    }

    [Fact]
    public void GetStringByBigEndianUnicode_HandlesUtf16BE()
    {
        // Arrange
        var bigEndianBytes = Encoding.BigEndianUnicode.GetBytes("BE🔑");

        // Act
        var result = bigEndianBytes.GetStringByBigEndianUnicode();

        // Assert
        Assert.Equal("BE🔑", result);
    }

    [Fact]
    public void GetStringByUtf32_HandlesUtf32Encoding()
    {
        // Arrange
        var utf32Bytes = Encoding.UTF32.GetBytes("UTF32✨");

        // Act
        var result = utf32Bytes.GetStringByUtf32();

        // Assert
        Assert.Equal("UTF32✨", result);
    }

    #endregion

    #region General GetString Tests

    [Fact]
    public void GetString_WithCustomEncoding_ReturnsCorrectString()
    {
        // Arrange
        var latin1 = Encoding.GetEncoding("ISO-8859-1");
        var bytes = latin1.GetBytes("ñöö");

        // Act
        var result = bytes.GetString(latin1);

        // Assert
        Assert.Equal("ñöö", result);
    }

    [Fact]
    public void GetString_WithoutEncoding_DefaultsToUtf8()
    {
        // Act
        var result1 = _testBytes.GetString();
        var result2 = _testBytes.GetStringByUtf8();

        // Assert
        Assert.Equal(result1, result2);
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void EmptyArray_ReturnsEmptyString()
    {
        // Act & Assert
        Assert.Equal(string.Empty, _emptyBytes.GetStringByUtf8());
        Assert.Equal(string.Empty, _emptyBytes.GetStringByAscii());
        Assert.Equal(string.Empty, _emptyBytes.GetStringByUnicode());
    }

    [Fact]
    public void GetStringByAscii_ReplacesInvalidBytes()
    {
        // Act
        var result = _invalidAsciiBytes.GetStringByAscii();

        // Assert
        Assert.Equal("??", result); // Replacement characters
    }

    [Fact]
    public void GetStringByDefault_UsesSystemDefaultEncoding()
    {
        // Arrange
        var expected = Encoding.Default.GetString(_testBytes);

        // Act
        var result = _testBytes.GetStringByDefault();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultiByteCharacters_HandleEncodingCorrectly()
    {
        // Act
        var utf8Result = _multiByteBytes.GetStringByUtf8();
        var utf32Result = Encoding.UTF32.GetBytes(utf8Result).GetStringByUtf32();

        // Assert
        Assert.Equal("café 🚀", utf8Result);
        Assert.Equal(utf8Result, utf32Result);
    }

    #endregion
}
