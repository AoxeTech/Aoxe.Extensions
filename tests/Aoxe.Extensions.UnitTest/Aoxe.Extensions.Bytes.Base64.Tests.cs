namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionBytesTests
{
    [Fact]
    public void CloneNew_WithNonEmptyArray_ReturnsEqualButDistinctArray()
    {
        // Arrange
        byte[] original = { 1, 2, 3, 4, 5 };

        // Act
        byte[] clone = original.CloneNew();

        // Assert
        Assert.Equal(original, clone);
        Assert.NotSame(original, clone);
    }

    [Fact]
    public void CloneNew_WithEmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        byte[] original = Array.Empty<byte>();

        // Act
        byte[] clone = original.CloneNew();

        // Assert
        Assert.Empty(clone);
        Assert.NotSame(original, clone);
    }

    [Fact]
    public void CloneNew_ModifyingClone_DoesNotAffectOriginal()
    {
        // Arrange
        byte[] original = { 1, 2, 3 };
        byte[] clone = original.CloneNew();

        // Act
        clone[0] = 10;

        // Assert
        Assert.Equal(1, original[0]);
        Assert.Equal(10, clone[0]);
    }

    [Fact]
    public void ToHex_WithNonEmptyArray_ReturnsCorrectHexBytes()
    {
        // Arrange
        byte[] original = { 0xA3, 0x4F };
        byte[] expected = { 0x0A, 0x03, 0x04, 0x0F };

        // Act
        byte[] hex = original.ToHex();

        // Assert
        Assert.Equal(expected, hex);
    }

    [Fact]
    public void ToHex_WithEmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        byte[] original = Array.Empty<byte>();

        // Act
        byte[] hex = original.ToHex();

        // Assert
        Assert.Empty(hex);
    }

    [Fact]
    public void FromHex_WithValidHexBytes_ReturnsOriginalArray()
    {
        // Arrange
        byte[] hex = { 0x0A, 0x03, 0x04, 0x0F };
        byte[] expected = { 0xA3, 0x4F };

        // Act
        byte[] original = hex.FromHex();

        // Assert
        Assert.Equal(expected, original);
    }

    [Fact]
    public void FromHex_WithEmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        byte[] hex = Array.Empty<byte>();

        // Act
        byte[] original = hex.FromHex();

        // Assert
        Assert.Empty(original);
    }

    [Fact]
    public void FromHex_WithOddLengthArray_ProcessesCompletePairs()
    {
        // Arrange
        byte[] hex = { 0x0A, 0x03, 0x04 }; // Odd length
        byte[] expected = { 0xA3 }; // Last byte ignored

        // Act
        byte[] original = hex.FromHex();

        // Assert
        Assert.Equal(expected, original);
    }

    [Fact]
    public void ToHex_FromHex_RoundTrip_ReturnsOriginalArray()
    {
        // Arrange
        byte[] original = { 10, 20, 30, 40, 50 };

        // Act
        byte[] hex = original.ToHex();
        byte[] result = hex.FromHex();

        // Assert
        Assert.Equal(original, result);
    }

    [Fact]
    public void ToHex_WithAllPossibleByteValues_ReturnsCorrectHexBytes()
    {
        // Arrange
        byte[] original = new byte[256];
        for (int i = 0; i < 256; i++)
            original[i] = (byte)i;

        byte[] expected = new byte[512];
        for (int i = 0; i < 256; i++)
        {
            expected[i * 2] = (byte)(i / 16);
            expected[i * 2 + 1] = (byte)(i % 16);
        }

        // Act
        byte[] hex = original.ToHex();

        // Assert
        Assert.Equal(expected, hex);
    }
}
