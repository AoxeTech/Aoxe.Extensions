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
}
