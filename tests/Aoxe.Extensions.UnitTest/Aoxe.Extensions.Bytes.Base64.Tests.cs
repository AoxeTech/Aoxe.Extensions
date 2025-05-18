namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsBytesBase64Tests
{
    private static readonly byte[] SampleBytes = { 97, 98, 99 }; // "abc"
    private const string SampleBase64 = "YWJj";
    private const string TestString = "Hello world";

    public static IEnumerable<object[]> Encodings =>
        new[]
        {
            new object[] { null },
            new object[] { Encoding.UTF8 },
            new object[] { Encoding.ASCII },
            new object[] { Encoding.Unicode },
            new object[] { Encoding.UTF32 }
        };

    // Test null input for all methods
    [Fact]
    public void AllMethods_NullInput_ThrowsArgumentNullException()
    {
        byte[] nullBytes = null;
        Assert.Throws<ArgumentNullException>(() => nullBytes.ToBase64String());
        Assert.Throws<ArgumentNullException>(() => nullBytes.ToBase64Bytes());
        Assert.Throws<ArgumentNullException>(() => nullBytes.DecodeBase64ToBytes());
        Assert.Throws<ArgumentNullException>(() => nullBytes.DecodeBase64ToString());
    }

    // Test basic Base64 string conversion
    [Fact]
    public void ToBase64String_ValidInput_ReturnsCorrectResult()
    {
        var result = SampleBytes.ToBase64String();
        Assert.Equal(SampleBase64, result);
    }

    // Test empty byte array handling
    [Fact]
    public void ToBase64String_EmptyArray_ReturnsEmptyString()
    {
        var result = Array.Empty<byte>().ToBase64String();
        Assert.Equal(string.Empty, result);
    }

    // Parameterized test for different encodings
    [Theory]
    [MemberData(nameof(Encodings))]
    public void ToBase64Bytes_WithVariousEncodings_ProducesValidBase64(Encoding? encoding)
    {
        var result = SampleBytes.ToBase64Bytes(encoding);
        var base64String = (encoding ?? Encoding.UTF8).GetString(result);
        Assert.Equal(SampleBase64, base64String);
    }

    // Test Base64 bytes decoding
    [Theory]
    [MemberData(nameof(Encodings))]
    public void DecodeBase64ToBytes_RoundTrip_ReturnsOriginalData(Encoding encoding)
    {
        var encoded = SampleBytes.ToBase64Bytes(encoding);
        var decoded = encoded.DecodeBase64ToBytes(encoding);
        Assert.Equal(SampleBytes, decoded);
    }

    // Test invalid Base64 input
    [Fact]
    public void DecodeBase64ToBytes_InvalidInput_ThrowsFormatException()
    {
        var invalidBytes = Encoding.UTF8.GetBytes("NotValidBase64!");
        Assert.Throws<FormatException>(() => invalidBytes.DecodeBase64ToBytes());
    }

    // Test string decoding with various encodings
    [Theory]
    [MemberData(nameof(Encodings))]
    public void DecodeBase64ToString_RoundTrip_ReturnsOriginalString(Encoding encoding)
    {
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(TestString);
        var base64Bytes = bytes.ToBase64Bytes(encoding);
        var result = base64Bytes.DecodeBase64ToString(encoding);
        Assert.Equal(TestString, result);
    }

    // Test maximum edge cases
    [Fact]
    public void DecodeBase64ToString_MaxSizeData_HandlesCorrectly()
    {
        var largeArray = new byte[65536];
        var random = new Random(); // .NET Framework compatible
        random.NextBytes(largeArray);

        var encoded = largeArray.ToBase64Bytes();
        var decoded = encoded.DecodeBase64ToBytes();

        Assert.Equal(largeArray, decoded);
    }

    // Test empty byte array decoding
    [Fact]
    public void DecodeBase64ToString_EmptyInput_ReturnsEmptyString()
    {
        var empty = Array.Empty<byte>();
        var result = empty.DecodeBase64ToString();
        Assert.Equal(string.Empty, result);
    }
}
