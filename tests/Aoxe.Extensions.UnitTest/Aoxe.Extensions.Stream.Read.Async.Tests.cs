namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamReadAsyncTests
{
    #region TryReadAsync Tests (Simple Overload)
    [Fact]
    public async Task TryReadAsync_NullStream_ReturnsMinusOne()
    {
        // Arrange
        Stream stream = null;
        byte[] buffer = new byte[10];

        // Act
        var result = await stream.TryReadAsync(buffer);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public async Task TryReadAsync_UnreadableStream_ReturnsMinusOne()
    {
        // Arrange
        var stream = new UnreadableStream();
        byte[] buffer = new byte[10];

        // Act
        var result = await stream.TryReadAsync(buffer);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public async Task TryReadAsync_ReadableStream_ReturnsBytesRead()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3, 4, 5 };
        using var stream = new MemoryStream(data);
        byte[] buffer = new byte[data.Length];

        // Act
        var result = await stream.TryReadAsync(buffer);

        // Assert
        Assert.Equal(data.Length, result);
        Assert.Equal(data, buffer);
    }

    [Fact]
    public async Task TryReadAsync_CancellationRequested_ThrowsTaskCanceledException()
    {
        // Arrange
        using var stream = new SlowStream(new byte[100]);
        var cts = new CancellationTokenSource();
        var buffer = new byte[10];
        cts.Cancel();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => stream.TryReadAsync(buffer, cts.Token).AsTask()
        );
    }
    #endregion

    #region TryReadAsync Tests (Offset Overload)
    [Fact]
    public async Task TryReadAsync_WithOffset_ReadsCorrectSegment()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3, 4, 5 };
        using var stream = new MemoryStream(data);
        byte[] buffer = new byte[5] { 0, 0, 0, 0, 0 };
        int offset = 1;
        int count = 3;

        // Act
        var result = await stream.TryReadAsync(buffer, offset, count);

        // Assert
        Assert.Equal(3, result);
        Assert.Equal(new byte[] { 0, 1, 2, 3, 0 }, buffer);
    }
    #endregion

    #region ReadToEndAsync Tests
    [Fact]
    public async Task ReadToEndAsync_NullStream_ReturnsEmptyArray()
    {
        // Arrange
        Stream stream = null;

        // Act
        var result = await stream.ReadToEndAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ReadToEndAsync_MemoryStream_ReturnsDirectBuffer()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3 };
        using var stream = new MemoryStream(data);

        // Act
        var result = await stream.ReadToEndAsync();

        // Assert
        Assert.Equal(data, result);
    }

    [Fact]
    public async Task ReadToEndAsync_NonMemoryStream_CopiesCorrectly()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3 };
        using var baseStream = new MemoryStream(data);
        using var stream = new BufferedStream(baseStream);

        // Act
        var result = await stream.ReadToEndAsync();

        // Assert
        Assert.Equal(data, result);
    }

    [Fact]
    public async Task ReadToEndAsync_WithBufferSize_CopiesCorrectly()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3, 4, 5 };
        using var stream = new TestStream(data);

        // Act
        var result = await stream.ReadToEndAsync(bufferSize: 1024);

        // Assert
        Assert.Equal(data, result);
    }
    #endregion

    #region ReadStringAsync Tests
    [Fact]
    public async Task ReadStringAsync_NullStream_ReturnsEmptyString()
    {
        // Arrange
        Stream stream = null;

        // Act
        var result = await stream.ReadStringAsync();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("ASCII", "Hello World")]
    [InlineData("UTF-8", "こんにちは")]
    [InlineData("Unicode", "���")]
    public async Task ReadStringAsync_WithEncoding_ReturnsCorrectString(
        string encodingName,
        string text
    )
    {
        // Arrange
        var encoding = encodingName switch
        {
            "ASCII" => Encoding.ASCII,
            "UTF-8" => Encoding.UTF8,
            "Unicode" => Encoding.Unicode,
            _ => throw new ArgumentException("Invalid encoding")
        };
        var data = encoding.GetBytes(text);
        using var stream = new MemoryStream(data);

        // Act
        var result = await stream.ReadStringAsync(encoding);

        // Assert
        Assert.Equal(text, result);
    }

    [Fact]
    public async Task ReadStringAsync_DefaultEncoding_UsesUtf8()
    {
        // Arrange
        var text = "Test UTF-8 Default";
        var data = Encoding.UTF8.GetBytes(text);
        using var stream = new MemoryStream(data);

        // Act
        var result = await stream.ReadStringAsync();

        // Assert
        Assert.Equal(text, result);
    }
    #endregion

    #region Helper Classes
    private class UnreadableStream : Stream
    {
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
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

    private class SlowStream : MemoryStream
    {
        public SlowStream(byte[] buffer)
            : base(buffer) { }

        public override async Task<int> ReadAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        )
        {
            await Task.Delay(100, cancellationToken);
            return await base.ReadAsync(buffer, offset, count, cancellationToken);
        }
    }

    private class TestStream : Stream
    {
        private readonly MemoryStream _inner;

        public TestStream(byte[] data) => _inner = new MemoryStream(data);

        public override bool CanRead => _inner.CanRead;
        public override bool CanSeek => _inner.CanSeek;
        public override bool CanWrite => _inner.CanWrite;
        public override long Length => _inner.Length;
        public override long Position
        {
            get => _inner.Position;
            set => _inner.Position = value;
        }

        public override void Flush() => _inner.Flush();

        public override int Read(byte[] buffer, int offset, int count) =>
            _inner.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin) => _inner.Seek(offset, origin);

        public override void SetLength(long value) => _inner.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) =>
            _inner.Write(buffer, offset, count);
    }
    #endregion
}
