namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamAsyncTests
{
    [Fact(DisplayName = "Should copy stream content to MemoryStream")]
    public async Task ToMemoryStreamAsync_CopiesContentCorrectly()
    {
        // Arrange
        var expected = Encoding.UTF8.GetBytes("Test content");
        using var sourceStream = new MemoryStream(expected);

        // Act
        var resultStream = await sourceStream.ToMemoryStreamAsync();
        var actual = resultStream.ToArray();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Should reset source stream position after copy")]
    public async Task ToMemoryStreamAsync_ResetsSourceStreamPosition()
    {
        // Arrange
        var data = Encoding.UTF8.GetBytes("Test data");
        using var sourceStream = new MemoryStream(data);
        sourceStream.Position = data.Length;

        // Act
        _ = await sourceStream.ToMemoryStreamAsync();

        // Assert
        Assert.Equal(0, sourceStream.Position);
    }

    [Fact(DisplayName = "Should reset MemoryStream position before returning")]
    public async Task ToMemoryStreamAsync_ResetsResultStreamPosition()
    {
        // Arrange
        using var sourceStream = new MemoryStream(Encoding.UTF8.GetBytes("Content"));

        // Act
        var resultStream = await sourceStream.ToMemoryStreamAsync();

        // Assert
        Assert.Equal(0, resultStream.Position);
    }

    [Fact(DisplayName = "Should handle non-seekable streams gracefully")]
    public async Task ToMemoryStreamAsync_HandlesNonSeekableStreams()
    {
        // Arrange
        var data = Encoding.UTF8.GetBytes("Non-seekable content");
        using var sourceStream = new NonSeekableMemoryStream(data);

        // Act
        var resultStream = await sourceStream.ToMemoryStreamAsync();
        var resultData = resultStream.ToArray();

        // Assert
        Assert.Equal(data, resultData);
        Assert.Equal(0, resultStream.Position);
    }

    [Fact(DisplayName = "ReadToEnd with FileStream reads from current position")]
    public void ReadToEnd_FileStream_ReadsFromCurrentPosition()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        try
        {
            // Write test bytes directly
            File.WriteAllBytes(tempFile, [1, 2, 3, 4]);
            using var fileStream = File.OpenRead(tempFile);
            fileStream.Position = 2;

            // Act
            var result = fileStream.ReadToEnd();

            // Assert
            Assert.Equal([3, 4], result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    // Helper class for testing non-seekable streams
    private class NonSeekableMemoryStream(byte[] buffer) : MemoryStream(buffer)
    {
        public override bool CanSeek => false;

        public override long Position
        {
            get => base.Position;
            set => throw new NotSupportedException();
        }
    }
}
