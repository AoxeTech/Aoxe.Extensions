namespace Zaabee.Extensions.UnitTest;

public class DictionaryExtensionsTest
{
    [Fact]
    public void DictionaryToDynamicTest()
    {
#if NETSTANDARD2_0
        var dictionary = new Dictionary<string, object>
#else
        var dictionary = new Dictionary<string, object?>
#endif
        {
            { "Id", Guid.NewGuid() },
            { "Name", "Zaabee" },
            { "Birthday", DateTime.Now }
        };
        var dynamicObj = dictionary!.DictionaryToDynamic();
        Assert.Equal(dictionary["Id"], dynamicObj.Id);
        Assert.Equal(dictionary["Name"], dynamicObj.Name);
        Assert.Equal(dictionary["Birthday"], dynamicObj.Birthday);
    }

    [Fact]
    public void DictionaryToObjectTest()
    {
#if NETSTANDARD2_0
        var dictionary = new Dictionary<string, object>
#else
        var dictionary = new Dictionary<string, object?>
#endif
        {
            { "Id", Guid.NewGuid() },
            { "Name", "Zaabee" },
            { "Birthday", DateTime.Now }
        };
        var testModel = dictionary!.ToObject<TestModel>();
        Assert.NotNull(testModel);
        Assert.Equal(dictionary["Id"], testModel.Id);
        Assert.Equal(dictionary["Name"], testModel.Name);
        Assert.Equal(dictionary["Birthday"], testModel.Birthday);
    }
}