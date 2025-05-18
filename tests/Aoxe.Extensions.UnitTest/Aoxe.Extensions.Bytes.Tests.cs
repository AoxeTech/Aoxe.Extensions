namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionBytesTests
{
    // Test null input throws ArgumentNullException
    [Fact]
    public void CloneNew_NullSource_ThrowsArgumentNullException()
    {
        byte[] source = null;
        var ex = Assert.Throws<ArgumentNullException>(() => source.CloneNew());
        Assert.Equal("src", ex.ParamName);
    }

    // Test empty array returns empty array
    [Fact]
    public void CloneNew_EmptyArray_ReturnsEmptyArray()
    {
        byte[] source = [];
        var result = source.CloneNew();

        Assert.NotSame(source, result);
        Assert.Empty(result);
    }

    // Test non-empty array cloning
    [Fact]
    public void CloneNew_NonEmptyArray_ProducesCorrectClone()
    {
        byte[] source = [0x01, 0x02, 0x03];
        var result = source.CloneNew();

        Assert.NotSame(source, result);
        Assert.True(source.SequenceEqual(result));
    }

    // Test modification of clone doesn't affect source
    [Fact]
    public void CloneNew_ModifiedClone_DoesNotAffectSource()
    {
        byte[] source = [0x10, 0x20, 0x30];
        var clone = source.CloneNew();
        clone[0] = 0xFF;

        Assert.Equal(0x10, source[0]);
        Assert.Equal(0xFF, clone[0]);
    }

    // Parameterized test for different array sizes
    [Theory]
    [InlineData(new byte[0])]
    [InlineData(new byte[] { 0x00 })]
    [InlineData(new byte[] { 0x55, 0xAA })]
    [InlineData(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 })]
    public void CloneNew_VariousSizes_ClonesCorrectly(byte[] source)
    {
        var result = source.CloneNew();

        Assert.Equal(source.Length, result.Length);
        Assert.True(source.SequenceEqual(result));
        Assert.False(ReferenceEquals(source, result));
    }

    // Test maximum array length (boundary case)
    [Fact]
    public void CloneNew_LargeArray_ClonesSuccessfully()
    {
        byte[] source = new byte[100_000];
        new Random().NextBytes(source);

        var result = source.CloneNew();

        Assert.Equal(source.Length, result.Length);
        Assert.True(source.SequenceEqual(result));
    }
}
