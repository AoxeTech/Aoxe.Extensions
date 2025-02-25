namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionBoolTests
{
    [Fact]
    public void IfTrueThenThrow_WhenTrue_ThrowsException()
    {
        // Arrange
        bool condition = true;
        var exception = new InvalidOperationException();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => condition.ThrowIfTrue(exception));
    }

    [Fact]
    public void IfTrueThenThrow_WhenFalse_DoesNotThrow()
    {
        // Arrange
        bool condition = false;
        var exception = new InvalidOperationException();

        // Act & Assert
        var exceptionRecord = Record.Exception(() => condition.ThrowIfTrue(exception));
        Assert.Null(exceptionRecord);
    }

    [Fact]
    public void IfFalseThenThrow_WhenFalse_ThrowsException()
    {
        // Arrange
        bool condition = false;
        var exception = new ArgumentException();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => condition.ThrowIfFalse(exception));
    }

    [Fact]
    public void IfFalseThenThrow_WhenTrue_DoesNotThrow()
    {
        // Arrange
        bool condition = true;
        var exception = new ArgumentException();

        // Act & Assert
        var exceptionRecord = Record.Exception(() => condition.ThrowIfFalse(exception));
        Assert.Null(exceptionRecord);
    }

    [Fact]
    public void IfTrue_WhenTrue_ExecutesAction()
    {
        // Arrange
        bool condition = true;
        bool actionExecuted = false;
        Action action = () => actionExecuted = true;

        // Act
        condition.Then(action);

        // Assert
        Assert.True(actionExecuted);
    }

    [Fact]
    public void IfTrue_WhenFalse_DoesNotExecuteAction()
    {
        // Arrange
        bool condition = false;
        bool actionExecuted = false;
        Action action = () => actionExecuted = true;

        // Act
        condition.Then(action);

        // Assert
        Assert.False(actionExecuted);
    }

    [Fact]
    public void IfFalse_WhenFalse_ExecutesAction()
    {
        // Arrange
        bool condition = false;
        bool actionExecuted = false;
        Action action = () => actionExecuted = true;

        // Act
        condition.Otherwise(action);

        // Assert
        Assert.True(actionExecuted);
    }

    [Fact]
    public void IfFalse_WhenTrue_DoesNotExecuteAction()
    {
        // Arrange
        bool condition = true;
        bool actionExecuted = false;
        Action action = () => actionExecuted = true;

        // Act
        condition.Otherwise(action);

        // Assert
        Assert.False(actionExecuted);
    }

    [Fact]
    public void IfTrue_WithFunc_WhenTrue_ReturnsExpectedResult()
    {
        // Arrange
        bool condition = true;
        Func<int?> func = () => 42;

        // Act
        int? result = condition.Then(func);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void IfTrue_WithFunc_WhenFalse_ReturnsDefault()
    {
        // Arrange
        bool condition = false;
        Func<int?> func = () => 42;

        // Act
        int? result = condition.Then(func);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IfFalse_WithFunc_WhenFalse_ReturnsExpectedResult()
    {
        // Arrange
        bool condition = false;
        Func<string?> func = () => "False";

        // Act
        string? result = condition.Otherwise(func);

        // Assert
        Assert.Equal("False", result);
    }

    [Fact]
    public void IfFalse_WithFunc_WhenTrue_ReturnsDefault()
    {
        // Arrange
        bool condition = true;
        Func<string?> func = () => "False";

        // Act
        string? result = condition.Otherwise(func);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IfTrueElse_WhenTrue_ExecutesTrueAction()
    {
        // Arrange
        bool condition = true;
        bool trueExecuted = false;
        bool elseExecuted = false;
        Action trueAction = () => trueExecuted = true;
        Action elseAction = () => elseExecuted = true;

        // Act
        condition.ThenOtherwise(trueAction, elseAction);

        // Assert
        Assert.True(trueExecuted);
        Assert.False(elseExecuted);
    }

    [Fact]
    public void IfTrueElse_WhenFalse_ExecutesElseAction()
    {
        // Arrange
        bool condition = false;
        bool trueExecuted = false;
        bool elseExecuted = false;
        Action trueAction = () => trueExecuted = true;
        Action elseAction = () => elseExecuted = true;

        // Act
        condition.ThenOtherwise(trueAction, elseAction);

        // Assert
        Assert.False(trueExecuted);
        Assert.True(elseExecuted);
    }

    [Fact]
    public void IfTrueElse_WithFunc_WhenTrue_ReturnsTrueResult()
    {
        // Arrange
        bool condition = true;
        Func<string?> trueFunc = () => "True";
        Func<string?> elseFunc = () => "Else";

        // Act
        string? result = condition.ThenOtherwise(trueFunc, elseFunc);

        // Assert
        Assert.Equal("True", result);
    }

    [Fact]
    public void IfTrueElse_WithFunc_WhenFalse_ReturnsElseResult()
    {
        // Arrange
        bool condition = false;
        Func<string?> trueFunc = () => "True";
        Func<string?> elseFunc = () => "Else";

        // Act
        string? result = condition.ThenOtherwise(trueFunc, elseFunc);

        // Assert
        Assert.Equal("Else", result);
    }
}
