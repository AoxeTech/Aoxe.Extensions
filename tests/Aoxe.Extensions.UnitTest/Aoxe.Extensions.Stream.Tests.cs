namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionStreamTests
{
    #region IsNullOrEmpty Tests
    [Fact]
    public void IsNullOrEmpty_NullStream_ReturnsTrue()
    {
        // Arrange
        Stream? nullStream = null;

        // Act
        var result = nullStream.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNullOrEmpty_EmptySeekableStream_ReturnsTrue()
    {
        // Arrange
        var emptyStream = new MemoryStream();

        // Act
        var result = emptyStream.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNullOrEmpty_NonSeekableStream_ReturnsFalse()
    {
        // Arrange
        var nonSeekableStream = new NonSeekableStream(new byte[] { 1, 2, 3 });

        // Act
        var result = nonSeekableStream.IsNullOrEmpty();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsNullOrEmpty_NonEmptyStream_ReturnsFalse()
    {
        // Arrange
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });

        // Act
        var result = stream.IsNullOrEmpty();

        // Assert
        Assert.False(result);
    }
    #endregion

    #region TrySeek Tests
    [Fact]
    public void TrySeek_NullStream_ThrowsArgumentNullException()
    {
        // Arrange
        Stream? nullStream = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => nullStream!.TrySeek(0, SeekOrigin.Begin));
    }

    [Fact]
    public void TrySeek_SeekableStream_ReturnsNewPosition()
    {
        // Arrange
        var stream = new MemoryStream(new byte[100]);
        long expectedPosition = 50;

        // Act
        var result = stream.TrySeek(expectedPosition, SeekOrigin.Begin);

        // Assert
        Assert.Equal(expectedPosition, result);
    }

    [Fact]
    public void TrySeek_NonSeekableStream_ReturnsNegativeOne()
    {
        // Arrange
        var nonSeekableStream = new NonSeekableStream(new byte[100]);

        // Act
        var result = nonSeekableStream.TrySeek(0, SeekOrigin.Begin);

        // Assert
        Assert.Equal(-1, result);
    }
    #endregion

    #region ToMemoryStream Tests
    [Fact]
    public void ToMemoryStream_NullStream_ThrowsArgumentNullException()
    {
        // Arrange
        Stream? nullStream = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => nullStream!.ToMemoryStream());
    }

    [Fact]
    public void ToMemoryStream_NonReadableStream_ThrowsNotSupportedException()
    {
        // Arrange
        var nonReadableStream = new NonReadableStream(new byte[10]);

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => nonReadableStream.ToMemoryStream());
    }

    [Fact]
    public void ToMemoryStream_SeekableStream_CopiesContentAndResetsPosition()
    {
        // Arrange
        var originalData = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(originalData);
        stream.Position = 1;

        // Act
        var result = stream.ToMemoryStream();

        // Assert
        Assert.Equal(0, result.Position);
        Assert.Equal(originalData, result.ToArray());
        Assert.Equal(1, stream.Position); // Original position restored
    }

    [Fact]
    public void ToMemoryStream_NonSeekableStream_CopiesContent()
    {
        // Arrange
        var originalData = new byte[] { 1, 2, 3 };
        var nonSeekableStream = new NonSeekableStream(originalData);

        // Act
        var result = nonSeekableStream.ToMemoryStream();

        // Assert
        Assert.Equal(originalData, result.ToArray());
    }
    #endregion

    #region ToReadOnlyMemory Tests
    [Fact]
    public void ToReadOnlyMemory_NullStream_ThrowsArgumentNullException()
    {
        // Arrange
        Stream? nullStream = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => nullStream!.ToReadOnlyMemory());
    }

    [Fact]
    public void ToReadOnlyMemory_ValidStream_ReturnsCorrectMemory()
    {
        // Arrange
        var expectedData = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(expectedData);

        // Act
        var result = stream.ToReadOnlyMemory();

        // Assert
        Assert.Equal(expectedData, result.ToArray());
    }
    #endregion

    #region ToReadOnlySequence Tests
    [Fact]
    public void ToReadOnlySequence_NullStream_ThrowsArgumentNullException()
    {
        // Arrange
        Stream? nullStream = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => nullStream!.ToReadOnlySequence());
    }

    [Fact]
    public void ToReadOnlySequence_ValidStream_ReturnsCorrectSequence()
    {
        // Arrange
        var expectedData = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(expectedData);

        // Act
        var result = stream.ToReadOnlySequence();

        // Assert
        Assert.Equal(expectedData, result.ToArray());
    }
    #endregion

    #region Helper Classes
    private class NonSeekableStream(byte[] data) : MemoryStream(data)
    {
        public override bool CanSeek => false;
    }

    private class NonReadableStream(byte[] data) : MemoryStream(data)
    {
        public override bool CanRead => false;
    }
    #endregion
}
