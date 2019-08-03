using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zaabee.Extensions.Commons;

namespace Zaabee.Extensions
{
    public static class StringExtension
    {
        private static readonly Encoding Utf8Encoding = Encoding.UTF8;

        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        public static byte[] ToUtf8Bytes(this string value) => value.ToBytes(Encoding.UTF8);

        public static byte[] ToAsciiBytes(this string value) => value.ToBytes(Encoding.ASCII);

        public static byte[] ToBigEndianUnicodeBytes(this string value) => value.ToBytes(Encoding.BigEndianUnicode);

        public static byte[] ToDefaultBytes(this string value) => value.ToBytes(Encoding.Default);

        public static byte[] ToUtf32Bytes(this string value) => value.ToBytes(Encoding.UTF32);

        public static byte[] ToUtf7Bytes(this string value) => value.ToBytes(Encoding.UTF7);

        public static byte[] ToUnicodeBytes(this string value) => value.ToBytes(Encoding.Unicode);

        public static byte[] ToBytes(this string value, Encoding encoding = null) =>
            value is null ? throw new ArgumentNullException(nameof(value)) :
            encoding is null ? Utf8Encoding.GetBytes(value) : encoding.GetBytes(value);

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
            Convert.ToBase64String(encoding is null ? Utf8Encoding.GetBytes(str) : encoding.GetBytes(str));

        public static string FromBase64(this string str, Encoding encoding = null) =>
            encoding is null
                ? Utf8Encoding.GetString(Convert.FromBase64String(str))
                : encoding.GetString(Convert.FromBase64String(str));

        public static string GetLetterOrDigit(this string source) =>
            new string(source.Where(char.IsLetterOrDigit).ToArray());

        public static int ToInt(this string source, NumberSystem numberSystem)
        {
            if (source.IsNullOrWhiteSpace())
                return 0;
            if (source.Any(c => !char.IsLetterOrDigit(c)))
                throw new ArgumentException("String can only be letter or digit.");

            if (numberSystem <= NumberSystem.Hexatrigesimal)
                source = source.ToLower();

            var intScale = (int) numberSystem;
            return source.Select((t, i) =>
                (int) (Consts.Chars.IndexOf(t) * Math.Pow(intScale, source.Length - i - 1))).Sum();
        }

        public static long ToLong(this string source, NumberSystem numberSystem)
        {
            if (source.IsNullOrWhiteSpace())
                return 0;
            if (source.Any(c => !char.IsLetterOrDigit(c)))
                throw new ArgumentException("String can only be letter or digit.");

            if (numberSystem <= NumberSystem.Hexatrigesimal)
                source = source.ToLower();

            var intScale = (int) numberSystem;
            return source.Select((t, i) =>
                (long) (Consts.Chars.IndexOf(t) * Math.Pow(intScale, source.Length - i - 1))).Sum();
        }
    }
}