using System;
using System.IO;
using System.Threading.Tasks;

namespace Zaabee.Extensions
{
    public static class StreamExtension
    {
        public static long TrySeek(this Stream stream, long offset, SeekOrigin seekOrigin) =>
            stream.CanSeek ? stream.Seek(offset, seekOrigin) : 0L;

        public static int TryRead(this Stream stream, byte[] buffer, int offset, int count) =>
            stream.CanRead ? stream.Read(buffer, offset, count) : 0;

        public static async Task<int> TryReadAsync(this Stream stream, byte[] buffer, int offset, int count) =>
            stream.CanRead ? await stream.ReadAsync(buffer, offset, count) : 0;

        public static void TryWrite(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream.CanWrite) stream.Write(buffer, offset, count);
        }

        public static async Task TryWriteAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream.CanWrite) await stream.WriteAsync(buffer, offset, count);
        }

        public static void TrySetReadTimeout(this Stream stream, int timeout)
        {
            if (stream.CanTimeout) stream.ReadTimeout = timeout;
        }

        public static void TrySetReadTimeout(this Stream stream, TimeSpan timeout)
        {
            if (stream.CanTimeout) stream.ReadTimeout = timeout.Milliseconds;
        }

        public static void TrySetWriteTimeout(this Stream stream, TimeSpan timeout)
        {
            if (stream.CanTimeout) stream.WriteTimeout = timeout.Milliseconds;
        }

        public static int TryReadByte(this Stream stream) => stream.CanRead ? stream.ReadByte() : 0;

        public static void TryWriteByte(this Stream stream, byte value)
        {
            if (stream.CanWrite) stream.WriteByte(value);
        }
    }
}