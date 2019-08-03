using Xunit;
using Zaabee.Extensions.Commons;

namespace Zaabee.Extensions.TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void LongExtensionTest()
        {
            var i1 = 923904999999998791;
            var i2 = 923904999999998792;
            var i3 = 923904999999998793;
            var i4 = 923904999999998794;
            var i5 = 923904999999998795;
            var i6 = 923904999999998796;
            var s1 = i1.ToString(NumberSystem.Hexatrigesimal);
            var s2 = i2.ToString(NumberSystem.Hexatrigesimal);
            var s3 = i3.ToString(NumberSystem.Hexatrigesimal);
            var s4 = i4.ToString(NumberSystem.Hexatrigesimal);
            var s5 = i5.ToString(NumberSystem.Hexatrigesimal);
            var s6 = i6.ToString(NumberSystem.Hexatrigesimal);
            var r1 = s1.ToLong(NumberSystem.Hexatrigesimal);
            var r2 = s2.ToLong(NumberSystem.Hexatrigesimal);
            var r3 = s3.ToLong(NumberSystem.Hexatrigesimal);
            var r4 = s4.ToLong(NumberSystem.Hexatrigesimal);
            var r5 = s5.ToLong(NumberSystem.Hexatrigesimal);
            var r6 = s6.ToLong(NumberSystem.Hexatrigesimal);
            Assert.Equal(r1,i1);
            Assert.Equal(r2,i2);
            Assert.Equal(r3,i3);
            Assert.Equal(r4,i4);
            Assert.Equal(r5,i5);
            Assert.Equal(r6,i6);
        }
    }
}