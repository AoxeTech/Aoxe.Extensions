namespace Zaabee.Extensions.UnitTest;

public class AsyncExtensionTest
{
    [Fact]
    public void RunSyncTest()
    {
        var bytes = Enumerable.Range(0, 256).Select(p => (byte)p).ToArray();
        var ms = new MemoryStream();
        ms.WriteAsync(bytes, 0, bytes.Length).RunSync();
        ms.TrySeek(0, SeekOrigin.Begin);
        var result = new byte[256];
        Assert.Equal(256, ms.ReadAsync(result, 0, bytes.Length).RunSync());
        Assert.True(TestHelper.BytesEqual(bytes, result));
    }
}