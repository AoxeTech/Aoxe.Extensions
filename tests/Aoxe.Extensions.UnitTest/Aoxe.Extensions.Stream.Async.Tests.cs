namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamAsyncTests
{
    [Fact(DisplayName = "Should copy stream content to MemoryStream")]
    public async Task ToMemoryStreamAsync_CopiesContentCorrectly()
    {
        // Arrange
        var expected = Encoding.UTF8.GetBytes("Test content");
        using var sourceStream = new MemoryStream(expected);

        // Act
        var resultStream = await sourceStream.ToMemoryStreamAsync();
        var actual = resultStream.ToArray();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Should reset source stream position after copy")]
    public async Task ToMemoryStreamAsync_ResetsSourceStreamPosition()
    {
        // Arrange
        var data = Encoding.UTF8.GetBytes("Test data");
        using var sourceStream = new MemoryStream(data);
        sourceStream.Position = data.Length;

        // Act
        _ = await sourceStream.ToMemoryStreamAsync();

        // Assert
        Assert.Equal(0, sourceStream.Position);
    }

    [Fact(DisplayName = "Should reset MemoryStream position before returning")]
    public async Task ToMemoryStreamAsync_ResetsResultStreamPosition()
    {
        // Arrange
        using var sourceStream = new MemoryStream(Encoding.UTF8.GetBytes("Content"));

        // Act
        var resultStream = await sourceStream.ToMemoryStreamAsync();

        // Assert
        Assert.Equal(0, resultStream.Position);
    }

    [Fact(DisplayName = "Should handle non-seekable streams gracefully")]
    public async Task ToMemoryStreamAsync_HandlesNonSeekableStreams()
    {
        // Arrange
        var data = Encoding.UTF8.GetBytes("Non-seekable content");
        using var sourceStream = new NonSeekableMemoryStream(data);

        // Act
        var resultStream = await sourceStream.ToMemoryStreamAsync();
        var resultData = resultStream.ToArray();

        // Assert
        Assert.Equal(data, resultData);
        Assert.Equal(0, resultStream.Position);
    }

    [Fact(DisplayName = "ReadToEnd with FileStream reads from current position")]
    public void ReadToEnd_FileStream_ReadsFromCurrentPosition()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        try
        {
            // Write test bytes directly
            File.WriteAllBytes(tempFile, [1, 2, 3, 4]);
            using var fileStream = File.OpenRead(tempFile);
            fileStream.Position = 2;

            // Act
            var result = fileStream.ReadToEnd();

            // Assert
            Assert.Equal([3, 4], result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    #region ToReadOnlyMemoryAsync Tests
    [Fact]
    public async Task ToReadOnlyMemoryAsync_NullStream_ThrowsArgumentNullException()
    {
        // Arrange
        Stream? nullStream = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await nullStream!.ToReadOnlyMemoryAsync()
        );
    }

    [Fact]
    public async Task ToReadOnlyMemoryAsync_NonReadableStream_ThrowsNotSupportedException()
    {
        // Arrange
        var nonReadableStream = new NonReadableStream(new byte[10]);

        // Act & Assert
        await Assert.ThrowsAsync<NotSupportedException>(
            async () => await nonReadableStream.ToReadOnlyMemoryAsync()
        );
    }

    [Fact]
    public async Task ToReadOnlyMemoryAsync_ValidStream_ReturnsCorrectMemory()
    {
        // Arrange
        var expectedData = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(expectedData);

        // Act
        var result = await stream.ToReadOnlyMemoryAsync();

        // Assert
        Assert.Equal(expectedData, result.ToArray());
    }
    #endregion

    #region ToReadOnlySequenceAsync Tests
    [Fact]
    public async Task ToReadOnlySequenceAsync_NullStream_ThrowsArgumentNullException()
    {
        // Arrange
        Stream? nullStream = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await nullStream!.ToReadOnlySequenceAsync()
        );
    }

    [Fact]
    public async Task ToReadOnlySequenceAsync_NonReadableStream_ThrowsNotSupportedException()
    {
        // Arrange
        var nonReadableStream = new NonReadableStream(new byte[10]);

        // Act & Assert
        await Assert.ThrowsAsync<NotSupportedException>(
            async () => await nonReadableStream.ToReadOnlySequenceAsync()
        );
    }

    [Fact]
    public async Task ToReadOnlySequenceAsync_ValidStream_ReturnsCorrectSequence()
    {
        // Arrange
        var expectedData = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(expectedData);

        // Act
        var result = await stream.ToReadOnlySequenceAsync();

        // Assert
        Assert.Equal(expectedData, result.ToArray());
    }

    [Fact]
    public async Task ToReadOnlySequenceAsync_LargeStream_HandlesChunkedReads()
    {
        // Arrange
        var largeData = new byte[10_000];
        new Random().NextBytes(largeData);
        var stream = new ThrottledStream(largeData); // Stream that returns partial reads

        // Act
        var result = await stream.ToReadOnlySequenceAsync();

        // Assert
        Assert.Equal(largeData, result.ToArray());
    }
    #endregion

    #region Additional Helpers
    private class ThrottledStream(byte[] data) : MemoryStream(data)
    {
        public override async Task<int> ReadAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        )
        {
            // Simulate partial reads by returning max 100 bytes at a time
            var actualCount = Math.Min(count, 100);
            return await base.ReadAsync(buffer, offset, actualCount, cancellationToken);
        }
    }
    #endregion


    // Helper class for testing non-seekable streams
    private class NonSeekableMemoryStream(byte[] buffer) : MemoryStream(buffer)
    {
        public override bool CanSeek => false;

        public override long Position
        {
            get => base.Position;
            set => throw new NotSupportedException();
        }
    }

    private class NonReadableStream(byte[] data) : MemoryStream(data)
    {
        public override bool CanRead => false;
    }
}
