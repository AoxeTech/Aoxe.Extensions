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

    public class ToHexTests
    {
        [Fact]
        public void ToHex_NullInput_ThrowsArgumentNullException()
        {
            byte[]? nullArray = null;
            Assert.Throws<ArgumentNullException>(() => nullArray!.ToHex());
        }

        [Fact]
        public void ToHex_EmptyArray_ReturnsEmptyArray()
        {
            var emptyArray = Array.Empty<byte>();
            var result = emptyArray.ToHex();
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(new byte[] { 0x12 }, new byte[] { 0x01, 0x02 })] // 0x12 -> 0x01 0x02
        [InlineData(new byte[] { 0xAB }, new byte[] { 0x0A, 0x0B })] // 0xAB -> 0x0A 0x0B
        [InlineData(new byte[] { 0xFF }, new byte[] { 0x0F, 0x0F })] // 0xFF -> 0x0F 0x0F
        public void ToHex_ConvertsCorrectly(byte[] input, byte[] expected)
        {
            var result = input.ToHex();
            Assert.Equal(expected, result);
        }
    }

    public class FromHexTests
    {
        [Fact]
        public void FromHex_NullInput_ThrowsArgumentNullException()
        {
            byte[]? nullArray = null;
            Assert.Throws<ArgumentNullException>(() => nullArray!.FromHex());
        }

        [Fact]
        public void FromHex_OddLength_ThrowsArgumentException()
        {
            var invalidArray = new byte[] { 1, 2, 3 };
            Assert.Throws<ArgumentException>(() => invalidArray.FromHex());
        }

        [Fact]
        public void FromHex_EmptyArray_ReturnsEmptyArray()
        {
            var emptyArray = Array.Empty<byte>();
            var result = emptyArray.FromHex();
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(new byte[] { 0x01, 0x02 }, new byte[] { 0x12 })]
        [InlineData(new byte[] { 0x0A, 0x0B }, new byte[] { 0xAB })]
        [InlineData(new byte[] { 0x0F, 0x0F }, new byte[] { 0xFF })]
        public void FromHex_ConvertsCorrectly(byte[] input, byte[] expected)
        {
            var result = input.FromHex();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FromHex_RoundTripMaintainsOriginalData()
        {
            var original = new byte[] { 0x00, 0x01, 0xAB, 0xFF, 0x5F };
            var hex = original.ToHex();
            var reconstructed = hex.FromHex();
            Assert.Equal(original, reconstructed);
        }
    }

    [Fact]
    public void FullIntegrationTest()
    {
        var original = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

        var cloned = original.CloneNew();
        Assert.Equal(original, cloned);
        Assert.NotSame(original, cloned);

        var hex = cloned.ToHex();
        var expectedHex = new byte[]
        {
            0x00,
            0x01,
            0x02,
            0x03,
            0x04,
            0x05,
            0x06,
            0x07,
            0x08,
            0x09,
            0x0A,
            0x0B,
            0x0C,
            0x0D,
            0x0E,
            0x0F
        };
        Assert.Equal(expectedHex, hex);

        var reconstructed = hex.FromHex();
        Assert.Equal(original, reconstructed);
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

    [Fact]
    public void HexStringRoundTrip()
    {
        var original = new byte[] { 0x00, 0xFF, 0xAB, 0xCD };
        var hexString = original.ToHexString();
        var hexBytes = hexString.GetBytes();
        var reconstructed = hexBytes.FromHexToString().GetBytes();

        Assert.Equal(original, reconstructed);
    }
}
