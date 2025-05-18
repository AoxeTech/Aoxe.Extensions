namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionArrayTests
{
    // Test array for normal cases
    private readonly int[] _sampleArray = [1, 2, 3, 4, 5];

    // Test null array for exception cases
    private readonly int[]? _nullArray = null;

    #region ToSpan Tests

    [Fact]
    public void ToSpan_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _nullArray.ToSpan());
    }

    [Fact]
    public void ToSpan_ValidArray_ReturnsCorrectSpan()
    {
        var span = _sampleArray.ToSpan();
        Assert.Equal(_sampleArray.Length, span.Length);
        Assert.True(span.SequenceEqual(_sampleArray.AsSpan()));
    }

    [Theory]
    [InlineData(-1, 2)] // Negative start
    [InlineData(0, -1)] // Negative length
    [InlineData(3, 3)] // Length exceeds array bounds
    public void ToSpan_InvalidParameters_ThrowsArgumentOutOfRange(int start, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sampleArray.ToSpan(start, length));
    }

    [Fact]
    public void ToSpan_ValidRange_ReturnsCorrectSubspan()
    {
        const int start = 1;
        const int length = 3;
        var span = _sampleArray.ToSpan(start, length);

        Assert.Equal(length, span.Length);
        Assert.True(span.SequenceEqual(_sampleArray.AsSpan(start, length)));
    }

    #endregion

    #region ToMemory Tests

    [Fact]
    public void ToMemory_ValidArray_ReturnsCorrectMemory()
    {
        var memory = _sampleArray.ToMemory();
        Assert.Equal(_sampleArray.Length, memory.Length);
        Assert.True(memory.Span.SequenceEqual(_sampleArray.AsSpan()));
    }

    [Theory]
    [InlineData(-1, 2)]
    [InlineData(0, -1)]
    [InlineData(4, 2)]
    public void ToMemory_InvalidParameters_ThrowsArgumentOutOfRange(int start, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sampleArray.ToMemory(start, length));
    }

    [Fact]
    public void ToMemory_ValidRange_ReturnsCorrectSubmemory()
    {
        const int start = 2;
        const int length = 2;
        var memory = _sampleArray.ToMemory(start, length);

        Assert.Equal(length, memory.Length);
        Assert.True(memory.Span.SequenceEqual(_sampleArray.AsSpan(start, length)));
    }

    #endregion

    #region ReadOnlySpan Tests


    [Fact]
    public void ToReadOnlySpan_ValidArray_ReturnsCorrectReadOnlySpan()
    {
        var ros = _sampleArray.ToReadOnlySpan();
        Assert.Equal(_sampleArray.Length, ros.Length);
        Assert.True(ros.SequenceEqual(_sampleArray.AsSpan()));
    }

    [Theory]
    [InlineData(-1, 2)]
    [InlineData(0, -1)]
    [InlineData(5, 1)]
    public void ToReadOnlySpan_InvalidParameters_ThrowsException(int start, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _sampleArray.ToReadOnlySpan(start, length)
        );
    }

    #endregion

    #region ReadOnlyMemory Tests


    [Fact]
    public void ToReadOnlyMemory_ValidArray_ReturnsCorrectMemory()
    {
        var rom = _sampleArray.ToReadOnlyMemory();
        Assert.Equal(_sampleArray.Length, rom.Length);
        Assert.True(rom.Span.SequenceEqual(_sampleArray.AsSpan()));
    }

    [Theory]
    [InlineData(-1, 2)]
    [InlineData(0, -1)]
    [InlineData(5, 1)]
    public void ToReadOnlyMemory_InvalidParameters_ThrowsException(int start, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _sampleArray.ToReadOnlyMemory(start, length)
        );
    }

    #endregion

    #region ReadOnlySequence Tests

    [Fact]
    public void ToReadOnlySequence_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _nullArray.ToReadOnlySequence());
    }

    [Fact]
    public void ToReadOnlySequence_ValidArray_ReturnsCompleteSequence()
    {
        var sequence = _sampleArray.ToReadOnlySequence();
        Assert.Equal(_sampleArray.Length, sequence.Length);
        Assert.True(sequence.ToArray().AsSpan().SequenceEqual(_sampleArray));
    }

    [Theory]
    [InlineData(-1, 2)]
    [InlineData(0, -1)]
    [InlineData(5, 1)]
    public void ToReadOnlySequence_InvalidParameters_ThrowsException(int start, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _sampleArray.ToReadOnlySequence(start, length)
        );
    }

    [Fact]
    public void ToReadOnlySequence_ValidRange_ReturnsCorrectSubsequence()
    {
        const int start = 1;
        const int length = 3;
        var sequence = _sampleArray.ToReadOnlySequence(start, length);

        Assert.Equal(length, sequence.Length);
        Assert.True(sequence.ToArray().AsSpan().SequenceEqual(_sampleArray.AsSpan(start, length)));
    }

    #endregion

    // Additional edge case tests
    [Fact]
    public void EmptyArray_AllMethods_HandleCorrectly()
    {
        var emptyArray = Array.Empty<int>();

        // Test all methods with empty array
        Assert.Equal(0, emptyArray.ToSpan().Length);
        Assert.Equal(0, emptyArray.ToMemory().Length);
        Assert.Equal(0, emptyArray.ToReadOnlySpan().Length);
        Assert.Equal(0, emptyArray.ToReadOnlyMemory().Length);
        Assert.Equal(0, emptyArray.ToReadOnlySequence().Length);
    }

    [Fact]
    public void ZeroLengthRange_AllMethods_HandleCorrectly()
    {
        var memory = _sampleArray.ToMemory(2, 0);
        Assert.Equal(0, memory.Length);

        var sequence = _sampleArray.ToReadOnlySequence(3, 0);
        Assert.Equal(0, sequence.Length);
    }
}
