namespace Zaabee.Memory.Extensions.UnitTest;

public class ArrayExtensionTest
{
    [Fact]
    public void ToSpanTest()
    {
        var span = Consts.Chars.ToSpan();
        Assert.Equal(Consts.Chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToSpanWithLengthTest()
    {
        var span = Consts.Chars.ToSpan(0, Consts.Chars.Length);
        Assert.Equal(Consts.Chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToMemoryTest()
    {
        var memory = Consts.Chars.ToMemory();
        Assert.Equal(Consts.Chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToMemoryWithLengthTest()
    {
        var memory = Consts.Chars.ToMemory(0, Consts.Chars.Length);
        Assert.Equal(Consts.Chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySpanTest()
    {
        var span = Consts.Chars.ToReadOnlySpan();
        Assert.Equal(Consts.Chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySpanWithLengthTest()
    {
        var span = Consts.Chars.ToReadOnlySpan(0, Consts.Chars.Length);
        Assert.Equal(Consts.Chars.Length, span.Length);
        var chars = span.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlyMemoryTest()
    {
        var memory = Consts.Chars.ToReadOnlyMemory();
        Assert.Equal(Consts.Chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlyMemoryWithLengthTest()
    {
        var memory = Consts.Chars.ToReadOnlyMemory(0, Consts.Chars.Length);
        Assert.Equal(Consts.Chars.Length, memory.Length);
        var chars = memory.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySequenceTest()
    {
        var sequence = Consts.Chars.ToReadOnlySequence();
        Assert.Equal(Consts.Chars.Length, sequence.Length);
        var chars = sequence.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }

    [Fact]
    public void ToReadOnlySequenceWithLengthTest()
    {
        var sequence = Consts.Chars.ToReadOnlySequence(0, Consts.Chars.Length);
        Assert.Equal(Consts.Chars.Length, sequence.Length);
        var chars = sequence.ToArray();
        for (var i = 0; i < Consts.Chars.Length; i++)
            Assert.Equal(Consts.Chars[i], chars[i]);
    }
}