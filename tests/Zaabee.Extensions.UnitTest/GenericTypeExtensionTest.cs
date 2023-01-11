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
}