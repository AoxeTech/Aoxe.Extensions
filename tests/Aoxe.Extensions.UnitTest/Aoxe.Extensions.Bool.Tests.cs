namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionBoolTests
{
    // ThrowIfTrue tests
    [Fact]
    public void ThrowIfTrue_TrueCondition_ThrowsSpecifiedException()
    {
        bool condition = true;
        var ex = new InvalidOperationException();
        Assert.Throws<InvalidOperationException>(() => condition.ThrowIfTrue(ex));
    }

    [Fact]
    public void ThrowIfTrue_FalseCondition_DoesNotThrow()
    {
        bool condition = false;
        var ex = new InvalidOperationException();
        condition.ThrowIfTrue(ex); // Should not throw
    }

    [Fact]
    public void ThrowIfTrue_NullException_ThrowsNullReference()
    {
        bool condition = true;
        Assert.Throws<NullReferenceException>(
            () => condition.ThrowIfTrue<NullReferenceException>(null)
        );
    }

    // ThrowIfFalse tests
    [Fact]
    public void ThrowIfFalse_FalseCondition_ThrowsSpecifiedException()
    {
        bool condition = false;
        var ex = new ArgumentException();
        Assert.Throws<ArgumentException>(() => condition.ThrowIfFalse(ex));
    }

    [Fact]
    public void ThrowIfFalse_TrueCondition_DoesNotThrow()
    {
        bool condition = true;
        var ex = new ArgumentException();
        condition.ThrowIfFalse(ex); // Should not throw
    }

    [Fact]
    public void ThrowIfFalse_NullException_ThrowsNullReference()
    {
        bool condition = false;
        Assert.Throws<NullReferenceException>(
            () => condition.ThrowIfFalse<NullReferenceException>(null)
        );
    }

    // Then (Action) tests
    [Fact]
    public void Then_TrueCondition_ExecutesAction()
    {
        bool executed = false;
        true.Then(() => executed = true);
        Assert.True(executed);
    }

    [Fact]
    public void Then_FalseCondition_DoesNotExecute()
    {
        bool executed = false;
        false.Then(() => executed = true);
        Assert.False(executed);
    }

    [Fact]
    public void Then_NullAction_ThrowsNullReference()
    {
        Action nullAction = null;
        Assert.Throws<NullReferenceException>(() => true.Then(nullAction));
    }

    // Then<TResult> tests
    [Fact]
    public void ThenTResult_TrueCondition_ReturnsValue()
    {
        var result = true.Then(() => "success");
        Assert.Equal("success", result);
    }

    [Fact]
    public void ThenTResult_FalseCondition_ReturnsDefault()
    {
        var result = false.Then(() => "success");
        Assert.Null(result);
    }

    [Fact]
    public void ThenTResult_NullFunc_ThrowsNullReference()
    {
        Func<string> nullFunc = null;
        Assert.Throws<NullReferenceException>(() => true.Then(nullFunc));
    }

    // Otherwise (Action) tests
    [Fact]
    public void Otherwise_FalseCondition_ExecutesAction()
    {
        bool executed = false;
        false.Otherwise(() => executed = true);
        Assert.True(executed);
    }

    [Fact]
    public void Otherwise_TrueCondition_DoesNotExecute()
    {
        bool executed = false;
        true.Otherwise(() => executed = true);
        Assert.False(executed);
    }

    [Fact]
    public void Otherwise_NullAction_ThrowsNullReference()
    {
        Action nullAction = null;
        Assert.Throws<NullReferenceException>(() => false.Otherwise(nullAction));
    }

    // Otherwise<TResult> tests
    [Fact]
    public void OtherwiseTResult_FalseCondition_ReturnsValue()
    {
        var result = false.Otherwise(() => "fallback");
        Assert.Equal("fallback", result);
    }

    [Fact]
    public void OtherwiseTResult_TrueCondition_ReturnsDefault()
    {
        var result = true.Otherwise(() => "fallback");
        Assert.Null(result);
    }

    [Fact]
    public void OtherwiseTResult_NullFunc_ThrowsNullReference()
    {
        Func<string> nullFunc = null;
        Assert.Throws<NullReferenceException>(() => false.Otherwise(nullFunc));
    }

    // ThenOtherwise (Action) tests
    [Fact]
    public void ThenOtherwise_TrueCondition_ExecutesThenBranch()
    {
        bool thenExecuted = false;
        bool otherwiseExecuted = false;
        true.ThenOtherwise(() => thenExecuted = true, () => otherwiseExecuted = true);
        Assert.True(thenExecuted);
        Assert.False(otherwiseExecuted);
    }

    [Fact]
    public void ThenOtherwise_FalseCondition_ExecutesOtherwiseBranch()
    {
        bool thenExecuted = false;
        bool otherwiseExecuted = false;
        false.ThenOtherwise(() => thenExecuted = true, () => otherwiseExecuted = true);
        Assert.False(thenExecuted);
        Assert.True(otherwiseExecuted);
    }

    [Fact]
    public void ThenOtherwise_NullThenAction_ThrowsOnTrue()
    {
        Action nullAction = null;
        Assert.Throws<NullReferenceException>(() => true.ThenOtherwise(nullAction, () => { }));
    }

    [Fact]
    public void ThenOtherwise_NullOtherwiseAction_ThrowsOnFalse()
    {
        Action nullAction = null;
        Assert.Throws<NullReferenceException>(() => false.ThenOtherwise(() => { }, nullAction));
    }

    // ThenOtherwise<TResult> tests
    [Fact]
    public void ThenOtherwiseTResult_TrueCondition_ReturnsThenValue()
    {
        var result = true.ThenOtherwise(() => "then", () => "otherwise");
        Assert.Equal("then", result);
    }

    [Fact]
    public void ThenOtherwiseTResult_FalseCondition_ReturnsOtherwiseValue()
    {
        var result = false.ThenOtherwise(() => "then", () => "otherwise");
        Assert.Equal("otherwise", result);
    }

    [Fact]
    public void ThenOtherwiseTResult_NullThenFunc_ThrowsOnTrue()
    {
        Func<string> nullFunc = null;
        Assert.Throws<NullReferenceException>(
            () => true.ThenOtherwise(nullFunc, () => "otherwise")
        );
    }

    [Fact]
    public void ThenOtherwiseTResult_NullOtherwiseFunc_ThrowsOnFalse()
    {
        Func<string> nullFunc = null;
        Assert.Throws<NullReferenceException>(() => false.ThenOtherwise(() => "then", nullFunc));
    }

    // Edge case tests
    [Fact]
    public void ThenOtherwise_WithRealisticActions_ExecutesAppropriately()
    {
        int counter = 0;
        false.ThenOtherwise(thenAction: () => counter += 10, otherwiseAction: () => counter += 5);
        Assert.Equal(5, counter);
    }

    [Fact]
    public void ThenOtherwiseTResult_WithDifferentTypes_ReturnsCorrectType()
    {
        var result = true.ThenOtherwise<object>(thenFunc: () => 42, otherwiseFunc: () => "default");
        Assert.IsType<int>(result);
        Assert.Equal(42, result);
    }
}
