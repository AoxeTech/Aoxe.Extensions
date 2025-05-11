namespace Aoxe.Extensions.UnitTest;

public class BytesExtensionTest
{
    [Fact]
    public void CloneNewTest()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var cloneBytes = bytes.CloneNew();
        Assert.True(TestHelper.BytesEqual(bytes, cloneBytes));
    }

    [Fact]
    public void GetStringByUtf8Test()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        Assert.Equal(str, bytes.GetStringByUtf8());
    }

    [Fact]
    public void GetStringByAsciiTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.ASCII);
        Assert.Equal(str, bytes.GetStringByAscii());
    }

    [Fact]
    public void GetStringByBigEndianUnicodeTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.BigEndianUnicode);
        Assert.Equal(str, bytes.GetStringByBigEndianUnicode());
    }

    [Fact]
    public void GetStringByDefaultTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.Default);
        Assert.Equal(str, bytes.GetStringByDefault());
    }

    [Fact]
    public void GetStringByUtf32Test()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF32);
        Assert.Equal(str, bytes.GetStringByUtf32());
    }

    [Fact]
    public void GetStringByUnicodeTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.Unicode);
        Assert.Equal(str, bytes.GetStringByUnicode());
    }

    [Fact]
    public void ToHexStringTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        var hexString = bytes.ToHexString();
        Assert.Equal("416C696365", hexString);
    }

    [Fact]
    public void Base64Test()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);

        var bytesToBase64String = bytes.ToBase64String();
        var stringToBase64String = str.ToBase64String();
        Assert.Equal(bytesToBase64String, stringToBase64String);

        var bytesToBase64Bytes = bytes.ToBase64Bytes();
        var stringToBase64Bytes = str.ToBase64Bytes();
        Assert.Equal(bytesToBase64Bytes.Length, stringToBase64Bytes.Length);
        Assert.Equal(bytesToBase64Bytes.GetStringByUtf8(), stringToBase64Bytes.GetStringByUtf8());

        var bytesDecodeByBytes = bytesToBase64Bytes.DecodeBase64ToBytes();
        var stringDecodeByBytes = bytesToBase64Bytes.DecodeBase64ToString();
        Assert.Equal(bytesDecodeByBytes.GetStringByUtf8(), stringDecodeByBytes);
    }

    [Fact]
    public void ToMemoryStreamTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        var ms = bytes.ToMemoryStream();
        var result = ms.ReadToEnd();
        Assert.True(TestHelper.BytesEqual(bytes, result));
    }

    [Fact]
    public void WriteToTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        var ms = new MemoryStream();
        bytes.WriteTo(ms);
        var result = ms.ReadToEnd();
        Assert.True(TestHelper.BytesEqual(bytes, result));
    }

    [Fact]
    public async Task WriteToAsyncTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        var ms = new MemoryStream();
        await bytes.WriteToAsync(ms, CancellationToken.None);
        var result = await ms.ReadToEndAsync(bytes.Length, CancellationToken.None);
        Assert.True(TestHelper.BytesEqual(bytes, result));
    }

    [Fact]
    public void TryWriteToTest()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        var ms = new MemoryStream();
        Assert.True(bytes.TryWriteTo(ms));
        var result = ms.ReadToEnd();
        Assert.True(TestHelper.BytesEqual(bytes, result));
    }

    [Fact]
    public async Task TryWriteToTestAsync()
    {
        const string str = "Alice";
        var bytes = str.GetBytes(Encoding.UTF8);
        var ms = new MemoryStream();
        Assert.True(await bytes.TryWriteToAsync(ms, CancellationToken.None));
        var result = await ms.ReadToEndAsync(bytes.Length, CancellationToken.None);
        Assert.True(TestHelper.BytesEqual(bytes, result));
    }
}
