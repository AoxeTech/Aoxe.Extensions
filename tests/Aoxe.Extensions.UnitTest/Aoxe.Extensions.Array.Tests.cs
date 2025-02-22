namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionArrayTests
{
    [Fact]
    public void ToSpan_ReturnsCorrectSpan()
    {
        int[] array = [1, 2, 3];
        var span = array.ToSpan();

        Assert.Equal(array.Length, span.Length);
        Assert.True(array.AsSpan().SequenceEqual(span));
    }

    [Fact]
    public void ToSpan_WithStartAndLength_ReturnsCorrectSpan()
    {
        int[] array = [1, 2, 3, 4, 5];
        var span = array.ToSpan(1, 3);

        Assert.Equal(3, span.Length);
        Assert.True(array.AsSpan(1, 3).SequenceEqual(span));
    }

    [Fact]
    public void ToMemory_ReturnsCorrectMemory()
    {
        int[] array = [1, 2, 3];
        var memory = array.ToMemory();

        Assert.Equal(array.Length, memory.Length);
        Assert.True(array.AsMemory().Span.SequenceEqual(memory.Span));
    }

    [Fact]
    public void ToMemory_WithStartAndLength_ReturnsCorrectMemory()
    {
        int[] array = [1, 2, 3, 4, 5];
        var memory = array.ToMemory(1, 3);

        Assert.Equal(3, memory.Length);
        Assert.True(array.AsMemory(1, 3).Span.SequenceEqual(memory.Span));
    }

    [Fact]
    public void ToReadOnlySpan_ReturnsCorrectReadOnlySpan()
    {
        int[] array = [1, 2, 3];
        var span = array.ToReadOnlySpan();

        Assert.Equal(array.Length, span.Length);
        Assert.True(array.AsSpan().SequenceEqual(span));
    }

    [Fact]
    public void ToReadOnlySpan_WithStartAndLength_ReturnsCorrectReadOnlySpan()
    {
        int[] array = [1, 2, 3, 4, 5];
        var span = array.ToReadOnlySpan(1, 3);

        Assert.Equal(3, span.Length);
        Assert.True(array.AsSpan(1, 3).SequenceEqual(span));
    }

    [Fact]
    public void ToReadOnlyMemory_ReturnsCorrectReadOnlyMemory()
    {
        int[] array = [1, 2, 3];
        var memory = array.ToReadOnlyMemory();

        Assert.Equal(array.Length, memory.Length);
        Assert.True(array.AsMemory().Span.SequenceEqual(memory.Span));
    }

    [Fact]
    public void ToReadOnlyMemory_WithStartAndLength_ReturnsCorrectReadOnlyMemory()
    {
        int[] array = [1, 2, 3, 4, 5];
        var memory = array.ToReadOnlyMemory(1, 3);

        Assert.Equal(3, memory.Length);
        Assert.True(array.AsMemory(1, 3).Span.SequenceEqual(memory.Span));
    }

    [Fact]
    public void ToReadOnlySequence_ReturnsCorrectReadOnlySequence()
    {
        int[] array = [1, 2, 3];
        var sequence = array.ToReadOnlySequence();

        Assert.Equal(array.Length, sequence.Length);
        Assert.True(sequence.First.Span.SequenceEqual(array));
    }

    [Fact]
    public void ToReadOnlySequence_WithStartAndLength_ReturnsCorrectReadOnlySequence()
    {
        int[] array = [1, 2, 3, 4, 5];
        var sequence = array.ToReadOnlySequence(1, 3);

        Assert.Equal(3, sequence.Length);
        Assert.True(sequence.First.Span.SequenceEqual(array.AsSpan(1, 3)));
    }
}
