namespace Zaabee.Memory.Extensions.UnitTest;

public class ReadOnlyMemoryExtensionTest
{
    [Fact]
    public void ReadOnlyMemoryTest()
    {
        var sequence = Consts.Chars.ToReadOnlyMemory().ToReadOnlySequence();
        Assert.Equal(Consts.Chars.Length, sequence.Length);
        var chars = sequence.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }
}