namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStreamTimeoutTest
{
    #region TrySetReadTimeout (int milliseconds) Tests
    [Fact]
    public void TrySetReadTimeout_NegativeMilliseconds_ThrowsArgumentOutOfRange()
    {
        // Arrange
        Stream? stream = null;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => stream.TrySetReadTimeout(-1));
    }

    [Fact]
    public void TrySetReadTimeout_NullStream_ReturnsFalse()
    {
        // Arrange
        Stream? stream = null;

        // Act
        var result = stream.TrySetReadTimeout(100);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TrySetReadTimeout_NonTimeoutStream_ReturnsFalse()
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        var result = stream.TrySetReadTimeout(100);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TrySetReadTimeout_ValidStream_SetsTimeoutAndReturnsTrue()
    {
        // Arrange
        using var stream = new TestStream();
        const int timeout = 500;

        // Act
        var result = stream.TrySetReadTimeout(timeout);

        // Assert
        Assert.True(result);
        Assert.Equal(timeout, stream.ReadTimeout);
    }

    [Fact]
    public void TrySetReadTimeout_InvalidOperation_ReturnsFalse()
    {
        // Arrange
        using var stream = new TestStream { SimulateSetError = true };

        // Act
        var result = stream.TrySetReadTimeout(500);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region TrySetReadTimeout (TimeSpan) Tests
    [Fact]
    public void TrySetReadTimeout_ValidTimeSpan_ConvertsAndSetsTimeout()
    {
        // Arrange
        using var stream = new TestStream();
        var timeout = TimeSpan.FromSeconds(5);

        // Act
        var result = stream.TrySetReadTimeout(timeout);

        // Assert
        Assert.True(result);
        Assert.Equal(5000, stream.ReadTimeout);
    }

    [Fact]
    public void TrySetReadTimeout_OverflowTimeSpan_ThrowsOverflowException()
    {
        // Arrange
        Stream? stream = null;
        var timeout = TimeSpan.FromMilliseconds(int.MaxValue + 1L);

        // Act & Assert
        Assert.Throws<OverflowException>(() => stream.TrySetReadTimeout(timeout));
    }
    #endregion

    #region TrySetWriteTimeout (int milliseconds) Tests
    [Fact]
    public void TrySetWriteTimeout_NegativeMilliseconds_ThrowsArgumentOutOfRange()
    {
        // Arrange
        Stream? stream = null;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => stream.TrySetWriteTimeout(-1));
    }

    [Fact]
    public void TrySetWriteTimeout_NullStream_ReturnsFalse()
    {
        // Arrange
        Stream? stream = null;

        // Act
        var result = stream.TrySetWriteTimeout(100);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TrySetWriteTimeout_NonTimeoutStream_ReturnsFalse()
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        var result = stream.TrySetWriteTimeout(100);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TrySetWriteTimeout_ValidStream_SetsTimeoutAndReturnsTrue()
    {
        // Arrange
        using var stream = new TestStream();
        const int timeout = 500;

        // Act
        var result = stream.TrySetWriteTimeout(timeout);

        // Assert
        Assert.True(result);
        Assert.Equal(timeout, stream.WriteTimeout);
    }

    [Fact]
    public void TrySetWriteTimeout_InvalidOperation_ReturnsFalse()
    {
        // Arrange
        using var stream = new TestStream { SimulateSetError = true };

        // Act
        var result = stream.TrySetWriteTimeout(500);

        // Assert
        Assert.False(result);
    }
    #endregion

    #region TrySetWriteTimeout (TimeSpan) Tests
    [Fact]
    public void TrySetWriteTimeout_ValidTimeSpan_ConvertsAndSetsTimeout()
    {
        // Arrange
        using var stream = new TestStream();
        var timeout = TimeSpan.FromSeconds(5);

        // Act
        var result = stream.TrySetWriteTimeout(timeout);

        // Assert
        Assert.True(result);
        Assert.Equal(5000, stream.WriteTimeout);
    }

    [Fact]
    public void TrySetWriteTimeout_OverflowTimeSpan_ThrowsOverflowException()
    {
        // Arrange
        Stream? stream = null;
        var timeout = TimeSpan.FromMilliseconds(int.MaxValue + 1L);

        // Act & Assert
        Assert.Throws<OverflowException>(() => stream.TrySetWriteTimeout(timeout));
    }
    #endregion

    private class TestStream : Stream
    {
        #region Required Overrides
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => throw new NotSupportedException();
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }
        private int _readTimeout;
        private int _writeTimeout;
        public bool SimulateSetError { get; set; }

        public override bool CanTimeout => true;

        public override int ReadTimeout
        {
            get => _readTimeout;
            set
            {
                if (SimulateSetError)
                    throw new InvalidOperationException();
                _readTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get => _writeTimeout;
            set
            {
                if (SimulateSetError)
                    throw new InvalidOperationException();
                _writeTimeout = value;
            }
        }

        public override void Flush() { }

        public override int Read(byte[] buffer, int offset, int count) => 0;

        public override long Seek(long offset, SeekOrigin origin) =>
            throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) { }

        #endregion
    }
}
