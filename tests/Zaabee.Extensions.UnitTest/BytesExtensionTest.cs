using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Zaabee.Extensions.UnitTest
{
    public class BytesExtensionTest
    {
        [Fact]
        public void GetStringByUtf8Test()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            Assert.Equal(str, bytes.GetStringByUtf8());
        }

        [Fact]
        public void GetStringByAsciiTest()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.ASCII);
            Assert.Equal(str, bytes.GetStringByAscii());
        }

        [Fact]
        public void GetStringByBigEndianUnicodeTest()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.BigEndianUnicode);
            Assert.Equal(str, bytes.GetStringByBigEndianUnicode());
        }

        [Fact]
        public void GetStringByDefaultTest()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.Default);
            Assert.Equal(str, bytes.GetStringByDefault());
        }

        [Fact]
        public void GetStringByUtf32Test()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF32);
            Assert.Equal(str, bytes.GetStringByUtf32());
        }

        [Fact]
        public void GetStringByUnicodeTest()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.Unicode);
            Assert.Equal(str, bytes.GetStringByUnicode());
        }

        [Fact]
        public void Base64Test()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);

            var bytesToBase64String = bytes.ToBase64String();
            var stringToBase64String = str.ToBase64String();
            Assert.Equal(bytesToBase64String, stringToBase64String);

            var bytesToBase64Bytes = bytes.ToBase64Bytes();
            var stringToBase64Bytes = str.ToBase64Bytes();
            Assert.Equal(bytesToBase64Bytes.Length, stringToBase64Bytes.Length);
            Assert.Equal(bytesToBase64Bytes.GetStringByUtf8(), stringToBase64Bytes.GetStringByUtf8());

            var bytesDecodeByBytes = bytesToBase64Bytes.DecodeBase64ToBytes();
            var stringDecodeByBytes = bytesToBase64Bytes.DecodeBase64ToString();
            Assert.Equal(bytesDecodeByBytes.GetStringByUtf8(), stringDecodeByBytes);
        }

        [Fact]
        public void ToStreamTest()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            var ms = bytes.ToStream();
            var result = ms.ReadToEnd();
            Assert.True(Equal(bytes, result));
        }

        [Fact]
        public async Task ToStreamTestAsync()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            var ms = await bytes.ToStreamAsync();
            var result = await ms.ReadToEndAsync();
            Assert.True(Equal(bytes, result));
        }

        [Fact]
        public async Task ToStreamTestWithCancellationTokenAsync()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            var ms = await bytes.ToStreamAsync(CancellationToken.None);
            var result = await ms.ReadToEndAsync(bytes.Length, CancellationToken.None);
            Assert.True(Equal(bytes, result));
        }

        [Fact]
        public void WriteToTest()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            var ms = new MemoryStream();
            bytes.WriteTo(ms);
            var result = ms.ReadToEnd();
            Assert.True(Equal(bytes, result));
        }

        [Fact]
        public async Task WriteToTestAsync()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            var ms = new MemoryStream();
            await bytes.WriteToAsync(ms);
            var result = await ms.ReadToEndAsync();
            Assert.True(Equal(bytes, result));
        }

        [Fact]
        public async Task WriteToTestWithCancellationTokenAsync()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF8);
            var ms = new MemoryStream();
            await bytes.WriteToAsync(ms, CancellationToken.None);
            var result = await ms.ReadToEndAsync(bytes.Length, CancellationToken.None);
            Assert.True(Equal(bytes, result));
        }

        private static bool Equal(byte[] first, byte[] second)
        {
            if (first is null || second is null) return false;
            if (first.Length != second.Length) return false;
            return !first.Where((t, i) => t != second[i]).Any();
        }
    }
}