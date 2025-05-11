namespace Aoxe.Extensions.UnitTest;

public class StreamExtensionTest
{
    [Fact]
    public void ToMemoryStreamTest()
    {
        var bytes = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        var ms = new MemoryStream(bytes);
        var result = ms.ToMemoryStream();
        Assert.True(BytesEqual(bytes, result.ToArray()));
    }

    [Fact]
    public async Task ToMemoryStreamAsyncTest()
    {
        var bytes = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        var ms = new MemoryStream(bytes);
        var result = await ms.ToMemoryStreamAsync();
        Assert.True(BytesEqual(bytes, result.ToArray()));
    }

    [Fact]
    public void IsNullOrEmptyTest()
    {
        MemoryStream? ms = null;
        Assert.True(ms.IsNullOrEmpty());
        ms = new MemoryStream();
        Assert.True(ms.IsNullOrEmpty());
        ms.Write([0, 1], 0, 2);
        Assert.False(ms.IsNullOrEmpty());
    }

    [Fact]
    public void TrySeekTest()
    {
        var ms = new MemoryStream();
        var bytes = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        Assert.True(ms.TryWrite(bytes, 0, 1024));
        Assert.Equal(1024, ms.Position);
        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        Assert.Equal(0, ms.Position);
    }

    [Fact]
    public void TryReadWriteTest()
    {
        var ms = new MemoryStream();
        var bytes = new byte[1024];
        var result = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        Assert.True(ms.TryWrite(bytes));
        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        Assert.Equal(1024, ms.TryRead(result));
        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        Assert.Equal(1024, ms.TryRead(result, 0, result.Length));
        Assert.True(BytesEqual(bytes, result));
    }

    [Fact]
    public async Task TryReadWriteAsyncTest()
    {
        var ms = new MemoryStream();
        var bytes = new byte[1024];
        var result = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        Assert.True(await ms.TryWriteAsync(bytes));
        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        Assert.Equal(1024, await ms.TryReadAsync(result));
        Assert.True(BytesEqual(bytes, result));
    }

    [Fact]
    public async Task TryReadWriteWithCancellationTokenAsyncTest()
    {
        var ms = new MemoryStream();
        var bytes = new byte[1024];
        var result = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        Assert.True(await ms.TryWriteAsync(bytes, 0, 1024, CancellationToken.None));
        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        Assert.Equal(1024, await ms.TryReadAsync(result, 0, 1024, CancellationToken.None));
        Assert.True(BytesEqual(bytes, result));
    }

