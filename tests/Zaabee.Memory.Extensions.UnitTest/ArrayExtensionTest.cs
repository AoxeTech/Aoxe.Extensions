namespace Zaabee.Memory.Extensions.UnitTest;

public class ArrayExtensionTest
{
    private readonly char[] _chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    [Fact]
    public void ToSpanTest()
    {
        var span = _chars.ToSpan();
        Assert.Equal(_chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToSpanWithLengthTest()
    {
        var span = _chars.ToSpan(0, _chars.Length);
        Assert.Equal(_chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToMemoryTest()
    {
        var memory = _chars.ToMemory();
        Assert.Equal(_chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToMemoryWithLengthTest()
    {
        var memory = _chars.ToMemory(0, _chars.Length);
        Assert.Equal(_chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySpanTest()
    {
        var span = _chars.ToReadOnlySpan();
        Assert.Equal(_chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySpanWithLengthTest()
    {
        var span = _chars.ToReadOnlySpan(0, _chars.Length);
        Assert.Equal(_chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlyMemoryTest()
    {
        var memory = _chars.ToReadOnlyMemory();
        Assert.Equal(_chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlyMemoryWithLengthTest()
    {
        var memory = _chars.ToReadOnlyMemory(0, _chars.Length);
        Assert.Equal(_chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySequenceTest()
    {
        var sequence = _chars.ToReadOnlySequence();
        Assert.Equal(_chars.Length, sequence.Length);
        var chars = sequence.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySequenceWithLengthTest()
    {
        var sequence = _chars.ToReadOnlySequence(0, _chars.Length);
        Assert.Equal(_chars.Length, sequence.Length);
        var chars = sequence.ToArray();
        for (var i = 0; i < _chars.Length; i++)
            Assert.Equal(_chars[i], chars[i]);
    }
}