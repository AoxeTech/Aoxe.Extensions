namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    private static readonly TaskFactory TaskFactory =
        new(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default
        );

    /// <summary>
    /// Synchronously executes a <see cref="Task{TResult}"/> and returns its result
    /// </summary>
    /// <typeparam name="TResult">The type of the task result</typeparam>
    /// <param name="func">The task to execute</param>
    /// <returns>The result of the task execution</returns>
    /// <remarks>
    /// This method uses a custom TaskFactory to execute the task synchronously.
    /// Use with caution as it may cause deadlocks in certain contexts.
    /// </remarks>
    public static TResult RunSync<TResult>(this Task<TResult> func)
    {
        if (func is null)
            throw new NullReferenceException(nameof(func));
        return TaskFactory.StartNew(Func).Unwrap().GetAwaiter().GetResult();
        Task<TResult> Func() => func;
    }

    /// <summary>
    /// Synchronously executes a Task
    /// </summary>
    /// <param name="func">The task to execute</param>
    /// <remarks>
    /// This method uses a custom TaskFactory to execute the task synchronously.
    /// Use with caution as it may cause deadlocks in certain contexts.
    /// </remarks>
    public static void RunSync(this Task func)
    {
        if (func is null)
            throw new NullReferenceException(nameof(func));
        TaskFactory.StartNew(Func).Unwrap().GetAwaiter().GetResult();
        return;
        Task Func() => func;
    }
}
