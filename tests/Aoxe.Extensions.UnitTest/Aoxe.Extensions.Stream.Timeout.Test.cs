namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamTimeoutTest
{
    #region TrySetReadTimeout Tests
    [Fact(DisplayName = "TrySetReadTimeout with null stream returns false")]
    public void TrySetReadTimeout_NullStream_ReturnsFalse()
    {
        // Arrange
        Stream nullStream = null!;

        // Act
        var result = nullStream.TrySetReadTimeout(1000);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "TrySetReadTimeout with non-timeout stream returns false")]
    public void TrySetReadTimeout_NonTimeoutStream_ReturnsFalse()
    {
        // Arrange
        using var stream = new NonTimeoutStream();

        // Act
        var result = stream.TrySetReadTimeout(1000);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "TrySetReadTimeout with valid stream sets timeout")]
    public void TrySetReadTimeout_ValidStream_SetsTimeout()
    {
        // Arrange
        using var stream = new TimeoutCapableStream();
        const int testTimeout = 5000;

        // Act
        var result = stream.TrySetReadTimeout(testTimeout);

        // Assert
        Assert.True(result);
        Assert.Equal(testTimeout, stream.ReadTimeout);
    }

    [Fact(DisplayName = "TrySetReadTimeout throws for negative milliseconds")]
    public void TrySetReadTimeout_NegativeMilliseconds_ThrowsException()
    {
        // Arrange
        using var stream = new TimeoutCapableStream();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => stream.TrySetReadTimeout(-100));
    }

    [Fact(DisplayName = "TrySetReadTimeout handles invalid operation exceptions")]
    public void TrySetReadTimeout_InvalidOperation_ReturnsFalse()
    {
        // Arrange
        using var stream = new InvalidOperationStream();

        // Act
        var result = stream.TrySetReadTimeout(1000);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region TrySetReadTimeout TimeSpan Tests
    [Fact(DisplayName = "TrySetReadTimeout with TimeSpan converts correctly")]
    public void TrySetReadTimeout_ValidTimeSpan_ConvertsCorrectly()
    {
        // Arrange
        using var stream = new TimeoutCapableStream();
        var timeout = TimeSpan.FromSeconds(5);

        // Act
        var result = stream.TrySetReadTimeout(timeout);

        // Assert
        Assert.True(result);
        Assert.Equal(5000, stream.ReadTimeout);
    }

    [Fact(DisplayName = "TrySetReadTimeout with large TimeSpan throws overflow")]
    public void TrySetReadTimeout_TooLargeTimeSpan_ThrowsOverflow()
    {
        // Arrange
        using var stream = new TimeoutCapableStream();
        var timeout = TimeSpan.FromMilliseconds(int.MaxValue + 1.0);

        // Act & Assert
        Assert.Throws<OverflowException>(() => stream.TrySetReadTimeout(timeout));
    }
    #endregion

    #region TrySetWriteTimeout Tests
    [Fact(DisplayName = "TrySetWriteTimeout with valid stream sets timeout")]
    public void TrySetWriteTimeout_ValidStream_SetsTimeout()
    {
        // Arrange
        using var stream = new TimeoutCapableStream();
        const int testTimeout = 3000;

        // Act
        var result = stream.TrySetWriteTimeout(testTimeout);

        // Assert
        Assert.True(result);
        Assert.Equal(testTimeout, stream.WriteTimeout);
    }

    [Fact(DisplayName = "TrySetWriteTimeout throws for negative milliseconds")]
    public void TrySetWriteTimeout_NegativeMilliseconds_ThrowsException()
    {
        // Arrange
        using var stream = new TimeoutCapableStream();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => stream.TrySetWriteTimeout(-200));
    }
    #endregion

    #region Helper Streams
    private class TimeoutCapableStream : MemoryStream
    {
        public override bool CanTimeout => true;
        public override int ReadTimeout { get; set; } = -1;
        public override int WriteTimeout { get; set; } = -1;
    }

    private class InvalidOperationStream : TimeoutCapableStream
    {
        public override int ReadTimeout
        {
            set => throw new InvalidOperationException();
        }

        public override int WriteTimeout
        {
            set => throw new InvalidOperationException();
        }
    }

    private class NonTimeoutStream : MemoryStream
    {
        public override bool CanTimeout => false;
    }
    #endregion
}
