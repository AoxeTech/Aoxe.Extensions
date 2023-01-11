namespace Zaabee.Extensions.UnitTest;

public class GenericTypeExtensionTest
{
    [Fact]
    public void ConvertToDictionaryTest()
    {
        var obj = new { Name = "Zaabee", Age = 18 };
        var dict = obj.ConvertToDictionary();
        foreach (var propertyInfo in obj.GetType().GetProperties())
            Assert.Equal(propertyInfo.GetValue(obj), dict[propertyInfo.Name]);
    }

    [Fact]
    public void ConvertToDatatableTest()
    {
        var objs = new List<TestModel>
        {
            new() { Name = "Zaaby", Birthday = DateTime.UtcNow },
            new() { Name = "Zaabee", Birthday = DateTime.UtcNow }
        };
        var table = objs.ConvertToDataTable();
        for (var i = 0; i < objs.Count; i++)
        {
            var obj = objs[i];
            var row = table.Rows[i];

            foreach (var propertyInfo in typeof(TestModel).GetProperties())
                Assert.Equal(propertyInfo.GetValue(obj), row[propertyInfo.Name]);
        }
    }
}