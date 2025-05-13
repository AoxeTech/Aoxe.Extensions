namespace Aoxe.Extensions.UnitTest;

public class Base64ConversionTests
{
    [Fact]
    public void ToBase64String_NullInput_ThrowsArgumentNullException()
    {
        byte[]? nullArray = null;
        Assert.Throws<ArgumentNullException>(() => nullArray!.ToBase64String());
    }

    [Theory]
    [InlineData(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, "SGVsbG8=")]
    [InlineData(new byte[0], "")]
    public void ToBase64String_ConvertsCorrectly(byte[] input, string expected)
    {
        var result = input.ToBase64String();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, "SGVsbG8=")]
    [InlineData("utf-8", "SGVsbG8=")]
    [InlineData("ascii", "SGVsbG8=")]
    public void ToBase64Bytes_WithDifferentEncodings(string? encodingName, string expectedBase64)
    {
        var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
        var encoding = encodingName is null ? null : Encoding.GetEncoding(encodingName);

        var result = bytes.ToBase64Bytes(encoding);
        var expected = Encoding.ASCII.GetBytes(expectedBase64);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DecodeBase64ToBytes_RoundTripSuccess()
    {
        var original = new byte[] { 0x01, 0x02, 0x03 };
        var base64 = original.ToBase64String();
        var decoded = base64.GetBytes().DecodeBase64ToBytes();
        Assert.Equal(original, decoded);
    }

    [Fact]
    public void DecodeBase64ToString_InvalidData_ThrowsFormatException()
    {
        var invalidBase64 = "Not base64!".GetBytes();
        Assert.Throws<FormatException>(() => invalidBase64.DecodeBase64ToString());
    }
}
