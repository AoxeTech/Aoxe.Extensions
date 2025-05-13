namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringStreamTests
{
    #region ToMemoryStream Tests

    [Fact]
    public void ToMemoryStream_WithUtf8Encoding_ContainsCorrectBytes()
    {
        // Arrange
        const string input = "Hello 世界";
        var expectedBytes = Encoding.UTF8.GetBytes(input);

        // Act
        using var stream = input.ToMemoryStream();

        // Assert
        Assert.Equal(expectedBytes, stream.ToArray());
        Assert.True(stream.CanRead);
        Assert.Equal(0, stream.Position);
    }

    [Fact]
    public void ToMemoryStream_WithEmptyString_ReturnsEmptyStream()
    {
        // Arrange
        const string input = "";

        // Act
        using var stream = input.ToMemoryStream();

        // Assert
        Assert.Equal(0, stream.Length);
    }

    #endregion

    #region WriteTo Tests

    [Fact]
    public void WriteTo_WritesCorrectBytesToStream()
    {
        // Arrange
        const string input = "Test Data";
        var encoding = Encoding.ASCII;
        using var stream = new MemoryStream();
        var expectedBytes = encoding.GetBytes(input);

        // Act
        input.WriteTo(stream, encoding);

        // Assert
        Assert.Equal(expectedBytes, stream.ToArray());
        Assert.Equal(expectedBytes.Length, stream.Position);
    }

    [Fact]
    public void WriteTo_WhenStreamClosed_ThrowsInvalidOperation()
    {
        // Arrange
        const string input = "Test";
        using var stream = new MemoryStream();
        stream.Close();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => input.WriteTo(stream));
    }

    [Fact]
    public void WriteTo_WithNullStream_ThrowsArgumentNull()
    {
        // Arrange
        const string input = "Test";
        Stream? stream = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => input.WriteTo(stream!));
    }

    #endregion

    #region TryWriteTo Tests

    [Fact]
    public void TryWriteTo_WithValidStream_ReturnsTrueAndWritesData()
    {
        // Arrange
        const string input = "Successful write";
        using var stream = new MemoryStream();
        var expectedBytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = input.TryWriteTo(stream);

        // Assert
        Assert.True(result);
        Assert.Equal(expectedBytes, stream.ToArray());
    }

    [Fact]
    public void TryWriteTo_WithReadOnlyStream_ReturnsFalse()
    {
        // Arrange
        const string input = "Test";
        using var stream = new MemoryStream(new byte[10], writable: false);

        // Act
        var result = input.TryWriteTo(stream);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryWriteTo_WithDisposedStream_ReturnsFalse()
    {
        // Arrange
        const string input = "Test";
        var stream = new MemoryStream();
        stream.Dispose();

        // Act
        var result = input.TryWriteTo(stream);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryWriteTo_WithNullStream_ReturnsFalse()
    {
        // Arrange
        const string input = "Test";

        // Act
        var result = input.TryWriteTo(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryWriteTo_WithEmptyString_WritesNothing()
    {
        // Arrange
        const string input = "";
        using var stream = new MemoryStream();

        // Act
        var result = input.TryWriteTo(stream);

        // Assert
        Assert.True(result);
        Assert.Equal(0, stream.Length);
    }

    #endregion

    #region Cross-Method Tests

    [Fact]
    public void WriteToAndTryWriteTo_ProduceSameResults()
    {
        // Arrange
        const string input = "Consistent output";
        var encoding = Encoding.Unicode;
        using var stream1 = new MemoryStream();
        using var stream2 = new MemoryStream();

        // Act
        input.WriteTo(stream1, encoding);
        input.TryWriteTo(stream2, encoding);

        // Assert
        Assert.Equal(stream1.ToArray(), stream2.ToArray());
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void AllMethods_WithNullInput_ThrowArgumentNull()
    {
        // Arrange
        const string? input = null;
        using var stream = new MemoryStream();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => input!.ToMemoryStream());
        Assert.Throws<ArgumentNullException>(() => input!.WriteTo(stream));
        Assert.Throws<ArgumentNullException>(() => input!.TryWriteTo(stream));
    }

    [Fact]
    public void HighCapacityString_HandlesLargeData()
    {
        // Arrange
        var input = new string('X', 100000);
        using var stream = new MemoryStream();

        // Act
        input.WriteTo(stream);

        // Assert
        Assert.Equal(Encoding.UTF8.GetBytes(input), stream.ToArray());
    }

    #endregion
}
