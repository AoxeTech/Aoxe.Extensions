namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamWriteAsyncTests
{
    #region TryWriteAsync(byte[]) Tests
    [Fact(DisplayName = "TryWriteAsync handles cancellation request")]
    public async Task TryWriteAsync_Cancellation_ThrowsTaskCanceled()
    {
        // Arrange
        using (var stream = new MemoryStream())
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(
                async () => await stream.TryWriteAsync(new byte[5], cts.Token)
            );
        }
    }

    [Fact(DisplayName = "TryWriteAsync writes full buffer successfully")]
    public async Task TryWriteAsync_ValidStream_WritesAllBytes()
    {
        using (var stream = new MemoryStream())
        {
            var buffer = new byte[] { 0xAA, 0xBB, 0xCC };

            // Act
            var result = await stream.TryWriteAsync(buffer);

            // Assert
            Assert.True(result);
            Assert.Equal(buffer, stream.ToArray());
        }
    }
    #endregion

    #region TryWriteAsync(byte[], offset, count) Tests
    [Fact(DisplayName = "TryWriteAsync handles non-seekable streams")]
    public async Task TryWriteAsync_NonSeekableStream_WritesCorrectly()
    {
        using (var stream = new NonSeekableMemoryStream())
        {
            var buffer = new byte[] { 1, 2, 3, 4 };
            int offset = 1,
                count = 2;

            // Act
            var result = await stream.TryWriteAsync(buffer, offset, count);

            // Assert
            Assert.True(result);
            Assert.Equal(new byte[] { 2, 3 }, stream.ToArray());
        }
    }
    #endregion

    #region WriteAsync(string) Tests
    [Fact(DisplayName = "WriteAsync handles large strings efficiently")]
    public async Task WriteAsync_LargeString_WritesCorrectBytes()
    {
        using (var stream = new MemoryStream())
        {
            var sb = new StringBuilder(10000);
            for (int i = 0; i < 1000; i++)
                sb.Append("test");
            var largeString = sb.ToString();

            // Act
            await stream.WriteAsync(largeString, Encoding.ASCII);

            // Assert
            Assert.Equal(largeString, Encoding.ASCII.GetString(stream.ToArray()));
        }
    }
    #endregion

    #region TryWriteAsync(string) Tests
    [Fact(DisplayName = "TryWriteAsync handles encoding fallback failures")]
    public async Task TryWriteAsync_InvalidCharacters_ReturnsFalse()
    {
        using (var stream = new MemoryStream())
        {
            var encoding = Encoding.GetEncoding(
                "ASCII",
                new EncoderExceptionFallback(),
                new DecoderExceptionFallback()
            );

            // Act
            var result = await stream.TryWriteAsync("café", encoding);

            // Assert
            Assert.False(result);
        }
    }
    #endregion

    #region Helper Classes
    private class NonSeekableMemoryStream : MemoryStream
    {
        public override bool CanSeek => false;
    }

    private class NonWritableMemoryStream : MemoryStream
    {
        public override bool CanWrite => false;
    }
    #endregion
}
