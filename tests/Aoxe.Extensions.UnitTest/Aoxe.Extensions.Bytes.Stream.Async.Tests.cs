namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsBytesStreamAsyncTests
{
    private MemoryStream _validStream = new();
    private readonly byte[] _testData = { 1, 2, 3, 4, 5 };

    // region WriteToAsync Tests

    [Fact]
    public async Task WriteToAsync_NullBytes_ThrowsArgumentNullException()
    {
        byte[] nullBytes = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await nullBytes.WriteToAsync(_validStream)
        );
    }

    [Fact]
    public async Task WriteToAsync_NullStream_ThrowsArgumentNullException()
    {
        Stream nullStream = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _testData.WriteToAsync(nullStream)
        );
    }

    [Fact]
    public async Task WriteToAsync_ValidParameters_WritesAllBytes()
    {
        await _testData.WriteToAsync(_validStream);
        Assert.Equal(_testData, _validStream.ToArray());
    }

    [Fact]
    public async Task WriteToAsync_CancelledToken_ThrowsTaskCanceled()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await _testData.WriteToAsync(_validStream, cts.Token)
        );
    }

    // endregion

    // region TryWriteToAsync Tests

    [Fact]
    public async Task TryWriteToAsync_NullBytes_ThrowsArgumentNullException()
    {
        byte[] nullBytes = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await nullBytes.TryWriteToAsync(_validStream)
        );
    }

    [Fact]
    public async Task TryWriteToAsync_NullStream_ThrowsArgumentNullException()
    {
        Stream nullStream = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _testData.TryWriteToAsync(nullStream)
        );
    }

    [Fact]
    public async Task TryWriteToAsync_ValidParameters_ReturnsTrue()
    {
        var result = await _testData.TryWriteToAsync(_validStream);
        Assert.True(result);
        Assert.Equal(_testData, _validStream.ToArray());
    }

    [Fact]
    public async Task TryWriteToAsync_CancelledToken_ReturnsFalse()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();

        var result = await _testData.TryWriteToAsync(new CancellableStream(), cts.Token);

        Assert.False(result);
    }

    [Fact]
    public async Task TryWriteToAsync_WriteFailure_PropagatesException()
    {
        var brokenStream = new BrokenStream();
        await Assert.ThrowsAsync<IOException>(
            async () => await _testData.TryWriteToAsync(brokenStream)
        );
    }

    // endregion

    // region Helper Classes

    private class CancellableStream : MemoryStream
    {
#if NET48
        public override async Task WriteAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        )
        {
            await Task.Delay(-1, cancellationToken);
        }
#else
        public override async ValueTask WriteAsync(
            ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default
        )
        {
            await Task.Delay(-1, cancellationToken);
        }
#endif
    }

    private class BrokenStream : MemoryStream
    {
#if NET48
        public override async Task WriteAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken
        )
        {
            throw new IOException("Simulated write failure");
        }
#else
        public override async ValueTask WriteAsync(
            ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default
        )
        {
            throw new IOException("Simulated write failure");
        }
#endif
    }

    // endregion
}
