using System.IO;
using Xunit;

namespace Zaabee.Extensions.UnitTest
{
    public class AsyncExtensionTest
    {
        [Fact]
        public void RunSyncTest()
        {
            var bytes = new byte[1024];
            var ms = new MemoryStream();
            ms.WriteAsync(bytes,0,1024).RunSync();
            Assert.Equal(0, ms.ReadAsync(bytes, 0, 1024).RunSync());
        }
    }
}