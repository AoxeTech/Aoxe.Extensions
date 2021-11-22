namespace Zaabee.Extensions.UnitTest;

public class EnumExtensionTest
{
    [Fact]
    public void GetDescriptionTest()
    {
        Assert.Equal("A", TestEnum.Create.GetDescription());
        Assert.Equal("B", TestEnum.Delete.GetDescription());
        Assert.Equal("C", TestEnum.Modify.GetDescription());
        Assert.Equal("D", TestEnum.Query.GetDescription());
    }

    [Fact]
    public void GetDescriptionsTest()
    {
        Assert.Equal("A,B,C", (TestEnum.Create | TestEnum.Delete | TestEnum.Modify).GetDescriptions());
        Assert.Equal("A B D", (TestEnum.Create | TestEnum.Delete | TestEnum.Query).GetDescriptions(" "));
    }
}