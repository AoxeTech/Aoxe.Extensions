using System;

namespace Zaabee.Extensions
{
    public static class BoolExtension
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
    }
}