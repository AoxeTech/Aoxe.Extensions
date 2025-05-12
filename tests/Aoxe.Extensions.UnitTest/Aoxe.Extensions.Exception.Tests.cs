namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsException
{
    [Fact]
    public void GetInmostException_NoInnerException_ReturnsOriginal()
    {
        // Arrange
        var exception = new Exception("Test exception");

        // Act
        var result = exception.GetInmostException();

        // Assert
        Assert.Same(exception, result);
    }

    [Fact]
    public void GetInmostException_SingleInnerException_ReturnsInner()
    {
        // Arrange
        var inner = new Exception("Inner exception");
        var exception = new Exception("Test exception", inner);

        // Act
        var result = exception.GetInmostException();

        // Assert
        Assert.Same(inner, result);
    }

    [Fact]
    public void GetInmostException_MultipleInnerExceptions_ReturnsDeepest()
    {
        // Arrange
        var innerMost = new Exception("Deepest exception");
        var intermediate = new Exception("Intermediate exception", innerMost);
        var exception = new Exception("Root exception", intermediate);

        // Act
        var result = exception.GetInmostException();

        // Assert
        Assert.Same(innerMost, result);
    }

    [Fact]
    public void GetInmostException_ComplexNesting_ReturnsCorrectException()
    {
        // Arrange
        var level4 = new Exception("Level 4");
        var level3 = new Exception("Level 3", level4);
        var level2 = new Exception("Level 2", level3);
        var level1 = new Exception("Level 1", level2);

        // Act
        var result = level1.GetInmostException();

        // Assert
        Assert.Same(level4, result);
    }
}
