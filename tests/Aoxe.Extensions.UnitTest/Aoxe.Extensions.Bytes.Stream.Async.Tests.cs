namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionBytesStreamAsyncTests
{
    [Fact]
    public async Task WriteToAsync_WritesBytesToStream()
    {
        // Arrange
        byte[] buffer = { 1, 2, 3, 4, 5 };
        using var memoryStream = new MemoryStream();

        // Act
        await buffer.WriteToAsync(memoryStream);

        // Assert
        byte[] writtenBytes = memoryStream.ToArray();
        Assert.Equal(buffer, writtenBytes);
    }

    [Fact]
    public async Task WriteToAsync_WithCancellationToken_CancelsOperation()
    {
        // Arrange
        byte[] buffer = [1, 2, 3, 4, 5];
        using var memoryStream = new MemoryStream();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => buffer.WriteToAsync(memoryStream, cts.Token).AsTask()
        );
    }

    [Fact]
    public async Task WriteToAsync_WithEmptyBuffer_WritesNothing()
    {
        // Arrange
        byte[] buffer = Array.Empty<byte>();
        using var memoryStream = new MemoryStream();

        // Act
        await buffer.WriteToAsync(memoryStream);

        // Assert
        byte[] writtenBytes = memoryStream.ToArray();
        Assert.Empty(writtenBytes);
    }

    [Fact]
    public async Task TryWriteToAsync_WritesBytesToStream_ReturnsTrue()
    {
        // Arrange
        byte[] buffer = { 10, 20, 30 };
        using var memoryStream = new MemoryStream();

        // Act
        bool result = await buffer.TryWriteToAsync(memoryStream);

        // Assert
        Assert.True(result);
        byte[] writtenBytes = memoryStream.ToArray();
        Assert.Equal(buffer, writtenBytes);
    }

    [Fact]
    public async Task TryWriteToAsync_WithCancellationToken_CancelsOperation_ReturnsFalse()
    {
        // Arrange
        byte[] buffer = [1, 2, 3];
        using var memoryStream = new MemoryStream();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act
        bool result = await buffer.TryWriteToAsync(memoryStream, cts.Token);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteToAsync_WithEmptyBuffer_ReturnsTrue()
    {
        // Arrange
        byte[] buffer = [];
        using var memoryStream = new MemoryStream();

        // Act
        bool result = await buffer.TryWriteToAsync(memoryStream);

        // Assert
        Assert.True(result);
        byte[] writtenBytes = memoryStream.ToArray();
        Assert.Empty(writtenBytes);
    }

    [Fact]
    public async Task WriteToAsync_StreamIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        byte[] buffer = [1, 2, 3];
        Stream? stream = null;

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(
            () => buffer.WriteToAsync(stream).AsTask()
        );
    }

    [Fact]
    public async Task WriteToAsync_WritesLargeDataSuccessfully()
    {
        // Arrange
        var buffer = new byte[10000];
        new Random().NextBytes(buffer);
        using var memoryStream = new MemoryStream();

        // Act
        await buffer.WriteToAsync(memoryStream);

        // Assert
        byte[] writtenBytes = memoryStream.ToArray();
        Assert.Equal(buffer, writtenBytes);
    }

    [Fact]
    public async Task TryWriteToAsync_WritesLargeDataSuccessfully()
    {
        // Arrange
        var buffer = new byte[10000];
        new Random().NextBytes(buffer);
        using var memoryStream = new MemoryStream();

        // Act
        bool result = await buffer.TryWriteToAsync(memoryStream);

        // Assert
        Assert.True(result);
        byte[] writtenBytes = memoryStream.ToArray();
        Assert.Equal(buffer, writtenBytes);
    }
}
