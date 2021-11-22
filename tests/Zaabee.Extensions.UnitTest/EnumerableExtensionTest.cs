namespace Zaabee.Extensions.UnitTest;

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

        IList<int>? nullList = null;
        Assert.Throws<ArgumentNullException>(() => nullList.AddRange(collection));
    }

    [Theory]
    [InlineData(10, 5)]
    public void IndexOfTest(int total, int index)
    {
        IEnumerable<TestModel> testModels = Enumerable.Range(0, total)
            .Select(_ => new TestModel {Name = "Alice"}).ToList();
        var testModel = testModels.Skip(index).First();
        Assert.Equal(index, testModels.IndexOf(testModel));
    }

    [Theory]
    [InlineData(10)]
    public void IndexOfNotExistTest(int total)
    {
        IEnumerable<TestModel> testModels = Enumerable.Range(0, total)
            .Select(_ => new TestModel {Name = "Alice"}).ToList();
        var testModel = new TestModel {Name = "Bob"};
        Assert.Equal(-1, testModels.IndexOf(testModel));
    }

    [Fact]
    public void NotContainsTest()
    {
        IEnumerable<int> testInts = Enumerable.Range(0, 10).ToList();
        Assert.False(testInts.NotContains(0));
        Assert.False(testInts.NotContains(9));
        Assert.True(testInts.NotContains(10));
    }

    [Fact]
    public void ToListTest()
    {
        var i = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
        var even = i.ToList(p => p % 2 == 0);
        Assert.True(even.All(p => p % 2 == 0));
    }

    [Fact]
    public void ForEachTest()
    {
        var testModels = new List<TestModel>
        {
            new() {Name = "Alice"},
            new() {Name = "Alice"},
            new() {Name = "Alice"},
            new() {Name = "Alice"},
            new() {Name = "Alice"}
        };
        var enumerable = testModels.AsEnumerable();
        enumerable.ForEach(p => p.Name = "Bob");
        Assert.True(enumerable.All(p => p.Name == "Bob"));
        enumerable.ForEach(null);
    }

    [Fact]
    public void ForEachLazyTest()
    {
        var testModels = new List<TestModel>
        {
            new() {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
            new() {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
            new() {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
            new() {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
            new() {Name = "Alice", Birthday = new DateTime(2000, 1, 1)}
        };
        var enumerable = testModels.AsEnumerable();
        var result = enumerable.ForEachLazy(p => p.Name = "Bob")
            .ForEachLazy(p => p.Birthday = new DateTime(2001, 1, 1))
            .ToList();
        Assert.True(result.All(p => p.Name == "Bob" && p.Birthday == new DateTime(2001, 1, 1)));
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
}