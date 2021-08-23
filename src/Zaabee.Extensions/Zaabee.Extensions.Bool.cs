using System;

namespace Zaabee.Extensions
{
    public static partial class ZaabeeExtension
    {
        public static void IfTrueThenThrow<TException>(this bool b, TException exception)
            where TException : Exception
        {
            if (b) throw exception;
        }

        public static void IfFalseThenThrow<TException>(this bool b, TException exception)
            where TException : Exception
        {
            if (!b) throw exception;
        }

        public static void IfTrue(this bool b, Action action)
        {
            if (b) action();
        }

        public static void IfFalse(this bool b, Action action)
        {
            if (!b) action();
        }

        public static void IfTrueElse(this bool b, Action actionTrue, Action actionElse)
        {
            if (b) actionTrue();
            else actionElse();
        }

        public static void IfFalseElse(this bool b, Action actionFalse, Action actionElse)
        {
            if (!b) actionFalse();
            else actionElse();
        }
    }
}