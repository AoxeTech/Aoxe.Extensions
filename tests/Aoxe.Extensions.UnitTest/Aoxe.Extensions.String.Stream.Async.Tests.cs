namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringStreamAsyncTests
{
    #region WriteToAsync Tests

    [Fact]
    public async Task WriteToAsync_ValidInput_WritesCorrectBytes()
    {
        // Arrange
        const string input = "Hello 世界";
        var encoding = Encoding.UTF8;
        using var stream = new MemoryStream();
        var expectedBytes = encoding.GetBytes(input);

        // Act
        await input.WriteToAsync(stream, encoding);

        // Assert
        Assert.Equal(expectedBytes, stream.ToArray());
    }

    [Fact]
    public async Task WriteToAsync_WithNullStream_ThrowsArgumentNullException()
    {
        // Arrange
        const string input = "Test";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => input.WriteToAsync(null!).AsTask());
    }

    [Fact]
    public async Task WriteToAsync_WithClosedStream_ThrowsInvalidOperation()
    {
        // Arrange
        const string input = "Test";
        using var stream = new MemoryStream();
        stream.Close();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => input.WriteToAsync(stream).AsTask()
        );
    }

    [Fact]
    public async Task WriteToAsync_WithCancellation_ProperlyCancels()
    {
        // Arrange
        const string input = "Long data ";
        using var stream = new BlockingMemoryStream();
        var cts = new CancellationTokenSource();
        cts.CancelAfter(50); // Increased timeout for reliability

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
        {
            await input.WriteToAsync(stream, cancellationToken: cts.Token);
        });
    }

    #endregion

    #region TryWriteToAsync Tests

    [Fact]
    public async Task TryWriteToAsync_SuccessfulWrite_ReturnsTrue()
    {
        // Arrange
        const string input = "Success";
        using var stream = new MemoryStream();

        // Act
        var result = await input.TryWriteToAsync(stream);

        // Assert
        Assert.True(result);
        Assert.NotEmpty(stream.ToArray());
    }

    [Fact]
    public async Task TryWriteToAsync_WithNullStream_ReturnsFalse()
    {
        // Arrange
        const string input = "Test";

        // Act
        var result = await input.TryWriteToAsync(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteToAsync_WithDisposedStream_ReturnsFalse()
    {
        // Arrange
        const string input = "Test";
        var stream = new MemoryStream();
        stream.Dispose();

        // Act
        var result = await input.TryWriteToAsync(stream);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteToAsync_HandlesIoExceptions_Gracefully()
    {
        // Arrange
        const string input = "Test";
        using var stream = new ThrowingStream();

        // Act
        var result = await input.TryWriteToAsync(stream);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteToAsync_WithEmptyString_ReturnsTrue()
    {
        // Arrange
        const string input = "";
        using var stream = new MemoryStream();

        // Act
        var result = await input.TryWriteToAsync(stream);

        // Assert
        Assert.True(result);
        Assert.Empty(stream.ToArray());
    }

    #endregion

    #region Cross-Method Tests

    [Fact]
    public async Task SyncAndAsyncMethods_ProduceIdenticalResults()
    {
        // Arrange
        const string input = "Consistency check";
        var encoding = Encoding.Unicode;
        using var syncStream = new MemoryStream();
        using var asyncStream = new MemoryStream();

        // Act
        input.WriteTo(syncStream, encoding);
        await input.WriteToAsync(asyncStream, encoding);

        // Assert
        Assert.Equal(syncStream.ToArray(), asyncStream.ToArray());
    }

    #endregion

    #region Helper Classes

    private class BlockingMemoryStream : MemoryStream
    {
#if NETCOREAPP
        public override async ValueTask WriteAsync(
            ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default
        )
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(25, cancellationToken); // Shorter delay interval
            }
            cancellationToken.ThrowIfCancellationRequested();
        }
#else
        public override async Task WriteAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        )
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(25, cancellationToken);
            }
            cancellationToken.ThrowIfCancellationRequested();
        }
#endif
    }

    private class ThrowingStream : MemoryStream
    {
#if NETCOREAPP
        public override ValueTask WriteAsync(
            ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default
        ) => throw new IOException("Simulated write failure");
#else
        public override Task WriteAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        ) => throw new IOException("Simulated write failure");
#endif
    }

    #endregion
}
