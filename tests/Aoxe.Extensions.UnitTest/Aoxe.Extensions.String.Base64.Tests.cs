namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringBase64Tests
{
    #region ToBase64String Tests

    /// <summary>
    /// Verify that ToBase64String encodes basic input correctly with default encoding
    /// </summary>
    [Fact]
    public void ToBase64String_WithDefaultEncoding_ReturnsValidBase64()
    {
        // Arrange
        const string input = "Hello World";

        // Act
        var result = input.ToBase64String();

        // Assert
        Assert.Equal("SGVsbG8gV29ybGQ=", result);
    }

    /// <summary>
    /// Verify that ToBase64String uses specified encoding when provided
    /// </summary>
    [Fact]
    public void ToBase64String_WithCustomEncoding_ReturnsDifferentResult()
    {
        // Arrange
        const string input = "Test";
        var utf16Encoding = Encoding.Unicode;

        // Act
        var defaultResult = input.ToBase64String();
        var customResult = input.ToBase64String(utf16Encoding);

        // Assert
        Assert.NotEqual(defaultResult, customResult);
    }

    /// <summary>
    /// Verify that ToBase64String throws for null input
    /// </summary>
    [Fact]
    public void ToBase64String_WhenNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        const string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => input!.ToBase64String());
    }

    /// <summary>
    /// Verify that ToBase64String handles empty string correctly
    /// </summary>
    [Fact]
    public void ToBase64String_WhenEmptyString_ReturnsEmptyBase64()
    {
        // Arrange
        const string input = "";

        // Act
        var result = input.ToBase64String();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    #endregion

    #region ToBase64Bytes Tests

    /// <summary>
    /// Verify that ToBase64Bytes returns ASCII bytes of Base64 string
    /// </summary>
    [Fact]
    public void ToBase64Bytes_ReturnsAsciiEncodedBytes()
    {
        // Arrange
        const string input = "Test";
        var expectedBytes = Encoding.ASCII.GetBytes(input.ToBase64String());

        // Act
        var result = input.ToBase64Bytes();

        // Assert
        Assert.Equal(expectedBytes, result);
    }

    /// <summary>
    /// Verify that ToBase64Bytes propagates encoding parameter
    /// </summary>
    [Fact]
    public void ToBase64Bytes_WithCustomEncoding_ReflectsInOutput()
    {
        // Arrange
        const string input = "Hello";
        var encoding = Encoding.UTF32;

        // Act
        var result = input.ToBase64Bytes(encoding);
        var base64String = Encoding.ASCII.GetString(result);

        // Assert
        Assert.Equal(input.ToBase64String(encoding), base64String);
    }

    #endregion

    #region FromBase64ToBytes Tests

    /// <summary>
    /// Verify that FromBase64ToBytes decodes valid Base64 string
    /// </summary>
    [Fact]
    public void FromBase64ToBytes_WithValidInput_ReturnsOriginalBytes()
    {
        // Arrange
        var originalBytes = new byte[] { 0x01, 0x02, 0x03 };
        var base64String = Convert.ToBase64String(originalBytes);

        // Act
        var result = base64String.FromBase64ToBytes();

        // Assert
        Assert.Equal(originalBytes, result);
    }

    /// <summary>
    /// Verify that FromBase64ToBytes throws for invalid Base64 format
    /// </summary>
    [Fact]
    public void FromBase64ToBytes_WithInvalidInput_ThrowsFormatException()
    {
        // Arrange
        const string invalidBase64 = "NotValidBase64!";

        // Act & Assert
        var ex = Assert.Throws<FormatException>(() => invalidBase64.FromBase64ToBytes());
        Assert.NotNull(ex.InnerException);
    }

    /// <summary>
    /// Verify that FromBase64ToBytes handles empty string correctly
    /// </summary>
    [Fact]
    public void FromBase64ToBytes_WhenEmptyString_ReturnsEmptyByteArray()
    {
        // Arrange
        const string input = "";

        // Act
        var result = input.FromBase64ToBytes();

        // Assert
        Assert.Empty(result);
    }

    #endregion

    #region DecodeBase64 Tests

    /// <summary>
    /// Verify that DecodeBase64 round-trip works with default encoding
    /// </summary>
    [Fact]
    public void DecodeBase64_RoundTrip_ReturnsOriginalString()
    {
        // Arrange
        const string original = "Test String";

        // Act
        var encoded = original.ToBase64String();
        var decoded = encoded.DecodeBase64();

        // Assert
        Assert.Equal(original, decoded);
    }

    /// <summary>
    /// Verify that DecodeBase64 uses specified encoding correctly
    /// </summary>
    [Fact]
    public void DecodeBase64_WithCustomEncoding_PreservesCharacters()
    {
        // Arrange
        const string original = "Žluťoučký";
        var encoding = Encoding.UTF32;

        // Act
        var encoded = original.ToBase64String(encoding);
        var decoded = encoded.DecodeBase64(encoding);

        // Assert
        Assert.Equal(original, decoded);
    }

    /// <summary>
    /// Verify that DecodeBase64 propagates format exceptions
    /// </summary>
    [Fact]
    public void DecodeBase64_WithInvalidInput_ThrowsFormatException()
    {
        // Arrange
        const string invalidBase64 = "Invalid!";

        // Act & Assert
        Assert.Throws<FormatException>(() => invalidBase64.DecodeBase64());
    }

    #endregion

    #region Integration Tests

    /// <summary>
    /// Verify full encoding/decoding cycle with non-ASCII characters
    /// </summary>
    [Fact]
    public void FullCycle_WithUnicodeCharacters_PreservesData()
    {
        // Arrange
        const string original = "Hello 世界 🌍";
        var encoding = Encoding.UTF8;

        // Act
        var base64 = original.ToBase64String(encoding);
        var bytes = base64.FromBase64ToBytes();
        var decoded = encoding.GetString(bytes);

        // Assert
        Assert.Equal(original, decoded);
    }

    #endregion
}
