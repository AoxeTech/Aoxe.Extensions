namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamWriteTest
{
    #region TryWrite(byte[]) Tests
    [Fact(DisplayName = "TryWrite with null stream returns false")]
    public void TryWrite_NullStream_ReturnsFalse()
    {
        // Arrange
        Stream nullStream = null!;
        var buffer = new byte[10];

        // Act
        var result = nullStream.TryWrite(buffer);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "TryWrite with non-writable stream returns false")]
    public void TryWrite_NonWritableStream_ReturnsFalse()
    {
        // Arrange
        using var stream = new NonWritableStream();
        var buffer = new byte[5];

        // Act
        var result = stream.TryWrite(buffer);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "TryWrite handles write exceptions gracefully")]
    public void TryWrite_WriteFailure_ReturnsFalse()
    {
        // Arrange
        using var stream = new ExceptionThrowingStream();
        var buffer = new byte[10];

        // Act
        var result = stream.TryWrite(buffer);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "TryWrite writes full buffer successfully")]
    public void TryWrite_ValidStream_WritesAllBytes()
    {
        // Arrange
        using var stream = new MemoryStream();
        var buffer = new byte[] { 1, 2, 3, 4 };

        // Act
        var result = stream.TryWrite(buffer);

        // Assert
        Assert.True(result);
        Assert.Equal(buffer, stream.ToArray());
    }
    #endregion

    #region TryWrite(byte[], offset, count) Tests
    [Fact(DisplayName = "TryWrite validates offset and count parameters")]
    public void TryWrite_InvalidOffsetCount_ThrowsArgumentException()
    {
        // Arrange
        using var stream = new MemoryStream();
        var buffer = new byte[5];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => stream.TryWrite(buffer, -1, 2));
        Assert.Throws<ArgumentException>(() => stream.TryWrite(buffer, 3, 3));
    }

    [Fact(DisplayName = "TryWrite writes correct buffer segment")]
    public void TryWrite_WritesCorrectSubsection()
    {
        // Arrange
        using var stream = new MemoryStream();
        var buffer = new byte[] { 1, 2, 3, 4, 5 };
        int offset = 1,
            count = 3;

        // Act
        var result = stream.TryWrite(buffer, offset, count);

        // Assert
        Assert.True(result);
        Assert.Equal(new byte[] { 2, 3, 4 }, stream.ToArray());
    }
    #endregion

    #region TryWriteByte Tests
    [Fact(DisplayName = "TryWriteByte writes single byte successfully")]
    public void TryWriteByte_WritesByteCorrectly()
    {
        // Arrange
        using var stream = new MemoryStream();
        byte testByte = 0xAB;

        // Act
        var result = stream.TryWriteByte(testByte);

        // Assert
        Assert.True(result);
        Assert.Equal(testByte, stream.ToArray()[0]);
    }

    [Fact(DisplayName = "TryWriteByte handles disposed stream gracefully")]
    public void TryWriteByte_DisposedStream_ReturnsFalse()
    {
        // Arrange
        var stream = new MemoryStream();
        stream.Dispose();

        // Act
        var result = stream.TryWriteByte(0x01);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region Write(string) Tests
    [Fact(DisplayName = "Write string uses specified encoding")]
    public void WriteString_UsesCorrectEncoding()
    {
        // Arrange
        using var stream = new MemoryStream();
        var text = "Hello 你好";
        var encoding = Encoding.Unicode;

        // Act
        stream.Write(text, encoding);
        stream.Position = 0;

        // Assert
        using var reader = new StreamReader(stream, encoding);
        Assert.Equal(text, reader.ReadToEnd());
    }

    [Fact(DisplayName = "Write string handles null inputs gracefully")]
    public void WriteString_NullInputs_NoOperation()
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        stream.Write(null, Encoding.UTF8);
        stream.Write(null, null);

        // Assert
        Assert.Empty(stream.ToArray());
    }
    #endregion

    #region TryWrite(string) Tests
    [Fact(DisplayName = "TryWrite string handles encoding failures")]
    public void TryWriteString_InvalidEncoding_ReturnsFalse()
    {
        // Arrange
        using var stream = new MemoryStream();
        var encoding = Encoding.GetEncoding(
            "ASCII",
            new EncoderExceptionFallback(),
            new DecoderExceptionFallback()
        );

        // Act
        var result = stream.TryWrite("café", encoding);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "TryWrite string returns true on success")]
    public void TryWriteString_ValidData_ReturnsTrue()
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        var result = stream.TryWrite("test", Encoding.UTF8);

        // Assert
        Assert.True(result);
        Assert.Equal("test", Encoding.UTF8.GetString(stream.ToArray()));
    }
    #endregion

    #region Helper Classes
    private class NonWritableStream : MemoryStream
    {
        public override bool CanWrite => false;
    }

    private class ExceptionThrowingStream : MemoryStream
    {
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new IOException("Simulated write failure");
        }
    }
    #endregion
}
