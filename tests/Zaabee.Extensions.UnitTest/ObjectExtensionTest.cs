namespace Zaabee.Extensions.UnitTest;

public class ObjectExtensionTest
{
    [Fact]
    public void CastToTest()
    {
        var name = "Zaabee";
        var birthday = DateTime.UtcNow;
        object testModel = new TestModel { Name = name, Birthday = birthday };
        var result = testModel.CastTo<TestModel>();
        Assert.Equal(name, result.Name);
        Assert.Equal(birthday, result.Birthday);
    }
}