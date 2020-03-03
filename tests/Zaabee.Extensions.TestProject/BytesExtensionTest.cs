using System.Text;
using Xunit;

namespace Zaabee.Extensions.TestProject
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
        public void GetStringByUtf7Test()
        {
            const string str = "Alice";
            var bytes = str.ToBytes(Encoding.UTF7);
            Assert.Equal(str, bytes.GetStringByUtf7());
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
    }
}