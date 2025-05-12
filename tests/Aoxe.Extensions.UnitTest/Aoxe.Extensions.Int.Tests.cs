namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsIntTests
{
    #region GetEnumerator Tests

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    public void GetEnumerator_ValidCount_GeneratesCorrectSequence(int count)
    {
        // Arrange
        var expected = Enumerable.Range(0, count);

        // Act
        var result = new List<int>();
        foreach (var i in count)
        {
            result.Add(i);
        }

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetEnumerator_NegativeCount_ThrowsException()
    {
        // Arrange
        const int count = -5;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            foreach (var _ in count)
            {
                // Just iterate to trigger exception
            }
        });
    }

    #endregion

    #region Range Tests

    [Theory]
    [InlineData(0, 0, new int[0])]
    [InlineData(5, 0, new[] { 0, 1, 2, 3, 4 })]
    [InlineData(5, 3, new[] { 3, 4, 5, 6, 7 })]
    [InlineData(10, -2, new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 })]
    public void Range_ValidParameters_GeneratesCorrectSequence(int count, int start, int[] expected)
    {
        // Act
        var result = count.Range(start);

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(count, result.Count());
    }

    [Fact]
    public void Range_WithNegativeCount_ThrowsException()
    {
        // Arrange
        const int count = -5;
        const int start = 0;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => count.Range(start).ToList());
    }

    [Fact]
    public void Range_EdgeCaseMaxValue_HandlesCorrectly()
    {
        // Arrange
        const int start = int.MaxValue - 1;
        const int count = 2;

        // Act
        var result = count.Range(start).ToList();

        // Assert
        Assert.Equal(new[] { int.MaxValue - 1, int.MaxValue }, result);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void GetEnumerator_UsesRangeMethod_IntegrationTest()
    {
        // Arrange
        const int count = 5;

        // Act
        var rangeResult = count.Range().ToList();
        var enumeratorResult = new List<int>();
        foreach (var i in count)
        {
            enumeratorResult.Add(i);
        }

        // Assert
        Assert.Equal(rangeResult, enumeratorResult);
    }

    #endregion
}
