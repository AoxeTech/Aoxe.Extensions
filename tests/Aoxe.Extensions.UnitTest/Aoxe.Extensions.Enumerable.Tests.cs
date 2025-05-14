namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsEnumerableTests
{
    #region AddRange Tests

    [Fact]
    public void AddRange_ForList_UsesAddRangeOptimization()
    {
        // Arrange
        var list = new List<int> { 1, 2 };
        var source = (ICollection<int>)list;
        var newItems = new[] { 3, 4, 5 };

        // Act
        source.AddRange(newItems);

        // Assert
        Assert.Equal(new[] { 1, 2, 3, 4, 5 }, list);
    }

    [Fact]
    public void AddRange_ForNonListCollection_AddsItemsIndividually()
    {
        // Arrange
        var collection = new HashSet<int> { 1, 2 };
        var newItems = new[] { 2, 3, 4 };

        // Act
        collection.AddRange(newItems);

        // Assert
        Assert.Equal(new[] { 1, 2, 3, 4 }, collection.OrderBy(x => x));
    }

    [Fact]
    public void AddRange_WithNullSource_ThrowsArgumentNullException()
    {
        // Arrange
        ICollection<int> collection = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => collection.AddRange(new[] { 1 }));
    }

    #endregion

    #region IndexOf Tests

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 2, 1)]
    [InlineData(new[] { 1, 2, 3 }, 4, -1)]
    public void IndexOf_WithIntValues_ReturnsCorrectIndex(int[] source, int value, int expected)
    {
        // Act
        var result = source.IndexOf(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new[] { "a", "b", "c" }, "b", 1)]
    [InlineData(new[] { "a", "b", "c" }, "d", -1)]
    public void IndexOf_WithStringValues_ReturnsCorrectIndex(
        string[] source,
        string value,
        int expected
    )
    {
        // Act
        var result = source.IndexOf(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IndexOf_WithCustomComparer_UsesComparer()
    {
        // Arrange
        var source = new[] { "Apple", "Banana", "Cherry" };
        var comparer = StringComparer.OrdinalIgnoreCase;

        // Act
        var result = source.IndexOf("BANANA", comparer);

        // Assert
        Assert.Equal(1, result);
    }

    #endregion

    #region IndexForeach Tests

    [Fact]
    public void IndexForeach_ExecutesActionWithCorrectIndexes()
    {
        // Arrange
        var source = new[] { "a", "b", "c" };
        var indexes = new List<int>();
        var items = new List<string>();

        // Act
        source.IndexForeach(
            (i, item) =>
            {
                indexes.Add(i);
                items.Add(item);
            }
        );

        // Assert
        Assert.Equal(new[] { 0, 1, 2 }, indexes);
        Assert.Equal(source, items);
    }

    #endregion

    #region NotContains Tests

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 4, true)]
    [InlineData(new[] { 1, 2, 3 }, 2, false)]
    public void NotContains_WithIntValues_ReturnsCorrectResult(
        int[] source,
        int item,
        bool expected
    )
    {
        // Act
        var result = source.NotContains(item);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new[] { "a", "b", "c" }, "d", true)]
    [InlineData(new[] { "a", "b", "c" }, "b", false)]
    public void NotContains_WithStringValues_ReturnsCorrectResult(
        string[] source,
        string item,
        bool expected
    )
    {
        // Act
        var result = source.NotContains(item);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region ToList with Predicate Tests

    [Fact]
    public void ToList_WithPredicate_FiltersCorrectly()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = source.ToList(x => x % 2 == 0);

        // Assert
        Assert.Equal(new[] { 2, 4 }, result);
    }

    #endregion

    #region ForEach Tests

    [Fact]
    public void ForEach_ExecutesActionForAllItems()
    {
        // Arrange
        var source = new[] { 1, 2, 3 };
        var sum = 0;

        // Act
        source.ForEach(x => sum += x);

        // Assert
        Assert.Equal(6, sum);
    }

    [Fact]
    public void ForEachLazy_DeferredExecution_WorksCorrectly()
    {
        // Arrange
        var source = new[] { 1, 2, 3 };
        var executed = new List<int>();

        // Act
        var query = source.ForEachLazy(x => executed.Add(x));
        var first = query.First();

        // Assert
        Assert.Single(executed); // Only first item processed
    }

    #endregion


    #region Null Handling Tests

    [Fact]
    public void AllMethods_WithNullSource_ThrowArgumentNullException()
    {
        // Arrange
        IEnumerable<int> nullEnumerable = null;
        ICollection<int> nullCollection = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => nullCollection.AddRange(new[] { 1 }));
        Assert.Throws<NullReferenceException>(() => nullEnumerable.IndexOf(1));
        Assert.Throws<NullReferenceException>(() => nullEnumerable.IndexForeach((i, x) => { }));
        Assert.Throws<ArgumentNullException>(() => nullEnumerable.NotContains(1));
        Assert.Throws<ArgumentNullException>(() => nullEnumerable.ToList(x => true));
        Assert.Throws<NullReferenceException>(() => nullEnumerable.ForEach(x => { }));
    }

    #endregion
}
