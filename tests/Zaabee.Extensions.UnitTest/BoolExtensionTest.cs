namespace Zaabee.Extensions.UnitTest;

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
    public void IfTrueAction()
    {
        var i = 1;

        void Action()
        {
            i++;
        }

        true.IfTrue(Action);
        Assert.Equal(2, i);
    }

    [Fact]
    public void IfFalseAction()
    {
        var i = 1;

        void Action()
        {
            i++;
        }

        false.IfFalse(Action);
        Assert.Equal(2, i);
    }

    [Fact]
    public void IfTrueFunc()
    {
        Assert.Equal(1, true.IfTrue(() => 1));
    }

    [Fact]
    public void IfFalseFunc()
    {
        Assert.Equal(1, false.IfFalse(() => 1));
    }

    [Fact]
    public void IfTrueElseAction()
    {
        var a = 0;
        var b = "123";

        true.IfTrueElse(() => a++, () => b = "456");
        Assert.Equal(1, a);
        Assert.Equal("123", b);

        var c = 0;
        var d = "123";

        false.IfTrueElse(() => c++, () => d = "456");
        Assert.Equal(0, c);
        Assert.Equal("456", d);
    }

    [Fact]
    public void IfFalseElseAction()
    {
        var a = 0;
        var b = "123";

        true.IfFalseElse(() => a++, () => b = "456");
        Assert.Equal(0, a);
        Assert.Equal("456", b);

        var c = 0;
        var d = "123";

        false.IfFalseElse(() => c++, () => d = "456");
        Assert.Equal(1, c);
        Assert.Equal("123", d);
    }

    [Fact]
    public void IfTrueElseFunc()
    {
        const int i = 1;
        var s0 = true.IfTrueElse(() => i + 1, () => i - 1);
        Assert.Equal(2, s0);

        const int j = 1;
        var s1 = false.IfTrueElse(() => j + 1, () => j - 1);
        Assert.Equal(0, s1);
    }

    [Fact]
    public void IfFalseElseFunc()
    {
        const int i = 1;
        var s0 = false.IfFalseElse(() => i + 1, () => i - 1);
        Assert.Equal(2, s0);

        const int j = 1;
        var s1 = true.IfFalseElse(() => j + 1, () => j - 1);
        Assert.Equal(0, s1);
    }
}