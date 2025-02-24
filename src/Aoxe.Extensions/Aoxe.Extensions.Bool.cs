namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>Throws exception if condition is true</summary>
    public static void ThrowIfTrue<TException>(this bool condition, TException exception)
        where TException : Exception
    {
        if (condition)
            throw exception;
    }

    /// <summary>Throws exception if condition is false</summary>
    public static void ThrowIfFalse<TException>(this bool condition, TException exception)
        where TException : Exception
    {
        if (!condition)
            throw exception;
    }

    /// <summary>Executes action if condition is true</summary>
    public static void Then(this bool condition, Action action)
    {
        if (condition)
            action();
    }

    /// <summary>Returns function result if condition is true</summary>
    public static TResult? Then<TResult>(this bool condition, Func<TResult?> func) =>
        condition ? func() : default;

    /// <summary>Executes action if condition is false</summary>
    public static void Otherwise(this bool condition, Action action)
    {
        if (!condition)
            action();
    }

    /// <summary>Returns function result if condition is false</summary>
    public static TResult? Otherwise<TResult>(this bool condition, Func<TResult?> func) =>
        !condition ? func() : default;

    /// <summary>Executes appropriate action based on condition</summary>
    public static void ThenOrOtherwise(
        this bool condition,
        Action thenAction,
        Action otherwiseAction
    )
    {
        if (condition)
            thenAction();
        else
            otherwiseAction();
    }

    /// <summary>Returns result from appropriate function based on condition</summary>
    public static TResult? ThenOrOtherwise<TResult>(
        this bool condition,
        Func<TResult?> thenFunc,
        Func<TResult?> otherwiseFunc
    ) => condition ? thenFunc() : otherwiseFunc();
}
