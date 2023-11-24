namespace Zaabee.Extensions.UnitTest;

public class IntExtensionTest
{
    [Theory]
    [InlineData(10)]
    public void GetEnumeratorTest(int num)
    {
        var result = new List<int>();
        foreach (var i in num)
            result.Add(i);
        var ints = Enumerable.Range(0, num).ToList();
        Assert.Equal(ints.Count, result.Count);
        for (var i = 0; i < ints.Count; i++)
            Assert.Equal(ints[i], result[i]);
    }
}
