using System;
using Xunit;

namespace Zaabee.Extensions.TestProject
{
    public class BoolExtensionTest
    {
        [Fact]
        public void IfThrowTest()
        {
            false.IfThrow(new ArgumentException());
            Assert.Throws<ArgumentException>(() => true.IfThrow(new ArgumentException()));
        }

        [Fact]
        public void IfTrue()
        {
            var i = 1;
            true.IfTrue(() => i++);
            Assert.Equal(2, i);
        }

        [Fact]
        public void IfFalse()
        {
            var i = 1;
            false.IfFalse(() => i++);
            Assert.Equal(2, i);
        }
    }
}