    [Fact]
    public void TryWriteReadByteTest()
    {
        var ms = new MemoryStream();
        var bytes = new byte[1024];
        var result = new byte[1024];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in bytes)
            ms.TryWriteByte(t);

        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        for (var i = 0; i < result.Length; i++)
            result[i] = (byte)ms.TryReadByte();
        Assert.True(BytesEqual(bytes, result));
    }

    [Fact]
    public void TrySetReadTimeoutTest()
    {
        var stream = new FakeNetworkStream(new MemoryStream());
        Assert.True(stream.TrySetReadTimeout(1000));
        Assert.Equal(1000, stream.ReadTimeout);
        var timeSpan = TimeSpan.FromMinutes(1);
        Assert.True(stream.TrySetReadTimeout(timeSpan));
        Assert.Equal(timeSpan.TotalMilliseconds, stream.ReadTimeout);
    }

    [Fact]
    public void TrySetWriteTimeoutTest()
    {
        var stream = new FakeNetworkStream(new MemoryStream());
        Assert.True(stream.TrySetWriteTimeout(1000));
        Assert.Equal(1000, stream.WriteTimeout);
        var timeSpan = TimeSpan.FromMinutes(1);
        Assert.True(stream.TrySetWriteTimeout(timeSpan));
        Assert.Equal(timeSpan.TotalMilliseconds, stream.WriteTimeout);
    }

    [Fact]
    public void ReadToEndTest()
    {
        MemoryStream? ms = null;
        Assert.Empty(ms.ReadToEnd());
        ms = new MemoryStream();
        Assert.Empty(ms.ReadToEnd());

        var msBytes = new byte[1024];
        for (var i = 0; i < msBytes.Length; i++)
            msBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var b in msBytes)
            ms.TryWriteByte(b);

        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        var msResult = ms.ReadToEnd();
        Assert.True(BytesEqual(msBytes, msResult));

        var ns = new FakeNetworkStream(new MemoryStream());
        var nsBytes = new byte[1024];
        for (var i = 0; i < nsBytes.Length; i++)
            nsBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in nsBytes)
            ns.TryWriteByte(t);

        Assert.Equal(0, ns.TrySeek(0, SeekOrigin.Begin));
        var nsResult = ns.ReadToEnd();
        Assert.True(BytesEqual(nsBytes, nsResult));
    }

    [Fact]
    public async Task ReadToEndTestAsync()
    {
        MemoryStream? ms = null;
        Assert.Empty(await ms.ReadToEndAsync());
        ms = new MemoryStream();
        Assert.Empty(await ms.ReadToEndAsync());

        var msBytes = new byte[1024];
        for (var i = 0; i < msBytes.Length; i++)
            msBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in msBytes)
            ms.TryWriteByte(t);

        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        var msResult = await ms.ReadToEndAsync();
        Assert.True(BytesEqual(msBytes, msResult));

        var ns = new FakeNetworkStream(new MemoryStream());
        var nsBytes = new byte[1024];
        for (var i = 0; i < nsBytes.Length; i++)
            nsBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in nsBytes)
            ns.TryWriteByte(t);

        Assert.Equal(0, ns.TrySeek(0, SeekOrigin.Begin));
        var nsResult = await ns.ReadToEndAsync();
        Assert.True(BytesEqual(nsBytes, nsResult));
    }

    [Fact]
    public async Task ReadToEndWithBufferSizeTestAsync()
    {
        MemoryStream? ms = null;
        Assert.Empty(await ms.ReadToEndAsync(0));
        ms = new MemoryStream();
        Assert.Empty(await ms.ReadToEndAsync(0));

        var msBytes = new byte[1024];
        for (var i = 0; i < msBytes.Length; i++)
            msBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in msBytes)
            ms.TryWriteByte(t);

        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        var msResult = await ms.ReadToEndAsync(msBytes.Length);
        Assert.True(BytesEqual(msBytes, msResult));

        var ns = new FakeNetworkStream(new MemoryStream());
        var nsBytes = new byte[1024];
        for (var i = 0; i < nsBytes.Length; i++)
            nsBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in nsBytes)
            ns.TryWriteByte(t);

        Assert.Equal(0, ns.TrySeek(0, SeekOrigin.Begin));
        var nsResult = await ns.ReadToEndAsync(nsBytes.Length);
        Assert.True(BytesEqual(nsBytes, nsResult));
    }

    [Fact]
    public async Task ReadToEndWithCancellationTokenTestAsync()
    {
        MemoryStream? ms = null;
        Assert.Empty(await ms.ReadToEndAsync(0, CancellationToken.None));
        ms = new MemoryStream();
        Assert.Empty(await ms.ReadToEndAsync(0, CancellationToken.None));

        var msBytes = new byte[1024];
        for (var i = 0; i < msBytes.Length; i++)
            msBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in msBytes)
            ms.TryWriteByte(t);

        Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
        var msResult = await ms.ReadToEndAsync(msBytes.Length, CancellationToken.None);
        Assert.True(BytesEqual(msBytes, msResult));

        var ns = new FakeNetworkStream(new MemoryStream());
        var nsBytes = new byte[1024];
        for (var i = 0; i < nsBytes.Length; i++)
            nsBytes[i] = (byte)(i % (byte.MaxValue + 1));
        foreach (var t in nsBytes)
            ns.TryWriteByte(t);

        Assert.Equal(0, ns.TrySeek(0, SeekOrigin.Begin));
        var nsResult = await ns.ReadToEndAsync(nsBytes.Length, CancellationToken.None);
        Assert.True(BytesEqual(nsBytes, nsResult));
    }

    [Fact]
    public void WriteAndReadString()
    {
        const string str = "Aoxe";
        var ms = new MemoryStream();
        ms.Write(str);
        var result = ms.ReadString();
        Assert.Equal(str, result);
    }

    [Fact]
    public void TryWriteAndReadString()
    {
        const string str = "Aoxe";
        var ms = new MemoryStream();
        Assert.True(ms.TryWrite(str));
        var result = ms.ReadString();
        Assert.Equal(str, result);
    }

    [Fact]
    public async Task WriteAndReadStringAsync()
    {
        const string? str = "Aoxe";
        var ms = new MemoryStream();
        await ms.WriteAsync(str);
        var result = await ms.ReadStringAsync();
        Assert.Equal(str, result);
    }

    [Fact]
    public async Task TryWriteAndReadStringAsync()
    {
        const string? str = "Aoxe";
        var ms = new MemoryStream();
        Assert.True(await ms.TryWriteAsync(str));
        var result = await ms.ReadStringAsync();
        Assert.Equal(str, result);
    }

    [Fact]
    public void WriteStringIntoNullStream()
    {
        const string str = "Aoxe";
        MemoryStream? ms = null;
        ms.Write(str);
    }

    [Fact]
    public async Task WriteStringIntoNullStreamAsync()
    {
        const string? str = "Aoxe";
        MemoryStream? ms = null;
        await ms.WriteAsync(str);
    }

    [Fact]
    public void ToReadOnlyMemoryTest()
    {
        var readOnlyMemory = Consts.MemoryStream.ToReadOnlyMemory();
        Assert.Equal(Consts.MemoryStream.Length, readOnlyMemory.Length);
        var rawBytes = Consts.MemoryStream.ToArray();
        var bytes = readOnlyMemory.ToArray();
        for (var i = 0; i < rawBytes.Length; i++)
            Assert.Equal(rawBytes[i], bytes[i]);
    }

    [Fact]
    public void ToReadOnlySequenceTest()
    {
        var readOnlySequence = Consts.MemoryStream.ToReadOnlySequence();
        Assert.Equal(Consts.MemoryStream.Length, readOnlySequence.Length);
        var rawBytes = Consts.MemoryStream.ToArray();
        var bytes = readOnlySequence.ToArray();
        for (var i = 0; i < rawBytes.Length; i++)
            Assert.Equal(rawBytes[i], bytes[i]);
    }

    [Fact]
    public async Task ToReadOnlyMemoryTestAsync()
    {
        var readOnlyMemory = await Consts.MemoryStream.ToReadOnlyMemoryAsync();
        Assert.Equal(Consts.MemoryStream.Length, readOnlyMemory.Length);
        var rawBytes = Consts.MemoryStream.ToArray();
        var bytes = readOnlyMemory.ToArray();
        for (var i = 0; i < rawBytes.Length; i++)
            Assert.Equal(rawBytes[i], bytes[i]);
    }

    [Fact]
    public async Task ToReadOnlySequenceTestAsync()
    {
        var readOnlySequence = await Consts.MemoryStream.ToReadOnlySequenceAsync();
        Assert.Equal(Consts.MemoryStream.Length, readOnlySequence.Length);
        var rawBytes = Consts.MemoryStream.ToArray();
        var bytes = readOnlySequence.ToArray();
        for (var i = 0; i < rawBytes.Length; i++)
            Assert.Equal(rawBytes[i], bytes[i]);
    }

    private static bool BytesEqual(byte[]? first, byte[] second)
    {
        if (first.IsNullOrEmpty() || second.IsNullOrEmpty())
            return false;
        return !first!.Where((t, i) => t != second[i]).Any();
    }
}
