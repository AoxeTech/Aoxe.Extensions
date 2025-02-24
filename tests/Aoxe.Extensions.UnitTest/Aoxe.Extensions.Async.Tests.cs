namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionAsyncTests
{
    [Fact]
    public void RunSync_TaskResult_ReturnsExpectedResult()
    {
        // Arrange
        var asyncTask = GetValueAsync(42);

        // Act
        var result = asyncTask.RunSync();

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void RunSync_Task_CompletesSuccessfully()
    {
        // Arrange
        var isCompleted = false;
        var asyncTask = SetFlagAsync(() => isCompleted = true);

        // Act
        asyncTask.RunSync();

        // Assert
        Assert.True(isCompleted);
    }

    private static async Task<int> GetValueAsync(int value)
    {
        await Task.Delay(100);
        return value;
    }

    private static async Task SetFlagAsync(Action action)
    {
        await Task.Delay(100);
        action();
    }
}
