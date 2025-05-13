namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamReadAsyncTests
{
    #region TryReadAsync Tests
    [Fact(DisplayName = "TryReadAsync with null stream returns -1")]
    public async Task TryReadAsync_NullStream_ReturnsMinusOne()
    {
        // Arrange
        Stream nullStream = null!;
        byte[] buffer = new byte[10];

        // Act
        var result = await nullStream.TryReadAsync(buffer);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact(DisplayName = "TryReadAsync respects cancellation token")]
    public async Task TryReadAsync_Cancellation_ThrowsTaskCanceled()
    {
        // Arrange
        using var stream = new MemoryStream(new byte[100]);
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => stream.TryReadAsync(new byte[10], cts.Token).AsTask()
        );
    }

    [Fact(DisplayName = "TryReadAsync with offset reads correct data")]
    public async Task TryReadAsync_WithOffset_ReadsCorrectData()
    {
        // Arrange
        var testData = new byte[] { 1, 2, 3, 4, 5 };
        using var stream = new MemoryStream(testData);
        byte[] buffer = new byte[5];
        int offset = 2;
        int count = 3;

        // Act
        var bytesRead = await stream.TryReadAsync(buffer, offset, count);

        // Assert
        Assert.Equal(3, bytesRead);
        Assert.Equal([0, 0, 1, 2, 3], buffer);
    }
    #endregion

    #region ReadToEndAsync Tests
    [Fact(DisplayName = "ReadToEndAsync handles large streams")]
    public async Task ReadToEndAsync_LargeStream_ReadsAllContent()
    {
        // Arrange
        using var stream = new LargeTestStream(1024 * 1024); // 1MB
        var expected = new byte[1024 * 1024];
        for (int i = 0; i < expected.Length; i++)
        {
            expected[i] = 0x01;
        }

        // Act
        var result = await stream.ReadToEndAsync();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "ReadToEndAsync with bufferSize parameter")]
    public async Task ReadToEndAsync_CustomBufferSize_ReadsCorrectly()
    {
        // Arrange
        var testData = new byte[8192];
        new Random().NextBytes(testData); // Replaced Random.Shared
        using var stream = new MemoryStream(testData);

        // Act
        var result = await stream.ReadToEndAsync(bufferSize: 512);

        // Assert
        Assert.Equal(testData, result);
    }
    #endregion

    #region ReadStringAsync Tests
    [Fact(DisplayName = "ReadStringAsync handles empty streams")]
    public async Task ReadStringAsync_EmptyStream_ReturnsEmptyString()
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        var result = await stream.ReadStringAsync();

        // Assert
        Assert.Equal(string.Empty, result);
    }
    #endregion

    #region ToReadOnlyMemory/Sequence Tests
    [Fact(DisplayName = "ToReadOnlyMemoryAsync returns correct data")]
    public async Task ToReadOnlyMemoryAsync_ValidStream_MatchesContent()
    {
        // Arrange
        var testData = new byte[] { 1, 2, 3, 4 };
        using var stream = new MemoryStream(testData);

        // Act
        var memory = await stream.ToReadOnlyMemoryAsync();

        // Assert
        Assert.Equal(testData, memory.ToArray());
    }

    [Fact(DisplayName = "ToReadOnlySequenceAsync handles chunked data")]
    public async Task ToReadOnlySequenceAsync_ChunkedStream_CombinesCorrectly()
    {
        // Arrange
        using var stream = new ChunkedMemoryStream(
            [
                [1, 2],
                [3, 4]
            ]
        );

        // Act
        var sequence = await stream.ToReadOnlySequenceAsync();

        // Assert
        Assert.Equal([1, 2, 3, 4], sequence.ToArray());
    }
    #endregion

    #region Helper Classes
    private class LargeTestStream : MemoryStream
    {
        public LargeTestStream(int size)
        {
            var buffer = new byte[size];
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = 0x01;
            }
            Write(buffer, 0, size);
            Position = 0;
        }

        public sealed override long Position
        {
            get => base.Position;
            set => base.Position = value;
        }

        public sealed override void Write(byte[] buffer, int offset, int count) =>
            base.Write(buffer, offset, count);
    }

    private class ChunkedMemoryStream : MemoryStream
    {
        public ChunkedMemoryStream(byte[][] chunks)
            : base()
        {
            foreach (var chunk in chunks)
                Write(chunk, 0, chunk.Length);
            Position = 0;
        }

        public sealed override long Position
        {
            get => base.Position;
            set => base.Position = value;
        }

        public sealed override void Write(byte[] buffer, int offset, int count) =>
            base.Write(buffer, offset, count);
    }

    private class NonReadableStream : MemoryStream
    {
        public override bool CanRead => false;
    }
    #endregion
}
