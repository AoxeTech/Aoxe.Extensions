namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static void ThrowIfTrue<TException>(this bool condition, TException exception)
        where TException : Exception
    {
        if (condition)
            throw exception;
    }

    public static void ThrowIfFalse<TException>(this bool condition, TException exception)
        where TException : Exception
    {
        if (!condition)
            throw exception;
    }

    public static void Then(this bool condition, Action action)
    {
        if (condition)
            action();
    }

    public static TResult? Then<TResult>(this bool condition, Func<TResult?> func) =>
        condition ? func() : default;

    public static void Otherwise(this bool condition, Action action)
    {
        if (!condition)
            action();
    }

    public static TResult? Otherwise<TResult>(this bool condition, Func<TResult?> func) =>
        !condition ? func() : default;

    public static void ThenOtherwise(this bool condition, Action thenAction, Action otherwiseAction)
    {
        if (condition)
            thenAction();
        else
            otherwiseAction();
    }

    public static TResult? ThenOtherwise<TResult>(
        this bool condition,
        Func<TResult?> thenFunc,
        Func<TResult?> otherwiseFunc
    ) => condition ? thenFunc() : otherwiseFunc();
}
