using System;

namespace Zaabee.Extensions
{
    public static class BoolExtension
    {
        public static void IfThrow<TException>(this bool b, TException exception) where TException : Exception
        {
            if (b) throw exception;
        }
    }
}