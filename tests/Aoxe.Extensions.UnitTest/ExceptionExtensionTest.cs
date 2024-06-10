namespace Aoxe.Extensions.UnitTest;

public class ExceptionExtensionTest
{
    [Fact]
    public void Test()
    {
        var inmostEx = new Exception("This is the inmost exception.");
        var ex0 = new Exception("", inmostEx);
        var ex1 = new Exception("", ex0);
        Assert.Equal(inmostEx, ex1.GetInmostException());
        Assert.Equal(inmostEx, inmostEx.GetInmostException());
    }
}
