using System.Threading;
using System.Threading.Tasks;

namespace Zaabee.Extensions
{
    public static class AsyncExtension
    {
        private static readonly TaskFactory TaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(this Task<TResult> func)
        {
            Task<TResult> Func() => func;
            return TaskFactory.StartNew(Func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunSync(this Task func)
        {
            Task Func() => func;
            TaskFactory.StartNew(Func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}