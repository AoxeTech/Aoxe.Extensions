namespace Aoxe.Extensions.UnitTest;

public class BooleanExtensionTests
{
    [Fact]
    public void ThrowIfTrue_ThrowsExceptionWhenTrue()
    {
        var exception = new InvalidOperationException("Test error");

        // Should throw when true
        Assert.Throws<InvalidOperationException>(() => true.ThrowIfTrue(exception));

        // Should not throw when false
        var ex = Record.Exception(() => false.ThrowIfTrue(exception));
        Assert.Null(ex);
    }

    [Fact]
    public void ThrowIfFalse_ThrowsExceptionWhenFalse()
    {
        var exception = new ArgumentException("Test error");

        // Should throw when false
        Assert.Throws<ArgumentException>(() => false.ThrowIfFalse(exception));

        // Should not throw when true
        var ex = Record.Exception(() => true.ThrowIfFalse(exception));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void Then_ExecutesActionWhenTrue(bool condition, int expected)
    {
        int counter = 0;
        condition.Then(() => counter++);
        Assert.Equal(expected, counter);
    }

    [Theory]
    [InlineData(true, "Success")]
    [InlineData(false, null)]
    public void Then_ReturnsValueWhenTrue(bool condition, string? expected)
    {
        var result = condition.Then(() => "Success");
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(false, 1)]
    [InlineData(true, 0)]
    public void Otherwise_ExecutesActionWhenFalse(bool condition, int expected)
    {
        int counter = 0;
        condition.Otherwise(() => counter++);
        Assert.Equal(expected, counter);
    }

    [Theory]
    [InlineData(false, 42)]
    [InlineData(true, 0)]
    public void Otherwise_ReturnsValueWhenFalse(bool condition, int? expected)
    {
        var result = condition.Otherwise(() => 42);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "Then", "Otherwise")]
    [InlineData(false, "Then", "Otherwise")]
    public void ThenOrOtherwise_ExecutesCorrectBranch(
        bool condition,
        string expectedThen,
        string expectedOtherwise
    )
    {
        string result = "";
        condition.ThenOtherwise(() => result = expectedThen, () => result = expectedOtherwise);
        Assert.Equal(condition ? expectedThen : expectedOtherwise, result);
    }

    [Theory]
    [InlineData(true, 100, 200)]
    [InlineData(false, 200, 100)]
    public void ThenOrOtherwise_ReturnsCorrectValue(bool condition, int expected, int notExpected)
    {
        var result = condition.ThenOtherwise(() => 100, () => 200);
        Assert.Equal(expected, result);
        Assert.NotEqual(notExpected, result);
    }

    [Fact]
    public void NullActions_ThrowNullReferenceException()
    {
        // Then (Action)
        Assert.Throws<NullReferenceException>(() => true.Then(null!));

        // Then (Func)
        Assert.Throws<NullReferenceException>(() => true.Then<string>(null!));

        // Otherwise (Action)
        Assert.Throws<NullReferenceException>(() => false.Otherwise(null!));

        // ThenOrOtherwise
        Assert.Throws<NullReferenceException>(() => true.ThenOtherwise(null!, () => { }));
        Assert.Throws<NullReferenceException>(() => false.ThenOtherwise(() => { }, null!));
    }

    [Fact]
    public void ChainingMethods_WorksCorrectly()
    {
        int value = 0;

        true.ThenOtherwise(() => value += 10, () => value -= 5);

        Assert.Equal(10, value);

        false.ThenOtherwise(() => value *= 2, () => value += 3);

        Assert.Equal(13, value);
    }

    [Fact]
    public void MixedValueTypes_WorkAsExpected()
    {
        // Value type test
        var intResult = false.Otherwise(() => 42);
        Assert.Equal(42, intResult);

        // Reference type test
        var stringResult = true.Then(() => "Success");
        Assert.Equal("Success", stringResult);

        // Nullable handling
        var nullResult = false.Then<string?>(() => "Not null");
        Assert.Null(nullResult);
    }
}
