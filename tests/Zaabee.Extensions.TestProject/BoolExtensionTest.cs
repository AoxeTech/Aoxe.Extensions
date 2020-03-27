using System;
using Xunit;

namespace Zaabee.Extensions.TestProject
{
    public class BoolExtensionTest
    {
        [Fact]
        public void IfThrowTest()
        {
            true.IfFalseThenThrow(new ArgumentException());
            false.IfTrueThenThrow(new ArgumentException());
            Assert.Throws<ArgumentException>(() => true.IfTrueThenThrow(new ArgumentException()));
            Assert.Throws<ArgumentException>(() => false.IfFalseThenThrow(new ArgumentException()));
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