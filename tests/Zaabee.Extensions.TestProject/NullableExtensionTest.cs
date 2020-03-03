using Xunit;

namespace Zaabee.Extensions.TestProject
{
    public class NullableExtensionTest
    {
        [Fact]
        public void IsNullTest()
        {
            int? i0 = null;
            int? i1 = 1;
            Assert.True(i0.IsNull());
            Assert.False(i1.IsNull());
        }

        [Fact]
        public void IsNotNullTest()
        {
            int? i0 = null;
            int? i1 = 1;
            Assert.False(i0.IsNotNull());
            Assert.True(i1.IsNotNull());
        }

        [Fact]
        public void IsNullOrDefaultTest()
        {
            int? i0 = null;
            int? i1 = 0;
            Assert.True(i0.IsNullOrDefault());
            Assert.False(i1.IsNullOrDefault());
        }

        [Fact]
        public void TryGetValueTest()
        {
            int? i0 = null;
            Assert.Equal(0, i0.TryGetValue());
        }
    }
}