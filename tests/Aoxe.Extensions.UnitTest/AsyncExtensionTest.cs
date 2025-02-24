namespace Aoxe.Extensions.UnitTest;

public class AsyncExtensionTest
{
    [Fact]
    public void RunSyncTResult_ReturnsTaskResult()
    {
        var expected = 42;
        var task = Task.FromResult(expected);

        var result = task.RunSync();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void RunSync_ExecutesVoidTask()
    {
        bool executed = false;
        var task = Task.Run(() => executed = true);

        task.RunSync();

        Assert.True(executed);
    }

    [Fact]
    public void RunSyncTResult_PropagatesExceptions()
    {
        var task = Task.FromException<int>(new InvalidOperationException("Test error"));

        var ex = Assert.Throws<InvalidOperationException>(() => task.RunSync());
        Assert.Equal("Test error", ex.Message);
    }

    [Fact]
    public void RunSync_PropagatesExceptions()
    {
        var task = Task.FromException(new InvalidOperationException("Test error"));

        var ex = Assert.Throws<InvalidOperationException>(() => task.RunSync());
        Assert.Equal("Test error", ex.Message);
    }

    [Fact]
    public void RunSyncTResult_ThrowsOnNullTask()
    {
        Task<int>? nullTask = null;

        Assert.Throws<NullReferenceException>(() => nullTask.RunSync());
    }

    [Fact]
    public void RunSync_ThrowsOnNullTask()
    {
        Task? nullTask = null;

        Assert.Throws<NullReferenceException>(() => nullTask.RunSync());
    }

    [Fact]
    public void RunSync_BlocksUntilCompletion()
    {
        var completed = false;
        var task = Task.Run(async () =>
        {
            await Task.Delay(100);
            completed = true;
        });

        task.RunSync();

        Assert.True(completed);
    }

    [Fact]
    public void RunSync_HandlesCancellation()
    {
        using var cts = new CancellationTokenSource();
        var task = Task.Run(
            async () =>
            {
                await Task.Delay(1000, cts.Token);
            },
            cts.Token
        );

        cts.Cancel();

        Assert.Throws<TaskCanceledException>(() => task.RunSync());
    }
}
