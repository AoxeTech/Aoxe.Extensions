using System;

namespace Zaabee.Extensions
{
    public static class BoolExtension
    {
        public static void IfTrueThenThrow(this bool b, Exception exception)
        {
            if (b) throw exception;
        }
        public static void IfFalseThenThrow(this bool b, Exception exception)
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