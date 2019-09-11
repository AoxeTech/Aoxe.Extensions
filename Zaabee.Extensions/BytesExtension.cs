using System;
using System.Text;

namespace Zaabee.Extensions
{
    public static class BytesExtension
    {
        public static string Utf8ToString(this byte[] bytes) => bytes.GetString(Encoding.UTF8);

        public static string AsciiToString(this byte[] bytes) => bytes.GetString(Encoding.ASCII);

        public static string BigEndianUnicodeToString(this byte[] bytes) => bytes.GetString(Encoding.BigEndianUnicode);

        public static string DefaultToString(this byte[] bytes) => bytes.GetString(Encoding.Default);

        public static string Utf32ToString(this byte[] bytes) => bytes.GetString(Encoding.UTF32);

        public static string Utf7ToString(this byte[] bytes) => bytes.GetString(Encoding.UTF7);

        public static string UnicodeToString(this byte[] bytes) => bytes.GetString(Encoding.Unicode);

        public static byte[] ToBase64(this byte[] bytes) => bytes.ToBase64Bytes();

        public static byte[] FromBase64(this byte[] bytes) => bytes.DecodeBase64ToBytes();

        public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);

        public static byte[] ToBase64Bytes(this byte[] bytes, Encoding encoding = null) =>
            Convert.ToBase64String(bytes).ToBytes(encoding);

        public static byte[] DecodeBase64ToBytes(this byte[] bytes, Encoding encoding = null) =>
            Convert.FromBase64String(bytes.GetString(encoding));

        public static string DecodeBase64ToString(this byte[] bytes, Encoding encoding = null) =>
            Convert.FromBase64String(bytes.GetString(encoding)).GetString(encoding);

        public static string GetString(this byte[] bytes, Encoding encoding = null) =>
            bytes is null ? throw new ArgumentNullException(nameof(bytes)) :
            encoding is null ? Encoding.UTF8.GetString(bytes) : encoding.GetString(bytes);
    }
}