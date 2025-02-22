namespace Aoxe.Extensions.UnitTest
{
    public class AoxeExtensionBytesBase64Tests
    {
        [Fact]
        public void ToBase64String_ReturnsCorrectBase64String()
        {
            // Arrange
            byte[] bytes = [1, 2, 3, 4, 5];
            string expectedBase64 = Convert.ToBase64String(bytes);

            // Act
            string result = bytes.ToBase64String();

            // Assert
            Assert.Equal(expectedBase64, result);
        }

        [Fact]
        public void ToBase64Bytes_WithEncoding_ReturnsCorrectBytes()
        {
            // Arrange
            byte[] bytes = [1, 2, 3, 4, 5];
            string base64String = Convert.ToBase64String(bytes);
            Encoding encoding = Encoding.UTF8;
            byte[] expectedBytes = encoding.GetBytes(base64String);

            // Act
            byte[] result = bytes.ToBase64Bytes(encoding);

            // Assert
            Assert.Equal(expectedBytes, result);
        }

        [Fact]
        public void ToBase64Bytes_WithoutEncoding_ReturnsDefaultEncodingBytes()
        {
            // Arrange
            byte[] bytes = [1, 2, 3, 4, 5];
            string base64String = Convert.ToBase64String(bytes);
            byte[] expectedBytes = Encoding.Default.GetBytes(base64String);

            // Act
            byte[] result = bytes.ToBase64Bytes();

            // Assert
            Assert.Equal(expectedBytes, result);
        }

        [Fact]
        public void DecodeBase64ToBytes_WithEncoding_ReturnsOriginalBytes()
        {
            // Arrange
            byte[] originalBytes = [1, 2, 3, 4, 5];
            string base64String = Convert.ToBase64String(originalBytes);
            Encoding encoding = Encoding.UTF8;
            byte[] base64Bytes = encoding.GetBytes(base64String);

            // Act
            byte[] result = base64Bytes.DecodeBase64ToBytes(encoding);

            // Assert
            Assert.Equal(originalBytes, result);
        }

        [Fact]
        public void DecodeBase64ToBytes_WithoutEncoding_ReturnsOriginalBytes()
        {
            // Arrange
            byte[] originalBytes = [1, 2, 3, 4, 5];
            string base64String = Convert.ToBase64String(originalBytes);
            byte[] base64Bytes = Encoding.Default.GetBytes(base64String);

            // Act
            byte[] result = base64Bytes.DecodeBase64ToBytes();

            // Assert
            Assert.Equal(originalBytes, result);
        }

        [Fact]
        public void DecodeBase64ToString_WithEncoding_ReturnsOriginalString()
        {
            // Arrange
            string originalString = "Hello, World!";
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalString);
            string base64String = Convert.ToBase64String(originalBytes);
            byte[] base64Bytes = Encoding.UTF8.GetBytes(base64String);

            Encoding encoding = Encoding.UTF8;

            // Act
            string result = base64Bytes.DecodeBase64ToString(encoding);

            // Assert
            Assert.Equal(originalString, result);
        }

        [Fact]
        public void DecodeBase64ToString_WithoutEncoding_ReturnsOriginalString()
        {
            // Arrange
            string originalString = "Hello, World!";
            byte[] originalBytes = Encoding.Default.GetBytes(originalString);
            string base64String = Convert.ToBase64String(originalBytes);
            byte[] base64Bytes = Encoding.Default.GetBytes(base64String);

            // Act
            string result = base64Bytes.DecodeBase64ToString();

            // Assert
            Assert.Equal(originalString, result);
        }

        [Fact]
        public void ToBase64String_WithEmptyArray_ReturnsEmptyString()
        {
            // Arrange
            byte[] bytes = [];
            string expectedBase64 = string.Empty;

            // Act
            string result = bytes.ToBase64String();

            // Assert
            Assert.Equal(expectedBase64, result);
        }

        [Fact]
        public void DecodeBase64ToBytes_WithInvalidBase64_ThrowsFormatException()
        {
            // Arrange
            byte[] invalidBase64Bytes = Encoding.UTF8.GetBytes("InvalidBase64!");

            // Act & Assert
            Assert.Throws<FormatException>(() => invalidBase64Bytes.DecodeBase64ToBytes());
        }

        [Fact]
        public void DecodeBase64ToString_WithInvalidBase64_ThrowsFormatException()
        {
            // Arrange
            byte[] invalidBase64Bytes = Encoding.UTF8.GetBytes("InvalidBase64!");

            // Act & Assert
            Assert.Throws<FormatException>(() => invalidBase64Bytes.DecodeBase64ToString());
        }
    }
}
