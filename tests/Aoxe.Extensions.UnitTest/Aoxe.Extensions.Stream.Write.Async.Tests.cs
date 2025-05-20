namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionStreamWriteAsyncTests
{
    #region TryWriteAsync(byte[] buffer) Tests
    [Fact]
    public async Task TryWriteAsync_StreamIsNull_ReturnsFalse()
    {
        // Arrange
        Stream? stream = null;
        var buffer = new byte[1];

        // Act
        var result = await stream.TryWriteAsync(buffer);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteAsync_NonWritableStream_ReturnsFalse()
    {
        // Arrange
        using var stream = new NonWritableStream();

        // Act
        var result = await stream.TryWriteAsync(new byte[1]);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteAsync_ValidWrite_ReturnsTrue()
    {
        // Arrange
        using var stream = new MemoryStream();
        var buffer = new byte[] { 1, 2, 3 };

        // Act
        var result = await stream.TryWriteAsync(buffer);

        // Assert
        Assert.True(result);
        Assert.Equal(buffer, stream.ToArray());
    }

    [Theory]
    [InlineData(typeof(IOException))]
    [InlineData(typeof(ObjectDisposedException))]
    [InlineData(typeof(NotSupportedException))]
    public async Task TryWriteAsync_WriteErrors_ReturnsFalse(Type exceptionType)
    {
        // Arrange
        var exception = (Exception)Activator.CreateInstance(exceptionType, "Test exception");
        using var stream = new ThrowingStream(exception);
        var buffer = new byte[1];

        // Act
        var result = await stream.TryWriteAsync(buffer);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region TryWriteAsync(byte[] buffer, int offset, int count) Tests
    [Fact]
    public async Task TryWriteAsyncWithOffset_ValidWrite_ReturnsTrue()
    {
        // Arrange
        using var stream = new MemoryStream();
        var buffer = new byte[] { 1, 2, 3, 4, 5 };

        // Act
        var result = await stream.TryWriteAsync(buffer, 1, 3);

        // Assert
        Assert.True(result);
        Assert.Equal([2, 3, 4], stream.ToArray());
    }

    [Fact]
    public async Task TryWriteAsyncWithOffset_NonWritableStream_ReturnsFalse()
    {
        // Arrange
        using var stream = new NonWritableStream();

        // Act
        var result = await stream.TryWriteAsync(new byte[5], 0, 5);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region WriteAsync(string) Tests
    [Fact]
    public async Task WriteAsync_NullParameters_NoOperation()
    {
        // Arrange
        Stream? nullStream = null;
        string? nullString = null;
        using var memoryStream = new MemoryStream();

        // Act
        await nullStream.WriteAsync("test");
        await memoryStream.WriteAsync(nullString);

        // Assert
        Assert.Equal(0, memoryStream.Length);
    }

    [Fact]
    public async Task WriteAsync_ValidString_WritesUtf8()
    {
        // Arrange
        using var stream = new MemoryStream();
        var testString = "Hello World";

        // Act
        await stream.WriteAsync(testString);

        // Assert
        Assert.Equal(Encoding.UTF8.GetBytes(testString), stream.ToArray());
    }

    [Fact]
    public async Task WriteAsync_CustomEncoding_WritesCorrectBytes()
    {
        // Arrange
        using var stream = new MemoryStream();
        var testString = "Hello";
        var encoding = Encoding.Unicode;

        // Act
        await stream.WriteAsync(testString, encoding);

        // Assert
        Assert.Equal(encoding.GetBytes(testString), stream.ToArray());
    }
    #endregion

    #region TryWriteAsync(string) Tests
    [Fact]
    public async Task TryWriteAsyncString_InvalidParameters_ReturnsFalse()
    {
        // Arrange
        using var nonWritableStream = new NonWritableStream();
        Stream? nullStream = null;
        string? nullString = null;

        // Act
        var result1 = await nonWritableStream.TryWriteAsync("test");
        var result2 = await nullStream.TryWriteAsync("test");
        var result3 = await nonWritableStream.TryWriteAsync(nullString);

        // Assert
        Assert.False(result1);
        Assert.False(result2);
        Assert.False(result3);
    }

    [Fact]
    public async Task TryWriteAsyncString_ValidWrite_ReturnsTrue()
    {
        // Arrange
        using var stream = new MemoryStream();
        var testString = "Test Content";

        // Act
        var result = await stream.TryWriteAsync(testString);

        // Assert
        Assert.True(result);
        Assert.Equal(Encoding.UTF8.GetBytes(testString), stream.ToArray());
    }

    [Fact]
    public async Task TryWriteAsyncString_EncodingError_ReturnsFalse()
    {
        // Arrange
        using var stream = new MemoryStream();
        var encoding = Encoding.GetEncoding(
            "ASCII",
            new EncoderExceptionFallback(),
            new DecoderExceptionFallback()
        );
        var testString = "Résumé";

        // Act
        var result = await stream.TryWriteAsync(testString, encoding);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region Helper Classes
    private class NonWritableStream : Stream
    {
        public override bool CanWrite => false;
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override long Length => throw new NotSupportedException();
        public override long Position { get; set; }

        public override void Flush() => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count) =>
            throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin) =>
            throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) =>
            throw new NotSupportedException();
    }

    private class ThrowingStream(Exception exception) : MemoryStream
    {
        public override async Task WriteAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        )
        {
            await Task.Yield();
            throw exception;
        }

#if NETCOREAPP
        // Add this override for .NET Core style WriteAsync
        public override async ValueTask WriteAsync(
            ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default
        )
        {
            await Task.Yield();
            throw exception;
        }
#endif
    }
    #endregion
}
