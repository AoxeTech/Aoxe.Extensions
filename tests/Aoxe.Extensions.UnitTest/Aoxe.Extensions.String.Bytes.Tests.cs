namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringBytesTests
{
    #region UTF-8 Encoding Tests

    /// <summary>
    /// Verify that GetUtf8Bytes converts basic string correctly
    /// </summary>
    [Fact]
    public void GetUtf8Bytes_WithValidString_ReturnsCorrectEncoding()
    {
        // Arrange
        const string input = "Hello 世界";
        var expected = Encoding.UTF8.GetBytes(input);

        // Act
        var result = input.GetUtf8Bytes();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Verify that GetUtf8Bytes throws for null input
    /// </summary>
    [Fact]
    public void GetUtf8Bytes_WhenNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        const string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => input!.GetUtf8Bytes());
    }

    #endregion

    #region ASCII Encoding Tests

    /// <summary>
    /// Verify that GetAsciiBytes replaces non-ASCII characters
    /// </summary>
    [Fact]
    public void GetAsciiBytes_WithNonAsciiCharacters_ReplacesWithQuestionMark()
    {
        // Arrange
        const string input = "café";
        var expected = Encoding.ASCII.GetBytes("caf?");

        // Act
        var result = input.GetAsciiBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Big-Endian Unicode Tests

    /// <summary>
    /// Verify that GetBigEndianUnicodeBytes uses correct byte order
    /// </summary>
    [Fact]
    public void GetBigEndianUnicodeBytes_ReturnsCorrectByteOrder()
    {
        // Arrange
        const string input = "AB";
        var expected = new byte[] { 0x00, 0x41, 0x00, 0x42 };

        // Act
        var result = input.GetBigEndianUnicodeBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Default Encoding Tests

    /// <summary>
    /// Verify that GetDefaultBytes matches system encoding
    /// </summary>
    [Fact]
    public void GetDefaultBytes_MatchesSystemEncoding()
    {
        // Arrange
        const string input = "Test";
        var expected = Encoding.Default.GetBytes(input);

        // Act
        var result = input.GetDefaultBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region UTF-32 Encoding Tests

    /// <summary>
    /// Verify that GetUtf32Bytes uses 4-byte encoding
    /// </summary>
    [Fact]
    public void GetUtf32Bytes_WithSimpleString_ReturnsCorrectLength()
    {
        // Arrange
        const string input = "A";

        // Act
        var result = input.GetUtf32Bytes();

        // Assert
        Assert.Equal(4, result.Length);
    }

    #endregion

    #region Unicode (UTF-16LE) Tests

    /// <summary>
    /// Verify that GetUnicodeBytes uses little-endian format
    /// </summary>
    [Fact]
    public void GetUnicodeBytes_ReturnsLittleEndianFormat()
    {
        // Arrange
        const string input = "AB";
        var expected = new byte[] { 0x41, 0x00, 0x42, 0x00 };

        // Act
        var result = input.GetUnicodeBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Generic GetBytes Tests

    /// <summary>
    /// Verify that GetBytes defaults to UTF-8
    /// </summary>
    [Fact]
    public void GetBytes_WhenNoEncodingSpecified_UsesUtf8()
    {
        // Arrange
        const string input = "†";

        // Act
        var result = input.GetBytes();
        var utf8Result = Encoding.UTF8.GetBytes(input);

        // Assert
        Assert.Equal(utf8Result, result);
    }

    #endregion

    #region Common Edge Cases

    /// <summary>
    /// Verify that all methods handle empty string correctly
    /// </summary>
    [Fact]
    public void AllMethods_WithEmptyString_ReturnEmptyByteArray()
    {
        // Arrange
        const string input = "";

        // Act & Assert
        Assert.Empty(input.GetUtf8Bytes());
        Assert.Empty(input.GetAsciiBytes());
        Assert.Empty(input.GetBigEndianUnicodeBytes());
        Assert.Empty(input.GetDefaultBytes());
        Assert.Empty(input.GetUtf32Bytes());
        Assert.Empty(input.GetUnicodeBytes());
        Assert.Empty(input.GetBytes());
    }

    /// <summary>
    /// Verify that all methods throw for null input
    /// </summary>
    [Fact]
    public void AllMethods_WithNullInput_ThrowArgumentNullException()
    {
        // Arrange
        const string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => input!.GetUtf8Bytes());
        Assert.Throws<ArgumentNullException>(() => input!.GetAsciiBytes());
        Assert.Throws<ArgumentNullException>(() => input!.GetBigEndianUnicodeBytes());
        Assert.Throws<ArgumentNullException>(() => input!.GetDefaultBytes());
        Assert.Throws<ArgumentNullException>(() => input!.GetUtf32Bytes());
        Assert.Throws<ArgumentNullException>(() => input!.GetUnicodeBytes());
        Assert.Throws<ArgumentNullException>(() => input!.GetBytes());
    }

    #endregion

    #region Special Character Handling

    /// <summary>
    /// Verify encoding consistency across different methods
    /// </summary>
    [Fact]
    public void EncodingMethods_WithEmoji_PreserveCorrectByteLength()
    {
        // Arrange
        const string input = "🌍"; // U+1F30D EARTH GLOBE EUROPE-AFRICA

        // Act & Assert
        // UTF-16 (surrogate pair) - 2 chars × 2 bytes = 4 bytes
        Assert.Equal(4, input.GetUnicodeBytes().Length);

        // UTF-8 - 4 bytes for this emoji
        Assert.Equal(4, input.GetUtf8Bytes().Length);

        // UTF-32 - 4 bytes PER CHARACTER (not divided)
        Assert.Equal(4, input.GetUtf32Bytes().Length);
    }

    #endregion
}
