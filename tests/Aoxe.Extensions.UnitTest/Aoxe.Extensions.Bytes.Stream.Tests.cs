namespace Aoxe.Extensions.UnitTest;

public class StreamOperationTests
{
    [Fact]
    public void ToMemoryStream_ContainsOriginalData()
    {
        var bytes = new byte[] { 1, 2, 3 };
        using var ms = bytes.ToMemoryStream();
        Assert.Equal(bytes, ms.ToArray());
    }

    [Fact]
    public void WriteTo_WritesEntireBuffer()
    {
        var bytes = new byte[] { 1, 2, 3 };
        using var ms = new MemoryStream();
        bytes.WriteTo(ms);
        Assert.Equal(bytes, ms.ToArray());
    }

    [Fact]
    public void TryWriteTo_ReturnsFalseOnFailure()
    {
        var bytes = new byte[] { 1, 2, 3 };
        using var ms = new MemoryStream(new byte[0], writable: false);
        var result = bytes.TryWriteTo(ms);
        Assert.False(result);
    }

    [Fact]
    public async Task WriteToAsync_WritesEntireBuffer()
    {
        var bytes = new byte[] { 1, 2, 3 };
        using var ms = new MemoryStream();
        await bytes.WriteToAsync(ms);
        Assert.Equal(bytes, ms.ToArray());
    }

    [Fact]
    public async Task TryWriteToAsync_ReturnsFalseOnCancel()
    {
        var bytes = new byte[1000];
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        using var ms = new MemoryStream();
        var result = await bytes.TryWriteToAsync(ms, cts.Token);

        Assert.False(result);
    }
}
