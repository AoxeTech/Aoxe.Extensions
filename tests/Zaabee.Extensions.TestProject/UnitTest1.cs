using System.Net.Http;
using Xunit;

namespace Zaabee.Extensions.TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test()
        {
            var testModel = new TestModel();
            var id1 = testModel.Id;
            var id2 = testModel.Id;
        }
    }
}