namespace Zaabee.Memory.Extensions.UnitTest;

public class ReadOnlyMemoryExtensionTest
{
    private readonly char[] _chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    
    [Fact]
    public void ReadOnlyMemoryTest()
    {
        var sequence = _chars.ToReadOnlyMemory().ToReadOnlySequence();
        Assert.Equal(_chars.Length, sequence.Length);
        var chars = sequence.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }
}