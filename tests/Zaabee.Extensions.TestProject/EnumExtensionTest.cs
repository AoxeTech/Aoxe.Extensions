using System;
using System.ComponentModel;
using Xunit;

namespace Zaabee.Extensions.TestProject
{
    public class EnumExtensionTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal("A", TestEnum.Create.GetDescription());
            Assert.Equal("B", TestEnum.Delete.GetDescription());
            Assert.Equal("C", TestEnum.Modify.GetDescription());
            Assert.Equal("D", TestEnum.Query.GetDescription());

            Assert.Equal("A,B,C", (TestEnum.Create | TestEnum.Delete | TestEnum.Modify).GetDescriptions());
            Assert.Equal("A B D", (TestEnum.Create | TestEnum.Delete | TestEnum.Query).GetDescriptions(" "));
        }
    }
}