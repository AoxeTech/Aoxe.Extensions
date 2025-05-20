namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsHexTests
{
    #region ToHexString Tests

    [Fact]
    public void ToHexString_WhenGivenString_ReturnsCorrectHexString()
    {
        // Arrange
        const string input = "Hello";

        // Act
        var result = input.ToHexString();

        // Assert
        Assert.Equal("48656C6C6F", result);
    }

    [Fact]
    public void ToHexString_WhenGivenBytes_ReturnsCorrectHexString()
    {
        // Arrange
        var bytes = "Hello"u8.ToArray();

        // Act
        var result = bytes.ToHexString();

        // Assert
        Assert.Equal("48656C6C6F", result);
    }

    [Fact]
    public void ToHexString_WhenGivenNullBytes_ThrowsArgumentNullException()
    {
        // Arrange
        byte[]? bytes = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => bytes!.ToHexString());
    }

    #endregion

    #region ToHexBytes Tests

    [Fact]
    public void ToHexBytes_Null_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => ((string)null!).ToHexBytes());
    }

    [Fact]
    public void ToHexBytes_WhenGivenString_ReturnsCorrectHexBytes()
    {
        // Arrange
        var input = "AB";
        var expected = "4142"u8.ToArray();

        // Act
        var result = input.ToHexBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToHexBytes_WhenGivenBytes_ReturnsCorrectHexBytes()
    {
        // Arrange
        var bytes = new byte[] { 0xAB };
        var expected = "AB"u8.ToArray();

        // Act
        var result = bytes.ToHexBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region FromHexToString Tests

    [Fact]
    public void FromHexToString_WhenValidHexString_ReturnsOriginalString()
    {
        // Arrange
        var hex = "48656C6C6F";

        // Act
        var result = hex.FromHexToString();

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void FromHexToString_WhenHexBytes_ReturnsOriginalString()
    {
        // Arrange
        var hexBytes = "48656C6C6F"u8.ToArray();

        // Act
        var result = hexBytes.FromHexToString();

        // Assert
        Assert.Equal("Hello", result);
    }

    #endregion

    #region FromHexToBytes Tests

    [Fact]
    public void FromHexToBytes_WhenValidHexString_ReturnsOriginalBytes()
    {
        // Arrange
        var hex = "4142";
        var expected = new byte[] { 0x41, 0x42 };

        // Act
        var result = hex.FromHexToBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FromHexToBytes_WhenInvalidHexString_ThrowsFormatException()
    {
        // Arrange
        var invalidHex = "GH";

        // Act & Assert
        Assert.Throws<FormatException>(() => invalidHex.FromHexToBytes());
    }

    [Fact]
    public void FromHexToBytes_WhenOddLengthString_ThrowsFormatException()
    {
        // Arrange
        var oddHex = "123";

        // Act & Assert
        Assert.Throws<FormatException>(() => oddHex.FromHexToBytes());
    }

    [Fact]
    public void FromHexToBytes_WhenHexBytes_ReturnsOriginalBytes()
    {
        // Arrange
        var hexBytes = "4142"u8.ToArray();
        var expected = new byte[] { 0x41, 0x42 };

        // Act
        var result = hexBytes.FromHexToBytes();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Common Validation Tests

    [Fact]
    public void ValidateHexFormat_WhenInvalidChars_ThrowsFormatException()
    {
        // Arrange
        var invalid = "12G4";

        // Act & Assert
        Assert.Throws<FormatException>(() => invalid.FromHexToBytes());
    }

    [Fact]
    public void ValidateHexFormat_WhenValidChars_DoesNotThrow()
    {
        // Arrange
        var valid = "1a2B3c";

        // Act & Assert
        var result = valid.FromHexToBytes();
        Assert.NotNull(result);
    }

    #endregion

    #region Round-trip Conversion Tests

    [Fact]
    public void RoundTrip_StringViaHexString_ReturnsOriginalValue()
    {
        // Arrange: Complex string with multi-language characters and symbols
        var original = "Hello 你好 🚀! @#$%^&*() 测试";

        // Act: Convert to hex and back
        var hex = original.ToHexString(Encoding.UTF8);
        var result = hex.FromHexToString(Encoding.UTF8);

        // Assert: Verify data integrity
        Assert.Equal(original, result);
    }

    [Fact]
    public void RoundTrip_BytesViaHexString_HandlesFullByteRange()
    {
        // Arrange: Create byte array with all possible values (0x00-0xFF)
        var original = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

        // Act: Full conversion cycle
        var hex = original.ToHexString();
        var result = hex.FromHexToBytes();

        // Assert: Verify complete match
        Assert.Equal(original, result);
    }

    [Fact]
    public void RoundTrip_StringViaHexBytes_WithSpecialCharacters()
    {
        // Arrange: String with control characters and whitespace
        var original = "Line1\nLine2\r\nTab\tCharacter";

        // Act: Convert through hex bytes
        var hexBytes = original.ToHexBytes();
        var result = hexBytes.FromHexToString();

        // Assert: Verify exact match
        Assert.Equal(original, result);
    }

    [Fact]
    public void ToHex_Null_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => ((byte[])null!).ToHexBytes());
    }

    [Fact]
    public void RoundTrip_BytesViaHexBytes_PreservesDataIntegrity()
    {
        // Arrange: Random byte pattern
        var original = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

        // Act: Double conversion through hex bytes
        var hexBytes = original.ToHexBytes();
        var result = hexBytes.FromHexToBytes();

        // Assert: Binary equality check
        Assert.Equal(original, result);
    }

    [Fact]
    public void RoundTrip_EmptyValues_HandlesCorrectly()
    {
        // Arrange: Empty inputs
        var emptyString = string.Empty;
        var emptyBytes = Array.Empty<byte>();

        // Act & Assert for string
        var stringResult = emptyString.ToHexString().FromHexToString();
        Assert.Equal(emptyString, stringResult);

        // Act & Assert for bytes
        var byteResult = emptyBytes.ToHexString().FromHexToBytes();
        Assert.Equal(emptyBytes, byteResult);
    }

    [Fact]
    public void RoundTrip_DifferentEncodings_ProducesValidResults()
    {
        // Arrange: Test multiple encodings
        var testCases = new[]
        {
            new { Encoding = Encoding.ASCII, Text = "Simple ASCII" },
            new { Encoding = Encoding.Unicode, Text = "Unicode 你好" },
            new { Encoding = Encoding.UTF32, Text = "UTF32 😊" },
        };

        foreach (var tc in testCases)
        {
            // Act
            var hex = tc.Text.ToHexString(tc.Encoding);
            var result = hex.FromHexToString(tc.Encoding);

            // Assert
            Assert.Equal(tc.Text, result);
        }
    }

    [Fact]
    public void RoundTrip_BoundaryByteValues_ValidatesCorrectly()
    {
        // Arrange: Edge case bytes
        var testCases = new[]
        {
            new byte[] { 0x00 }, // Minimum value
            new byte[] { 0xFF }, // Maximum value
            new byte[] { 0x00, 0xFF }, // Min/Max combination
            new byte[] { 0xA0, 0xB1, 0xC2, 0xD3, 0xE4, 0xF5 } // Various high bytes
        };

        foreach (var original in testCases)
        {
            // Act
            var hex = original.ToHexString();
            var result = hex.FromHexToBytes();

            // Assert
            Assert.Equal(original, result);
        }
    }

    #endregion
}
