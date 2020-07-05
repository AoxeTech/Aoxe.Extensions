using Xunit;

namespace Zaabee.Extensions.UnitTest
{
    public class NullableExtensionTest
    {
        [Fact]
        public void IsNullTest()
        {
            int? i0 = null;
            int? i1 = 1;
            object i2 = null;
            var i3 = new object();
            Assert.True(i0.IsNull());
            Assert.False(i1.IsNull());
            Assert.True(i2.IsNull());
            Assert.False(i3.IsNull());
        }

        [Fact]
        public void IsNotNullTest()
        {
            int? i0 = null;
            int? i1 = 1;
            object i2 = null;
            var i3 = new object();
            Assert.False(i0.IsNotNull());
            Assert.True(i1.IsNotNull());
            Assert.False(i2.IsNotNull());
            Assert.True(i3.IsNotNull());
        }

        [Fact]
        public void IsNullOrDefaultTest()
        {
            int? i0 = null;
            int? i1 = 0;
            object i2 = null;
            var i3 = new object();
            Assert.True(i0.IsNullOrDefault());
            Assert.False(i1.IsNullOrDefault());
            Assert.True(i2.IsNullOrDefault());
            Assert.False(i3.IsNullOrDefault());
        }

        [Fact]
        public void TryGetValueTest()
        {
            int? i0 = null;
            Assert.Equal(0, i0.TryGetValue());
        }
    }
}