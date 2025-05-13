namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamTests
{
    #region IsNullOrEmpty Tests

    [Theory]
    [InlineData(null, true)] // Null stream
    [InlineData("", true)] // Empty MemoryStream
    [InlineData("test", false)] // Non-empty MemoryStream
    public void IsNullOrEmpty_ValidatesCorrectly(string content, bool expected)
    {
        // Arrange
        Stream stream = content != null ? new MemoryStream(ToBytes(content)) : null;

        // Act
        var result = stream.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsNullOrEmpty_WithNonSeekableStream_ReturnsFalse()
    {
        // Arrange - Create non-seekable stream wrapper
        var baseStream = new MemoryStream(ToBytes("test"));
        var nonSeekableStream = new NonSeekableStreamWrapper(baseStream);

        // Act & Assert
        Assert.False(nonSeekableStream.IsNullOrEmpty());
    }

    #endregion

    #region TrySeek Tests

    [Fact]
    public void TrySeek_WithSeekableStream_ReturnsNewPosition()
    {
        // Arrange
        using var stream = new MemoryStream(ToBytes("12345"));
        const int offset = 2;
        const int expectedPosition = 2;

        // Act
        var result = stream.TrySeek(offset, SeekOrigin.Begin);

        // Assert
        Assert.Equal(expectedPosition, result);
        Assert.Equal(expectedPosition, stream.Position);
    }

    [Fact]
    public void TrySeek_WithNonSeekableStream_ReturnsMinusOne()
    {
        // Arrange
        var baseStream = new MemoryStream(ToBytes("12345"));
        using var nonSeekableStream = new NonSeekableStreamWrapper(baseStream);

        // Act
        var result = nonSeekableStream.TrySeek(2, SeekOrigin.Begin);

        // Assert
        Assert.Equal(-1, result);
    }

    #endregion

    #region ToMemoryStream Tests

    [Theory]
    [InlineData("test")] // Regular content
    [InlineData("")] // Empty stream
    [InlineData(null)] // Null stream (should throw)
    public void ToMemoryStream_HandlesDifferentCases(string content)
    {
        // Arrange
        Stream stream = content != null ? new MemoryStream(ToBytes(content)) : null;

        if (content is null)
        {
            // Act & Assert for null case
            Assert.Throws<ArgumentNullException>(() => stream.ToMemoryStream());
            return;
        }

        // Act
        var memoryStream = stream.ToMemoryStream();

        // Assert
        Assert.Equal(content, ToUtf8String(memoryStream.ToArray()));
        Assert.True(memoryStream.CanRead);
    }

    [Fact]
    public void ToMemoryStream_PreservesOriginalPosition()
    {
        // Arrange
        using var stream = new MemoryStream(ToBytes("12345"));
        stream.Position = 3;
        var originalPosition = stream.Position;

        // Act
        _ = stream.ToMemoryStream();

        // Assert
        Assert.Equal(originalPosition, stream.Position);
    }

    [Fact]
    public void ToMemoryStream_WithNonReadableStream_ThrowsException()
    {
        // Arrange
        using var stream = new NonReadableStreamWrapper();

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => stream.ToMemoryStream());
    }

    #endregion

    #region ReadOnlyMemory/Sequence Conversion Tests

    [Theory]
    [InlineData("test")]
    [InlineData("")]
    [InlineData("large content")] // Add more test data as needed
    public async Task ConversionMethods_ProduceCorrectResults(string content)
    {
        // Arrange
        using var stream = new MemoryStream(ToBytes(content));

        // Act - Synchronous
        var memory = stream.ToReadOnlyMemory();
        var sequence = stream.ToReadOnlySequence();

        // Act - Asynchronous
        var asyncMemory = await stream.ToReadOnlyMemoryAsync();
        var asyncSequence = await stream.ToReadOnlySequenceAsync();

        // Assert
        Assert.Equal(content, ToUtf8String(memory.ToArray()));
        Assert.Equal(content, ToUtf8String(sequence.ToArray()));
        Assert.Equal(content, ToUtf8String(asyncMemory.ToArray()));
        Assert.Equal(content, ToUtf8String(asyncSequence.ToArray()));
    }

    #endregion

    #region Helper Methods/Classes

    private static byte[] ToBytes(string s) => s != null ? Encoding.UTF8.GetBytes(s) : [];

    private static string ToUtf8String(byte[] bytes) => Encoding.UTF8.GetString(bytes);

    // Helper stream implementations for testing
    private class NonSeekableStreamWrapper : Stream
    {
        private readonly Stream _baseStream;

        public NonSeekableStreamWrapper(Stream baseStream) => _baseStream = baseStream;

        public override bool CanSeek => false;
        public override bool CanRead => _baseStream.CanRead;
        public override bool CanWrite => _baseStream.CanWrite;
        public override long Length => _baseStream.Length;
        public override long Position
        {
            get => _baseStream.Position;
            set => throw new NotSupportedException();
        }

        public override void Flush() => _baseStream.Flush();

        public override int Read(byte[] buffer, int offset, int count) =>
            _baseStream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin) =>
            _baseStream.Seek(offset, origin);

        public override void SetLength(long value) => _baseStream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) =>
            _baseStream.Write(buffer, offset, count);

        protected override void Dispose(bool disposing) => _baseStream.Dispose();
    }

    private class NonReadableStreamWrapper : Stream
    {
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => throw new NotSupportedException();
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override void Flush() { }

        public override int Read(byte[] buffer, int offset, int count) =>
            throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin) =>
            throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) { }
    }

    #endregion
}
