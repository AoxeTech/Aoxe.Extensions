namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionBoolTests
{
    private bool _testFlag;

    // Test boolean extension for exception throwing
    [Fact]
    public void ThrowIfTrue_WhenTrue_ThrowsSpecifiedException()
    {
        Assert.Throws<InvalidOperationException>(
            () => true.ThrowIfTrue(new InvalidOperationException())
        );
    }

    [Fact]
    public void ThrowIfTrue_WhenFalse_DoesNotThrow()
    {
        var ex = Record.Exception(() => false.ThrowIfTrue(new Exception()));
        Assert.Null(ex);
    }

    [Fact]
    public void ThrowIfFalse_WhenFalse_ThrowsSpecifiedException()
    {
        Assert.Throws<ArgumentException>(() => false.ThrowIfFalse(new ArgumentException()));
    }

    [Fact]
    public void ThrowIfFalse_WhenTrue_DoesNotThrow()
    {
        var ex = Record.Exception(() => true.ThrowIfFalse(new Exception()));
        Assert.Null(ex);
    }

    // Test conditional action execution
    [Fact]
    public void Then_TrueCondition_ExecutesAction()
    {
        true.Then(() => _testFlag = true);
        Assert.True(_testFlag);
    }

    [Fact]
    public void Then_FalseCondition_SkipsAction()
    {
        false.Then(() => _testFlag = true);
        Assert.False(_testFlag);
    }

    [Fact]
    public void Otherwise_FalseCondition_ExecutesAction()
    {
        false.Otherwise(() => _testFlag = true);
        Assert.True(_testFlag);
    }

    [Fact]
    public void Otherwise_TrueCondition_SkipsAction()
    {
        true.Otherwise(() => _testFlag = true);
        Assert.False(_testFlag);
    }

    // Test conditional function execution
    [Fact]
    public void Then_WithFunc_ReturnsValueWhenTrue()
    {
        var result = true.Then(() => 42);
        Assert.Equal(42, result);
    }

    [Fact]
    public void Then_WithFunc_ReturnsDefaultWhenFalse()
    {
        var result = false.Then(() => 42);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Otherwise_WithFunc_ReturnsValueWhenFalse()
    {
        var result = false.Otherwise(() => "test");
        Assert.Equal("test", result);
    }

    [Fact]
    public void Otherwise_WithFunc_ReturnsDefaultWhenTrue()
    {
        var result = true.Otherwise(() => "test");
        Assert.Null(result);
    }

    // Test dual path execution
    [Fact]
    public void ThenOtherwise_ExecutesCorrectBranch()
    {
        true.ThenOtherwise(() => _testFlag = true, () => _testFlag = false);
        Assert.True(_testFlag);

        false.ThenOtherwise(() => _testFlag = true, () => _testFlag = false);
        Assert.False(_testFlag);
    }

    [Fact]
    public void ThenOtherwise_WithFunc_ReturnsCorrectValue()
    {
        var trueResult = true.ThenOtherwise(() => "yes", () => "no");
        var falseResult = false.ThenOtherwise(() => "yes", () => "no");

        Assert.Equal("yes", trueResult);
        Assert.Equal("no", falseResult);
    }

    // Test edge cases and null handling
    [Fact]
    public void Then_NullAction_ThrowsOnExecution()
    {
        Assert.Throws<NullReferenceException>(() => true.Then(null));
    }

    [Fact]
    public void Otherwise_NullAction_ThrowsOnExecution()
    {
        Assert.Throws<NullReferenceException>(() => false.Otherwise(null));
    }

    [Fact]
    public void ThrowIfTrue_NullException_ThrowsNullReference()
    {
        Assert.Throws<NullReferenceException>(() => true.ThrowIfTrue<Exception>(null));
    }

    // Parameterized tests for boundary values
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ThenOtherwise_CoversBothBranches(bool condition)
    {
        var result = condition.ThenOtherwise(() => condition ? 1 : 0, () => condition ? 1 : 0);

        Assert.Equal(condition ? 1 : 0, result);
    }
}
