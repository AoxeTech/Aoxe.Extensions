namespace Aoxe.Extensions.UnitTest;

public class ArrayExtensionTest
{
    private readonly int[] _testArray = [1, 2, 3, 4, 5];

    [Fact]
    public void ToSpan_ConvertsEntireArray()
    {
        var span = _testArray.ToSpan();
        Assert.Equal(_testArray.Length, span.Length);
        Assert.Equal(_testArray, span.ToArray());
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(0, 5)]
    [InlineData(4, 1)]
    public void ToSpan_ConvertsSubarray(int start, int length)
    {
        var span = _testArray.ToSpan(start, length);
        Assert.Equal(length, span.Length);
        Assert.Equal(_testArray.AsSpan(start, length).ToArray(), span.ToArray());
    }

    [Fact]
    public void ToMemory_ConvertsEntireArray()
    {
        var memory = _testArray.ToMemory();
        Assert.Equal(_testArray.Length, memory.Length);
        Assert.Equal(_testArray, memory.ToArray());
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(0, 5)]
    [InlineData(4, 1)]
    public void ToMemory_ConvertsSubarray(int start, int length)
    {
        var memory = _testArray.ToMemory(start, length);
        Assert.Equal(length, memory.Length);
        Assert.Equal(_testArray.AsMemory(start, length).ToArray(), memory.ToArray());
    }

    [Fact]
    public void ToReadOnlySpan_ConvertsEntireArray()
    {
        var span = _testArray.ToReadOnlySpan();
        Assert.Equal(_testArray.Length, span.Length);
        Assert.Equal(_testArray, span.ToArray());
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(0, 5)]
    [InlineData(4, 1)]
    public void ToReadOnlySpan_ConvertsSubarray(int start, int length)
    {
        var span = _testArray.ToReadOnlySpan(start, length);
        Assert.Equal(length, span.Length);
        Assert.Equal(_testArray.AsSpan(start, length).ToArray(), span.ToArray());
    }

    [Fact]
    public void ToReadOnlyMemory_ConvertsEntireArray()
    {
        var memory = _testArray.ToReadOnlyMemory();
        Assert.Equal(_testArray.Length, memory.Length);
        Assert.Equal(_testArray, memory.ToArray());
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(0, 5)]
    [InlineData(4, 1)]
    public void ToReadOnlyMemory_ConvertsSubarray(int start, int length)
    {
        var memory = _testArray.ToReadOnlyMemory(start, length);
        Assert.Equal(length, memory.Length);
        Assert.Equal(_testArray.AsMemory(start, length).ToArray(), memory.ToArray());
    }

    [Fact]
    public void ToReadOnlySequence_ConvertsEntireArray()
    {
        var sequence = _testArray.ToReadOnlySequence();
        Assert.Equal(_testArray.Length, sequence.Length);
        Assert.Equal(_testArray, sequence.ToArray());
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(0, 5)]
    [InlineData(4, 1)]
    public void ToReadOnlySequence_ConvertsSubarray(int start, int length)
    {
        var sequence = _testArray.ToReadOnlySequence(start, length);
        Assert.Equal(length, sequence.Length);
        Assert.Equal(_testArray.AsSpan(start, length).ToArray(), sequence.ToArray());
    }

    [Theory]
    [InlineData(-1, 2)]
    [InlineData(3, 3)]
    [InlineData(0, 6)]
    public void InvalidArguments_ThrowException(int start, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _testArray.ToSpan(start, length));
        Assert.Throws<ArgumentOutOfRangeException>(() => _testArray.ToMemory(start, length));
        Assert.Throws<ArgumentOutOfRangeException>(() => _testArray.ToReadOnlySpan(start, length));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _testArray.ToReadOnlyMemory(start, length)
        );
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _testArray.ToReadOnlySequence(start, length)
        );
    }
}
