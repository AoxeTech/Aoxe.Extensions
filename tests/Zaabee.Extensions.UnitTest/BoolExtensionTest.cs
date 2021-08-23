using System;
using Xunit;

namespace Zaabee.Extensions.UnitTest
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

        [Fact]
        public void IfTrueElse()
        {
            var i = 1;
            true.IfTrueElse(() => i++, () => i--);
            Assert.Equal(2, i);

            var j = 1;
            false.IfTrueElse(() => j++, () => j--);
            Assert.Equal(0, j);
        }

        [Fact]
        public void IfFalseElse()
        {
            var i = 1;
            false.IfFalseElse(() => i++, () => i--);
            Assert.Equal(2, i);

            var j = 1;
            true.IfFalseElse(() => j++, () => j--);
            Assert.Equal(0, j);
        }
    }
}