using System.IO;

namespace Zaabee.Extensions.TestProject.Commons
{

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