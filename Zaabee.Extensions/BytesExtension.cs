using System;
using System.Text;

namespace Zaabee.Extensions
{
    public static class BytesExtension
    {
        public static string Utf8ToString(this byte[] bytes) => GetString(bytes, Encoding.UTF8);

        public static string AsciiToString(this byte[] bytes) => GetString(bytes, Encoding.ASCII);

        public static string BigEndianUnicodeToString(this byte[] bytes) => GetString(bytes, Encoding.BigEndianUnicode);

        public static string DefaultToString(this byte[] bytes) => GetString(bytes, Encoding.Default);

        public static string Utf32ToString(this byte[] bytes) => GetString(bytes, Encoding.UTF32);

        public static string Utf7ToString(this byte[] bytes) => GetString(bytes, Encoding.UTF7);

        public static string UnicodeToString(this byte[] bytes) => GetString(bytes, Encoding.Unicode);

        public static string GetString(this byte[] bytes, Encoding encoding = null) =>
            bytes is null ? throw new ArgumentNullException(nameof(bytes)) :
            encoding is null ? Encoding.UTF8.GetString(bytes) : encoding.GetString(bytes);
    }
}