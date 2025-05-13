namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamReadTests
{
    #region TryRead Tests
    [Fact(DisplayName = "TryRead with null stream returns -1")]
    public void TryRead_NullStream_ReturnsMinusOne()
    {
        // Arrange
        Stream nullStream = null!;
        byte[] buffer = new byte[10];

        // Act
        var result = nullStream.TryRead(buffer);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact(DisplayName = "TryRead with non-readable stream returns -1")]
    public void TryRead_NonReadableStream_ReturnsMinusOne()
    {
        // Arrange
        using var nonReadableStream = new NonReadableMemoryStream(new byte[5]);
        byte[] buffer = new byte[5];

        // Act
        var result = nonReadableStream.TryRead(buffer);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact(DisplayName = "TryRead reads entire buffer from readable stream")]
    public void TryRead_ReadableStream_ReadsEntireBuffer()
    {
        // Arrange
        var testData = new byte[] { 1, 2, 3, 4 };
        using var stream = new MemoryStream(testData);
        byte[] buffer = new byte[4];

        // Act
        var bytesRead = stream.TryRead(buffer);

        // Assert
        Assert.Equal(4, bytesRead);
        Assert.Equal(testData, buffer);
    }

    [Fact(DisplayName = "TryRead with offset reads to correct buffer position")]
    public void TryRead_WithOffset_ReadsToCorrectPosition()
    {
        // Arrange
        var testData = new byte[] { 1, 2, 3, 4 };
        using var stream = new MemoryStream(testData);
        byte[] buffer = new byte[5];
        int offset = 1;
        int count = 3;

        // Act
        var bytesRead = stream.TryRead(buffer, offset, count);

        // Assert
        Assert.Equal(3, bytesRead);
        Assert.Equal(new byte[] { 0, 1, 2, 3, 0 }, buffer);
    }
    #endregion

    #region TryReadByte Tests
    [Fact(DisplayName = "TryReadByte with null stream returns -1")]
    public void TryReadByte_NullStream_ReturnsMinusOne()
    {
        // Arrange
        Stream nullStream = null!;

        // Act
        var result = nullStream.TryReadByte();

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact(DisplayName = "TryReadByte reads byte from current position")]
    public void TryReadByte_ReadableStream_ReturnsCurrentByte()
    {
        // Arrange
        var testData = new byte[] { 5, 10, 15 };
        using var stream = new MemoryStream(testData);

        // Act & Assert
        Assert.Equal(5, stream.TryReadByte());
        Assert.Equal(10, stream.TryReadByte());
        Assert.Equal(15, stream.TryReadByte());
        Assert.Equal(-1, stream.TryReadByte());
    }
    #endregion

    #region ReadToEnd Tests
    [Fact(DisplayName = "ReadToEnd with null stream returns empty array")]
    public void ReadToEnd_NullStream_ReturnsEmptyArray()
    {
        // Arrange
        Stream nullStream = null!;

        // Act
        var result = nullStream.ReadToEnd();

        // Assert
        Assert.Empty(result);
    }

    [Fact(DisplayName = "ReadToEnd with MemoryStream returns full content")]
    public void ReadToEnd_MemoryStream_ReturnsAllBytes()
    {
        // Arrange
        var testData = new byte[] { 1, 2, 3, 4 };
        using var stream = new MemoryStream(testData);
        stream.Position = 2; // Shouldn't affect MemoryStream.ToArray()

        // Act
        var result = stream.ReadToEnd();

        // Assert
        Assert.Equal(testData, result);
    }

    [Fact(DisplayName = "ReadToEnd with FileStream reads from current position")]
    public void ReadToEnd_FileStream_ReadsFromCurrentPosition()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(tempFile, new byte[] { 1, 2, 3, 4 });
            using var fileStream = File.OpenRead(tempFile);
            fileStream.Position = 2;

            // Act
            var result = fileStream.ReadToEnd();

            // Assert
            Assert.Equal(new byte[] { 3, 4 }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
    #endregion

    #region ReadString Tests
    [Fact(DisplayName = "ReadString with null stream returns empty string")]
    public void ReadString_NullStream_ReturnsEmptyString()
    {
        // Arrange
        Stream nullStream = null!;

        // Act
        var result = nullStream.ReadString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact(DisplayName = "ReadString uses specified encoding correctly")]
    public void ReadString_WithCustomEncoding_ReturnsCorrectString()
    {
        // Arrange
        var text = "Hello 你好";
        var encoding = Encoding.Unicode;
        var bytes = encoding.GetBytes(text);
        using var stream = new MemoryStream(bytes);

        // Act
        var result = stream.ReadString(encoding);

        // Assert
        Assert.Equal(text, result);
    }

    [Fact(DisplayName = "ReadString uses UTF-8 as default encoding")]
    public void ReadString_DefaultEncoding_UsesUtf8()
    {
        // Arrange
        var text = "Test ✓";
        var utf8Bytes = Encoding.UTF8.GetBytes(text);
        using var stream = new MemoryStream(utf8Bytes);

        // Act
        var result = stream.ReadString();

        // Assert
        Assert.Equal(text, result);
    }
    #endregion

    #region Helpers
    private class NonReadableMemoryStream : MemoryStream
    {
        public NonReadableMemoryStream(byte[] buffer)
            : base(buffer) { }

        public override bool CanRead => false;
    }
    #endregion
}
