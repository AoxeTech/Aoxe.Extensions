namespace Aoxe.Extensions.UnitTest;

public class EnumerableExtensionTest
{
    [Fact]
    public void AddRangeTest()
    {
        IList<int> list = new List<int>();
        var myCollection = new MyCollection<int>();
        var collection = Enumerable.Range(0, 5).ToList();
        list.AddRange(collection);
        myCollection.AddRange(collection);
        Assert.Equal(list.Count, myCollection.Count);
        Assert.True(list.All(p => myCollection.Contains(p)));
    }

    [Theory]
    [InlineData(10, 5)]
    public void IndexOfTest(int total, int index)
    {
        var testModels = Enumerable
            .Range(0, total)
            .Select(_ => new TestModel { Name = "Alice" })
            .ToList();
        var testModel = testModels.Skip(index).First();
        Assert.Equal(index, testModels.IndexOf(testModel));
    }

    [Theory]
    [InlineData(10)]
    public void IndexOfNotExistTest(int total)
    {
        var testModels = Enumerable.Range(0, total).Select(_ => new TestModel { Name = "Alice" });
        var testModel = new TestModel { Name = "Bob" };
        Assert.Equal(-1, testModels.IndexOf(testModel));
    }

    [Fact]
    public void NotContainsTest()
    {
        var testInts = Enumerable.Range(0, 10).ToList();
        Assert.False(testInts.NotContains(0));
        Assert.False(testInts.NotContains(9));
        Assert.True(testInts.NotContains(10));
    }

    [Fact]
    public void ToListTest()
    {
        var i = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        var even = i.ToList(p => p % 2 == 0);
        Assert.True(even.All(p => p % 2 == 0));
    }

    [Fact]
    public void ForEachTest()
    {
        var testModels = new List<TestModel>
        {
            new() { Name = "Alice" },
            new() { Name = "Alice" },
            new() { Name = "Alice" },
            new() { Name = "Alice" },
            new() { Name = "Alice" }
        };
        var enumerable = testModels.AsEnumerable();
        enumerable.ForEach(p => p!.Name = "Bob");
        Assert.True(enumerable.All(p => p.Name == "Bob"));
        enumerable.ForEach(null);
    }

    [Fact]
    public void ForEachLazyTest()
    {
        var testModels = new List<TestModel>
        {
            new() { Name = "Alice", Birthday = new DateTime(2000, 1, 1) },
            new() { Name = "Alice", Birthday = new DateTime(2000, 1, 1) },
            new() { Name = "Alice", Birthday = new DateTime(2000, 1, 1) },
            new() { Name = "Alice", Birthday = new DateTime(2000, 1, 1) },
            new() { Name = "Alice", Birthday = new DateTime(2000, 1, 1) }
        };
        var enumerable = testModels.AsEnumerable();
        var result = enumerable
            .ForEachLazy(p => p!.Name = "Bob")
            .ForEachLazy(p => p!.Birthday = new DateTime(2001, 1, 1));
        Assert.True(result.All(p => p!.Name == "Bob" && p.Birthday == new DateTime(2001, 1, 1)));
        enumerable.ForEachLazy(null);
    }

    [Fact]
    public void IsNullOrEmptyTest()
    {
        IList<TestModel>? testModels = null;
        Assert.True(testModels.IsNullOrEmpty());
        testModels = new List<TestModel>();
        Assert.True(testModels.IsNullOrEmpty());
        testModels.Add(new TestModel());
        Assert.False(testModels.IsNullOrEmpty());
    }

    [Fact]
    public void IndexForeach_EmptyCollection_NoActionExecuted()
    {
        // Arrange
        var list = new List<string>();
        var count = 0;

        // Act
        list.IndexForeach((i, item) => count++);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void IndexForeach_SingleItem_CorrectIndexAndValue()
    {
        // Arrange
        var list = new List<string> { "test" };
        var capturedIndex = -1;
        var capturedItem = string.Empty;

        // Act
        list.IndexForeach(
            (i, item) =>
            {
                capturedIndex = i;
                capturedItem = item;
            }
        );

        // Assert
        Assert.Equal(0, capturedIndex);
        Assert.Equal("test", capturedItem);
    }

    [Fact]
    public void IndexForeach_MultipleItems_CorrectIndexesAndValues()
    {
        // Arrange
        var list = new List<string> { "one", "two", "three" };
        var indexes = new List<int>();
        var items = new List<string>();

        // Act
        list.IndexForeach(
            (i, item) =>
            {
                indexes.Add(i);
                items.Add(item);
            }
        );

        // Assert
        Assert.Equal(new[] { 0, 1, 2 }, indexes);
        Assert.Equal(new[] { "one", "two", "three" }, items);
    }

    // [Fact]
    // public void IndexForeach_LargeCollection_CorrectIndexing()
    // {
    //     // Arrange
    //     var list = Enumerable.Range(1, 1000);
    //     var lastIndex = -1;
    //     var itemSum = 0;
    //
    //     // Act
    //     list.IndexForeach(
    //         (i, item) =>
    //         {
    //             lastIndex = i;
    //             itemSum += item;
    //         }
    //     );
    //
    //     // Assert
    //     Assert.Equal(999, lastIndex);
    //     Assert.Equal(500500, itemSum); // Sum of numbers 1 to 1000
    // }
}
