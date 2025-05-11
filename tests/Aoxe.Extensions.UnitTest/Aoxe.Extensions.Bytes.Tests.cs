namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionTests
{
    public class CloneNewTests
    {
        [Fact]
        public void CloneNew_NullInput_ThrowsArgumentNullException()
        {
            byte[]? nullArray = null;
            Assert.Throws<ArgumentNullException>(() => nullArray!.CloneNew());
        }

        [Fact]
        public void CloneNew_EmptyArray_ReturnsEmptyArray()
        {
            var emptyArray = Array.Empty<byte>();
            var result = emptyArray.CloneNew();
            Assert.Empty(result);
        }

        [Fact]
        public void CloneNew_ReturnsDeepCopy()
        {
            var original = new byte[] { 1, 2, 3, 4, 5 };
            var clone = original.CloneNew();

            Assert.Equal(original, clone);
            Assert.NotSame(original, clone);

            // Modify original and verify clone remains unchanged
            original[0] = 100;
            Assert.Equal(1, clone[0]);
        }
    }
}

public class EdgeCaseTests
{
    [Fact]
    public void AllMethods_HandleEmptyArrays()
    {
        var empty = Array.Empty<byte>();

        Assert.Empty(empty.ToMemoryStream().ToArray());
        Assert.Equal("", empty.ToBase64String());
        Assert.Equal("", empty.ToHexString());
    }

    [Fact]
    public void AllMethods_ThrowOnNullInput()
    {
        byte[]? nullArray = null;
        Assert.Throws<ArgumentNullException>(() => nullArray!.ToMemoryStream());
        Assert.Throws<ArgumentNullException>(() => nullArray!.GetString());
        Assert.Throws<ArgumentNullException>(() => nullArray!.ToHexString());
    }
}

public class IntegrationTests
{
    [Fact]
    public void FullEncodingRoundTrip()
    {
        var original = "Hello World! 👋";
        var bytes = original.GetBytes();

        var base64String = bytes.ToBase64String();
        var decodedString = base64String.GetBytes().DecodeBase64ToString();

        Assert.Equal(original, decodedString);
    }
}
