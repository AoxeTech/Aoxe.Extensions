using System.Collections.Generic;
using System.Linq;
using Xunit;
using Zaabee.Extensions.Commons;

namespace Zaabee.Extensions.UnitTest
{
    public class IntExtensionTest
    {
        [Theory]
        [InlineData(10)]
        public void GetEnumeratorTest(int num)
        {
            var nums = Enumerable.Range(0, num).ToList();
            var result = new List<int>();
            foreach (var i in num)
                result.Add(i);
            Assert.Equal(nums.Count, result.Count);
            for (var i = 0; i < nums.Count; i++)
                Assert.Equal(nums[i], result[i]);
        }

        [Theory]
        [InlineData(int.MaxValue, 32)]
        [InlineData(int.MaxValue, 36)]
        [InlineData(int.MaxValue, 62)]
        [InlineData(int.MinValue + 1, 32)]
        [InlineData(int.MinValue + 1, 36)]
        [InlineData(int.MinValue + 1, 62)]
        public void Test(int value, int radix)
        {
            var str = value.ToString(radix);
            var result = str.ToInt(radix);
            Assert.Equal(value, result);
        }
        
        [Theory]
        [InlineData(int.MaxValue, NumerationSystem.Binary)]
        [InlineData(int.MaxValue, NumerationSystem.Decimalism)]
        [InlineData(int.MaxValue, NumerationSystem.Duotricemary)]
        [InlineData(int.MaxValue, NumerationSystem.Hexadecimal)]
        [InlineData(int.MaxValue, NumerationSystem.Octal)]
        [InlineData(int.MaxValue, NumerationSystem.SixtyTwoAry)]
        [InlineData(int.MaxValue, NumerationSystem.ThirtySixAry)]
        [InlineData(int.MinValue + 1, NumerationSystem.Binary)]
        [InlineData(int.MinValue + 1, NumerationSystem.Decimalism)]
        [InlineData(int.MinValue + 1, NumerationSystem.Duotricemary)]
        [InlineData(int.MinValue + 1, NumerationSystem.Hexadecimal)]
        [InlineData(int.MinValue + 1, NumerationSystem.Octal)]
        [InlineData(int.MinValue + 1, NumerationSystem.SixtyTwoAry)]
        [InlineData(int.MinValue + 1, NumerationSystem.ThirtySixAry)]
        public void TestByNumerationSystem(int value, NumerationSystem numerationSystem)
        {
            var str = value.ToString(numerationSystem);
            var result = str.ToInt(numerationSystem);
            Assert.Equal(value, result);
        }
    }
}