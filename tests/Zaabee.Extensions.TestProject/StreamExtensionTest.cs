using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Zaabee.Extensions.TestProject
{
    public class StreamExtensionTest
    {
        [Fact]
        public void TrySeekTest()
        {
            var ms = new MemoryStream();
            var bytes = new byte[1024];
            for (var i = 0; i < bytes.Length; i++) bytes[i] = (byte) (i % (byte.MaxValue + 1));
            Assert.True(ms.TryWrite(bytes, 0, 1024));
            Assert.Equal(1024, ms.Position);
            Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
            Assert.Equal(0, ms.Position);
        }

        [Fact]
        public void TryReadWriteTest()
        {
            var ms = new MemoryStream();
            var bytes = new byte[1024];
            var result = new byte[1024];
            for (var i = 0; i < bytes.Length; i++) bytes[i] = (byte) (i % (byte.MaxValue + 1));
            Assert.True(ms.TryWrite(bytes, 0, 1024));
            Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
            Assert.Equal(1024, ms.TryRead(result, 0, 1024));
            Assert.True(BytesEqual(bytes, result));
        }

        [Fact]
        public async Task TryReadWriteAsyncTest()
        {
            var ms = new MemoryStream();
            var bytes = new byte[1024];
            var result = new byte[1024];
            for (var i = 0; i < bytes.Length; i++) bytes[i] = (byte) (i % (byte.MaxValue + 1));
            Assert.True(await ms.TryWriteAsync(bytes, 0, 1024));
            Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
            Assert.Equal(1024, await ms.TryReadAsync(result, 0, 1024));
            Assert.True(BytesEqual(bytes, result));
        }

        [Fact]
        public void TryWriteReadByteTest()
        {
            var ms = new MemoryStream();
            var bytes = new byte[1024];
            var result = new byte[1024];
            for (var i = 0; i < bytes.Length; i++) bytes[i] = (byte) (i % (byte.MaxValue + 1));
            for (var i = 0; i < bytes.Length; i++) ms.TryWriteByte(bytes[i]);
            Assert.Equal(0, ms.TrySeek(0, SeekOrigin.Begin));
            for (var i = 0; i < result.Length; i++) result[i] = (byte) ms.TryReadByte();
            Assert.True(BytesEqual(bytes, result));
        }

        [Fact]
        public void TrySetReadTimeoutTest()
        {
            var stream = new FakeNetworkStream(new MemoryStream());
            Assert.True(stream.TrySetReadTimeout(1000));
            Assert.Equal(1000, stream.ReadTimeout);
            var timeSpan = TimeSpan.FromMinutes(1);
            Assert.True(stream.TrySetReadTimeout(timeSpan));
            Assert.Equal(timeSpan.Milliseconds, stream.ReadTimeout);
        }

        [Fact]
        public void TrySetWriteTimeoutTest()
        {
            var stream = new FakeNetworkStream(new MemoryStream());
            Assert.True(stream.TrySetWriteTimeout(1000));
            Assert.Equal(1000, stream.WriteTimeout);
            var timeSpan = TimeSpan.FromMinutes(1);
            Assert.True(stream.TrySetWriteTimeout(timeSpan));
            Assert.Equal(timeSpan.Milliseconds, stream.WriteTimeout);
        }

        private static bool BytesEqual(byte[] first, byte[] second)
        {
            if (first == null || second == null) return false;
            if (first.Length != second.Length) return false;
            return !first.Where((t, i) => t != second[i]).Any();
        }
    }

    internal class FakeNetworkStream : Stream
    {
        private Stream Inner { get; }
        private int Threshold { get; }

        public FakeNetworkStream(Stream inner, int threshold = 1)
        {
            Inner = inner;
            Threshold = threshold;
        }

        public override bool CanRead => Inner.CanRead;
        public override bool CanSeek => Inner.CanSeek;
        public override bool CanWrite => Inner.CanWrite;
        public override long Length => Inner.Length;

        public override long Position
        {
            get => Inner.Position;
            set => Inner.Position = value;
        }

        public override void Flush() => Inner.Flush();

        public override long Seek(long offset, SeekOrigin origin)
            => Inner.Seek(offset, origin);

        public override void SetLength(long value)
            => Inner.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count)
            => Inner.Write(buffer, offset, count);

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count > Threshold)
                count = Threshold;
            return Inner.Read(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            Inner?.Dispose();
        }

        public override bool CanTimeout => true;

        public override int WriteTimeout { get; set; }
        public override int ReadTimeout { get; set; }
    }
}