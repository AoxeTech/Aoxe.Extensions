namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsReadOnlyMemoryTests
{
    #region Basic Functionality Tests

    [Fact]
    public void ToReadOnlySequence_WithEmptyMemory_ReturnsEmptySequence()
    {
        // Arrange - Create empty memory
        var memory = ReadOnlyMemory<int>.Empty;

        // Act - Convert to ReadOnlySequence
        var sequence = memory.ToReadOnlySequence();

        // Assert - Verify empty sequence properties
        Assert.Equal(0, sequence.Length);
        Assert.True(sequence.IsEmpty);
        Assert.True(sequence.First.IsEmpty);
    }

    [Theory]
    [InlineData(new[] { 1 })]
    [InlineData(new[] { 1, 2, 3 })]
    [InlineData(new[] { 0, int.MaxValue, int.MinValue })]
    public void ToReadOnlySequence_WithIntMemory_ReturnsValidSequence(int[] data)
    {
        // Arrange - Create memory from test data
        var memory = new ReadOnlyMemory<int>(data);

        // Act - Convert to ReadOnlySequence
        var sequence = memory.ToReadOnlySequence();

        // Assert - Verify sequence content and structure
        Assert.Equal(data.Length, sequence.Length);
        Assert.Equal(data, sequence.ToArray());
    }

    [Fact]
    public void ToReadOnlySequence_WithStringMemory_ReturnsValidSequence()
    {
        // Arrange - Create string array memory
        var data = new[] { "A", "B", "C" };
        var memory = new ReadOnlyMemory<string>(data);

        // Act - Convert to ReadOnlySequence
        var sequence = memory.ToReadOnlySequence();

        // Assert - Verify sequence content
        Assert.Equal(3, sequence.Length);
        Assert.Equal(data, sequence.ToArray());
    }

    #endregion

    #region Boundary Condition Tests

    [Fact]
    public void ToReadOnlySequence_WithLargeMemory_HandlesCorrectly()
    {
        // Arrange - Create large memory block
        var data = new byte[10_000];
        new Random().NextBytes(data);
        var memory = new ReadOnlyMemory<byte>(data);

        // Act - Convert to ReadOnlySequence
        var sequence = memory.ToReadOnlySequence();

        // Assert - Verify data integrity
        Assert.Equal(data.Length, sequence.Length);
        Assert.Equal(data, sequence.ToArray());
    }

    #endregion

    #region Structure Verification Tests

    [Fact]
    public void ToReadOnlySequence_SingleSegment_ConfirmsStructure()
    {
        // Arrange - Create double array memory
        var data = new[] { 1.1, 2.2, 3.3 };
        var memory = new ReadOnlyMemory<double>(data);

        // Act - Convert to ReadOnlySequence
        var sequence = memory.ToReadOnlySequence();

        // Assert - Verify sequence structure
        Assert.True(sequence.IsSingleSegment);
        var position = sequence.Start;
        Assert.True(sequence.TryGet(ref position, out var memorySegment));
        Assert.Equal(data.Length, memorySegment.Length);
        Assert.Equal(data, memorySegment.ToArray());
    }

    #endregion

    #region Special Type Tests

    [Fact]
    public void ToReadOnlySequence_WithCustomType_WorksCorrectly()
    {
        // Arrange - Create custom type memory
        var data = new[] { new CustomType(1), new CustomType(2), new CustomType(3) };
        var memory = new ReadOnlyMemory<CustomType>(data);

        // Act - Convert to ReadOnlySequence
        var sequence = memory.ToReadOnlySequence();

        // Assert - Verify custom type handling
        Assert.Equal(3, sequence.Length);
        Assert.Equal(data, sequence.ToArray());
    }

    private class CustomType(int Value);

    #endregion
}
