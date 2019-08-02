using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Zaabee.Extensions
{
    public static class StringExtension
    {
        private static readonly Encoding Utf8Encoding = Encoding.UTF8;

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static byte[] ToUtf8Bytes(this string str) => str.ToBytes(Encoding.UTF8);

        public static byte[] ToAsciiBytes(this string str) => str.ToBytes(Encoding.ASCII);

        public static byte[] ToBigEndianUnicodeBytes(this string str) => str.ToBytes(Encoding.BigEndianUnicode);

        public static byte[] ToDefaultBytes(this string str) => str.ToBytes(Encoding.Default);

        public static byte[] ToUtf32Bytes(this string str) => str.ToBytes(Encoding.UTF32);

        public static byte[] ToUtf7Bytes(this string str) => str.ToBytes(Encoding.UTF7);

        public static byte[] ToUnicodeBytes(this string str) => str.ToBytes(Encoding.Unicode);

        public static byte[] ToBytes(this string str, Encoding encoding = null) =>
            encoding == null ? Utf8Encoding.GetBytes(str) : encoding.GetBytes(str);

        public static string StringJoin<T>(this IEnumerable<T> strings, string separator) =>
            string.Join(separator, strings);

        public static short TryParseShort(this string str) =>
            short.TryParse(str, out var result) ? result : default;

        public static int TryParseInt(this string str) =>
            int.TryParse(str, out var result) ? result : default;

        public static long TryParseLong(this string str) =>
            long.TryParse(str, out var result) ? result : default;

        public static float TryParseFloat(this string str) =>
            float.TryParse(str, out var result) ? result : default;

        public static double TryParseDouble(this string str) =>
            double.TryParse(str, out var result) ? result : default;

        public static decimal TryParseDecimal(this string str) =>
            decimal.TryParse(str, out var result) ? result : default;

        public static TEnum TryParseEnum<TEnum>(this string str) where TEnum : struct, Enum =>
            Enum.TryParse(str, out TEnum result) ? result : default;

        public static string ToBase64(this string str, Encoding encoding = null) =>
            Convert.ToBase64String(encoding == null ? Utf8Encoding.GetBytes(str) : encoding.GetBytes(str));

        public static string FromBase64(this string str, Encoding encoding = null) =>
            encoding == null
                ? Utf8Encoding.GetString(Convert.FromBase64String(str))
                : encoding.GetString(Convert.FromBase64String(str));
    }
}