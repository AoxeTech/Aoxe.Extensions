namespace Aoxe.Extensions.UnitTest;

public class BytesStreamTests
{
    private static readonly byte[] TestData = [1, 2, 3, 4, 5];

    [Fact]
    public void ToMemoryStream_EmptyArray_CreatesEmptyStream()
    {
        var bytes = Array.Empty<byte>();
        using var stream = bytes.ToMemoryStream();

        Assert.Equal(0, stream.Length);
        Assert.Equal(0, stream.Position);
    }

    [Fact]
    public void ToMemoryStream_WithData_CreatesPopulatedStream()
    {
        using var stream = TestData.ToMemoryStream();

        Assert.Equal(TestData.Length, stream.Length);
        Assert.Equal(0, stream.Position);
        Assert.Equal(TestData, stream.ToArray());
    }

    [Fact]
    public void WriteTo_ToEmptyStream_WritesCorrectly()
    {
        using var stream = new MemoryStream();
        TestData.WriteTo(stream);

        Assert.Equal(TestData.Length, stream.Length);
        Assert.Equal(TestData, stream.ToArray());
    }

    [Fact]
    public void WriteTo_ToExistingStream_AppendsCorrectly()
    {
        using var stream = new MemoryStream();
        var initial = new byte[] { 0xFF };
        stream.Write(initial, 0, initial.Length);

        TestData.WriteTo(stream);

        var expected = new byte[] { 0xFF, 1, 2, 3, 4, 5 };
        Assert.Equal(expected, stream.ToArray());
    }

    [Fact]
    public void WriteTo_ToNullStream_ThrowsArgumentNullException()
    {
        Stream? stream = null;
        Assert.Throws<NullReferenceException>(() => TestData.WriteTo(stream));
    }

    [Fact]
    public void WriteTo_ToClosedStream_ThrowsObjectDisposedException()
    {
        var stream = new MemoryStream();
        stream.Dispose();

        Assert.Throws<ObjectDisposedException>(() => TestData.WriteTo(stream));
    }

    [Fact]
    public void TryWriteTo_ToValidStream_ReturnsTrue()
    {
        using var stream = new MemoryStream();
        var result = TestData.TryWriteTo(stream);

        Assert.True(result);
        Assert.Equal(TestData, stream.ToArray());
    }

    [Fact]
    public void TryWriteTo_ToNonWritableStream_ReturnsFalse()
    {
        using var stream = new MemoryStream([], writable: false);
        var result = TestData.TryWriteTo(stream);

        Assert.False(result);
    }

    [Fact]
    public void TryWriteTo_ToNullStream_ReturnsFalse()
    {
        Stream stream = null!;
        var result = TestData.TryWriteTo(stream);

        Assert.False(result);
    }
}
