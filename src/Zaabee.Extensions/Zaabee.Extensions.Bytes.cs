using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zaabee.Extensions
{
    public static partial class ZaabeeExtension
    {
        public static string GetStringByUtf8(this byte[] bytes) =>
            bytes.GetString(Encoding.UTF8);

        public static string GetStringByAscii(this byte[] bytes) =>
            bytes.GetString(Encoding.ASCII);

        public static string GetStringByBigEndianUnicode(this byte[] bytes) =>
            bytes.GetString(Encoding.BigEndianUnicode);

        public static string GetStringByDefault(this byte[] bytes) =>
            bytes.GetString(Encoding.Default);

        public static string GetStringByUtf32(this byte[] bytes) =>
            bytes.GetString(Encoding.UTF32);

        public static string GetStringByUnicode(this byte[] bytes) =>
            bytes.GetString(Encoding.Unicode);

        public static string ToBase64String(this byte[] bytes) =>
            Convert.ToBase64String(bytes);

        public static byte[] ToBase64Bytes(this byte[] bytes, Encoding encoding = null) =>
            bytes.ToBase64String().ToBytes(encoding);

        public static byte[] DecodeBase64ToBytes(this byte[] bytes, Encoding encoding = null) =>
            Convert.FromBase64String(bytes.GetString(encoding));

        public static string DecodeBase64ToString(this byte[] bytes, Encoding encoding = null) =>
            Convert.FromBase64String(bytes.GetString(encoding)).GetString(encoding);

        public static string GetString(this byte[] bytes, Encoding encoding = null) =>
            bytes is null ? throw new ArgumentNullException(nameof(bytes)) :
            encoding is null ? Encoding.UTF8.GetString(bytes) :
            encoding.GetString(bytes);

        public static MemoryStream ToStream(this byte[] bytes)
        {
            var ms = new MemoryStream();
            bytes.WriteTo(ms);
            ms.TrySeek(0, SeekOrigin.Begin);
            return ms;
        }

        public static async Task<MemoryStream> ToStreamAsync(this byte[] bytes)
        {
            var ms = new MemoryStream();
            await bytes.WriteToAsync(ms);
            ms.TrySeek(0, SeekOrigin.Begin);
            return ms;
        }

        public static async Task<MemoryStream> ToStreamAsync(this byte[] bytes, CancellationToken cancellationToken)
        {
            var ms = new MemoryStream();
            await bytes.WriteToAsync(ms, cancellationToken);
            ms.TrySeek(0, SeekOrigin.Begin);
            return ms;
        }

        public static void WriteTo(this byte[] bytes, Stream stream) =>
            stream.Write(bytes, 0, bytes.Length);

        public static Task WriteToAsync(this byte[] bytes, Stream stream) =>
            stream.WriteAsync(bytes, 0, bytes.Length);

        public static Task WriteToAsync(this byte[] bytes, Stream stream, CancellationToken cancellationToken) =>
            stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
    }
}