namespace Aoxe.Extensions.UnitTest;

public class EncodingTests
{
    [Theory]
    [InlineData("utf-8", new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F })]
    [InlineData("ascii", new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F })]
    [InlineData(
        "utf-32",
        new byte[]
        {
            0x48,
            0x00,
            0x00,
            0x00,
            0x65,
            0x00,
            0x00,
            0x00,
            0x6C,
            0x00,
            0x00,
            0x00,
            0x6C,
            0x00,
            0x00,
            0x00,
            0x6F,
            0x00,
            0x00,
            0x00
        }
    )]
    public void GetString_WithDifferentEncodings(string encodingName, byte[] bytes)
    {
        var encoding = Encoding.GetEncoding(encodingName);
        var result = bytes.GetString(encoding);
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetString_DefaultsToUtf8()
    {
        var bytes = new byte[] { 0xC3, 0xA9 }; // UTF-8 é
        var result = bytes.GetString();
        Assert.Equal("é", result);
    }

    [Fact]
    public void ToHexString_ProducesCorrectFormat()
    {
        var bytes = new byte[] { 0xAB, 0xCD, 0xEF };
#if NETSTANDARD2_0
        var expected = "ABCDEF";
#else
        var expected = "ABCDEF";
#endif
        Assert.Equal(expected, bytes.ToHexString());
    }

    [Fact]
    public void FromHexToString_ConvertsCorrectly()
    {
        // Create proper hex nibbles (A=10, B=11, C=12)
        var hexBytes1 = new byte[] { 0x0A, 0x0B, 0x0C };
        var result1 = hexBytes1.FromHexToString();
        Assert.Equal("\n\v\f", result1); // ASCII values for 10, 11, 12

        // Or for actual "ABC" string:
        // 1. First create proper hex representation
        var original = "414243"; // Hex string for "ABC"
        var hexBytes = original.ToHexBytes();
        var result = hexBytes.FromHexToString();
        Assert.Equal("ABC", result);
    }
}
