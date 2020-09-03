using System;
using System.IO;
using System.Threading.Tasks;

namespace Zaabee.Extensions
{
    public static class StreamExtension
    {
        public static long TrySeek(this Stream stream, long offset, SeekOrigin seekOrigin) =>
            stream.CanSeek ? stream.Seek(offset, seekOrigin) : default;

        public static int TryRead(this Stream stream, byte[] buffer, int offset, int count) =>
            stream.CanRead ? stream.Read(buffer, offset, count) : default;

        public static Task<int> TryReadAsync(this Stream stream, byte[] buffer, int offset, int count) =>
            stream.CanRead ? stream.ReadAsync(buffer, offset, count) : default;

        public static int TryReadByte(this Stream stream) => stream.CanRead ? stream.ReadByte() : default;

        public static bool TryWrite(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream.CanWrite) stream.Write(buffer, offset, count);
            return stream.CanWrite;
        }

        public static Task<bool> TryWriteAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream.CanWrite) stream.WriteAsync(buffer, offset, count);
            return Task.FromResult(stream.CanWrite);
        }

        public static bool TryWriteByte(this Stream stream, byte value)
        {
            if (stream.CanWrite) stream.WriteByte(value);
            return stream.CanWrite;
        }

        public static bool TrySetReadTimeout(this Stream stream, int milliseconds)
        {
            if (stream.CanTimeout) stream.ReadTimeout = milliseconds;
            return stream.CanTimeout;
        }

        public static bool TrySetReadTimeout(this Stream stream, TimeSpan timeout) =>
            stream.TrySetReadTimeout(timeout.Milliseconds);

        public static bool TrySetWriteTimeout(this Stream stream, int milliseconds)
        {
            if (stream.CanTimeout) stream.WriteTimeout = milliseconds;
            return stream.CanTimeout;
        }

        public static bool TrySetWriteTimeout(this Stream stream, TimeSpan timeout) =>
            stream.TrySetWriteTimeout(timeout.Milliseconds);

        public static byte[] ReadToEnd(this Stream stream)
        {
            switch (stream)
            {
                case null: return new byte[0];
                case MemoryStream ms: return ms.ToArray();
            }

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static async Task<byte[]> ReadToEndAsync(this Stream stream)
        {
            switch (stream)
            {
                case null: return new byte[0];
                case MemoryStream ms: return ms.ToArray();
            }

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}