namespace Zaabee.Memory.Extensions.UnitTest;

public class StreamExtensionTest
{
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
}