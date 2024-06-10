namespace Aoxe.Extensions.UnitTest;

public class ObjectExtensionTest
{
    [Fact]
    public void CastToTest()
    {
        var name = "Aoxe";
        var birthday = DateTime.UtcNow;
        object testModel = new TestModel { Name = name, Birthday = birthday };
        var result = testModel.CastTo<TestModel>();
        Assert.Equal(name, result.Name);
        Assert.Equal(birthday, result.Birthday);
    }

    [Fact]
    public void AsDictionaryTest()
    {
        var testModel = new TestModel { Name = "Aoxe", Birthday = DateTime.UtcNow };
        var dictionary = testModel.AsDictionary();
        Assert.Equal(testModel.Id, dictionary["Id"]);
        Assert.Equal(testModel.Name, dictionary["Name"]);
        Assert.Equal(testModel.Birthday, dictionary["Birthday"]);
    }
}
