namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionTest
{
    private static readonly byte[] TestBytes = "Hello"u8.ToArray(); // "Hello"

    [Fact]
    public void GetStringByUtf8_WithValidBytes_ReturnsCorrectString()
    {
        string result = TestBytes.GetStringByUtf8();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetStringByAscii_WithValidBytes_ReturnsCorrectString()
    {
        string result = TestBytes.GetStringByAscii();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetStringByBigEndianUnicode_WithValidBytes_ReturnsCorrectString()
    {
        byte[] unicodeBytes = Encoding.BigEndianUnicode.GetBytes("Hello");
        string result = unicodeBytes.GetStringByBigEndianUnicode();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetStringByDefault_WithValidBytes_ReturnsCorrectString()
    {
        string result = TestBytes.GetStringByDefault();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetStringByUtf32_WithValidBytes_ReturnsCorrectString()
    {
        byte[] utf32Bytes = Encoding.UTF32.GetBytes("Hello");
        string result = utf32Bytes.GetStringByUtf32();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetStringByUnicode_WithValidBytes_ReturnsCorrectString()
    {
        byte[] unicodeBytes = Encoding.Unicode.GetBytes("Hello");
        string result = unicodeBytes.GetStringByUnicode();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetString_WithNullEncoding_UsesUtf8()
    {
        string result = TestBytes.GetString();
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void ToHexString_WithValidBytes_ReturnsCorrectHexString()
    {
        byte[] bytes = [1, 15, 255];
        string result = bytes.ToHexString();
        Assert.Equal("010FFF", result);
    }

    [Fact]
    public void GetString_WithEmptyBytes_ReturnsEmptyString()
    {
        byte[] emptyBytes = [];
        string result = emptyBytes.GetString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FromHexToString_WithValidHexBytes_ReturnsCorrectString()
    {
        // Arrange
        byte[] hexBytes = Encoding.UTF8.GetBytes("4142"); // Hex for "AB"

        // Act
        string result = hexBytes.FromHexToString();

        // Assert
        Assert.Equal("AB", result);
    }

    [Fact]
    public void FromHexToString_WithLowerCaseHexBytes_ReturnsCorrectString()
    {
        // Arrange
        byte[] hexBytes = "6162"u8.ToArray(); // Hex for "ab"

        // Act
        string result = hexBytes.FromHexToString();

        // Assert
        Assert.Equal("ab", result);
    }

    [Fact]
    public void FromHexToString_WithEmptyArray_ReturnsEmptyString()
    {
        // Arrange
        byte[] hexBytes = [];

        // Act
        string result = hexBytes.FromHexToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("48656C6C6F", "Hello")] // Hello
    [InlineData("776F726C64", "world")] // world
    public void FromHexToString_WithVariousInputs_ReturnsExpectedResults(
        string hexString,
        string expected
    )
    {
        // Arrange
        byte[] hexBytes = Encoding.ASCII.GetBytes(hexString);

        // Act
        string result = hexBytes.FromHexToString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("48656C6C6F", "Hello")] // Hello
    [InlineData("776F726C64", "world")] // world
    [InlineData("48692074686572652E", "Hi there.")] // Hi there.
    public void FromHexToString_WithValidHexString_ReturnsExpectedString(
        string hexString,
        string expected
    )
    {
        // Arrange
        byte[] hexBytes = Convert.FromHexString(hexString);

        // Act
        string result = hexBytes.FromHexToString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("UTF-8", "Hello", "48656C6C6F")]
    [InlineData("ASCII", "world", "776F726C64")]
    [InlineData("Unicode", "Test", "54657374")]
    public void GetString_WithDifferentEncodings_ReturnsCorrectString(
        string encodingName,
        string input,
        string expectedHex
    )
    {
        // Arrange
        byte[] bytes = Convert.FromHexString(expectedHex);
        Encoding encoding = Encoding.GetEncoding(encodingName);

        // Act
        string result = bytes.GetString(encoding);

        // Assert
        Assert.Equal(input, result);
    }

    [Fact]
    public void ToHexString_ReturnsCorrectHexString()
    {
        // Arrange
        byte[] bytes = Encoding.UTF8.GetBytes("Hello");

        // Act
        string result = bytes.ToHexString();

        // Assert
        Assert.Equal("48656C6C6F", result);
    }
}
