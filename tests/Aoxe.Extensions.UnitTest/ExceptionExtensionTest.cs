namespace Aoxe.Extensions.UnitTest;

public class ExceptionExtensionsTests
{
    [Fact]
    public void GetInmostException_SingleException_ReturnsSelf()
    {
        // Arrange
        var ex = new Exception("Test exception");

        // Act
        var result = ex.GetInmostException();

        // Assert
        Assert.Same(ex, result);
    }

    [Fact]
    public void GetInmostException_ExceptionWithOneInner_ReturnsInner()
    {
        // Arrange
        var inner = new Exception("Inner exception");
        var outer = new Exception("Outer exception", inner);

        // Act
        var result = outer.GetInmostException();

        // Assert
        Assert.Same(inner, result);
    }

    [Fact]
    public void GetInmostException_ExceptionWithTwoInners_ReturnsLastInner()
    {
        // Arrange
        var innerMost = new Exception("Innermost exception");
        var middle = new Exception("Middle exception", innerMost);
        var outer = new Exception("Outer exception", middle);

        // Act
        var result = outer.GetInmostException();

        // Assert
        Assert.Same(innerMost, result);
    }

    [Fact]
    public void GetInmostException_DeepHierarchy_ReturnsDeepestException()
    {
        // Arrange
        var depth = 5;
        Exception current = new Exception("Level 0");
        for (int i = 1; i <= depth; i++)
        {
            current = new Exception($"Level {i}", current);
        }

        // Act
        var result = current.GetInmostException();

        // Assert
        Assert.Equal("Level 0", result.Message);
    }

    [Fact]
    public void GetInmostException_ComplexHierarchy_ReturnsCorrectInner()
    {
        // Arrange
        var actualInner = new InvalidOperationException("Real inner");
        var wrapper1 = new Exception("Wrapper 1", actualInner);
        var wrapper2 = new Exception("Wrapper 2", wrapper1);
        var outer = new ArgumentException("Outer", wrapper2);

        // Act
        var result = outer.GetInmostException();

        // Assert
        Assert.Same(actualInner, result);
        Assert.IsType<InvalidOperationException>(result);
    }

    [Fact]
    public void GetInmostException_AggregateException_IgnoresAggregateAndReturnsFirstInner()
    {
        // Arrange
        var realInner = new TimeoutException("Timeout");
        var aggregate = new AggregateException(
            new Exception("First"),
            new Exception("Second", realInner)
        );
        var outer = new Exception("Outer", aggregate);

        // Act
        var result = outer.GetInmostException();

        // Assert
        // AggregateException is treated as normal exception
        // The chain is outer → aggregate → Second → realInner
        Assert.Same(realInner, result);
    }
}
