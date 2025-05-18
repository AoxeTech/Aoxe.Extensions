namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsBytesStreamTests
{
    private MemoryStream _disposedStream;

    // Test 1: Null input throws ArgumentNullException
    [Fact]
    public void ToMemoryStream_NullInput_ThrowsArgumentNullException()
    {
        byte[] input = null;
        Assert.Throws<ArgumentNullException>(() => input.ToMemoryStream());
    }

    // Test 2: Valid input returns correct MemoryStream
    [Theory]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 1 })]
    [InlineData(new byte[] { 1, 2, 3 })]
    public void ToMemoryStream_ValidInput_ReturnsCorrectStream(byte[] input)
    {
        using var result = input.ToMemoryStream();
        Assert.Equal(input, result.ToArray());
    }

    // Test 3: Null byte array throws ArgumentNullException
    [Fact]
    public void WriteTo_NullBytes_ThrowsArgumentNullException()
    {
        byte[] bytes = null;
        using var stream = new MemoryStream();
        Assert.Throws<ArgumentNullException>(() => bytes.WriteTo(stream));
    }

    // Test 4: Null stream throws ArgumentNullException
    [Fact]
    public void WriteTo_NullStream_ThrowsArgumentNullException()
    {
        var bytes = new byte[] { 1 };
        Stream stream = null;
        Assert.Throws<ArgumentNullException>(() => bytes.WriteTo(stream));
    }

    // Test 5: Data is written correctly to stream
    [Theory]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 1 })]
    [InlineData(new byte[] { 1, 2, 3 })]
    public void WriteTo_ValidParameters_WritesCorrectData(byte[] input)
    {
        using var stream = new MemoryStream();
        input.WriteTo(stream);
        Assert.Equal(input, stream.ToArray());
    }

    // Test 6: Null byte array in TryWriteTo throws
    [Fact]
    public void TryWriteTo_NullBytes_ThrowsArgumentNullException()
    {
        byte[] bytes = null;
        using var stream = new MemoryStream();
        Assert.Throws<ArgumentNullException>(() => bytes.TryWriteTo(stream));
    }

    // Test 7: Null stream in TryWriteTo throws
    [Fact]
    public void TryWriteTo_NullStream_ThrowsArgumentNullException()
    {
        var bytes = new byte[] { 1 };
        Stream stream = null;
        Assert.Throws<ArgumentNullException>(() => bytes.TryWriteTo(stream));
    }

    // Test 8: Successful write returns true
    [Theory]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 1 })]
    [InlineData(new byte[] { 1, 2, 3 })]
    public void TryWriteTo_ValidParameters_ReturnsTrue(byte[] input)
    {
        using var stream = new MemoryStream();
        var result = input.TryWriteTo(stream);
        Assert.True(result);
        Assert.Equal(input, stream.ToArray());
    }

    // Test 9: Failed write returns false
    [Fact]
    public void TryWriteTo_InvalidStream_ReturnsFalse()
    {
        var bytes = new byte[] { 1 };
        _disposedStream = new MemoryStream();
        _disposedStream.Dispose();

        var result = bytes.TryWriteTo(_disposedStream);
        Assert.False(result);
    }
}
