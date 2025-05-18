namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsBytesStringTests
{
    private const string TestString = "Hello 世界!";
    private readonly byte[] _testBytesUtf8 = Encoding.UTF8.GetBytes(TestString);
    private readonly byte[] _testBytesAscii = Encoding.ASCII.GetBytes("ASCII test");
    private readonly byte[] _testBytesUnicode = Encoding.Unicode.GetBytes(TestString);

    #region Null Argument Tests
    [Theory]
    [MemberData(nameof(AllExtensionMethods))]
    public void AllMethods_NullInput_ThrowsArgumentNullException(Func<byte[], string> method)
    {
        byte[] nullBytes = null;
        var ex = Assert.Throws<ArgumentNullException>(() => method(nullBytes));
        Assert.Equal("bytes", ex.ParamName);
    }

    public static IEnumerable<object[]> AllExtensionMethods()
    {
        yield return [(Func<byte[], string>)AoxeExtension.GetStringByUtf8];
        yield return [(Func<byte[], string>)AoxeExtension.GetStringByAscii];
        yield return [(Func<byte[], string>)AoxeExtension.GetStringByBigEndianUnicode];
        yield return [(Func<byte[], string>)AoxeExtension.GetStringByDefault];
        yield return [(Func<byte[], string>)AoxeExtension.GetStringByUtf32];
        yield return [(Func<byte[], string>)AoxeExtension.GetStringByUnicode];
        yield return [(Func<byte[], string>)(b => b.GetString())];
    }
    #endregion

    #region Empty Array Tests
    [Theory]
    [MemberData(nameof(AllExtensionMethods))]
    public void AllMethods_EmptyArray_ReturnsEmptyString(Func<byte[], string> method)
    {
        byte[] emptyBytes = [];
        var result = method(emptyBytes);
        Assert.Equal(string.Empty, result);
    }
    #endregion

    #region Specific Encoding Tests
    [Fact]
    public void GetStringByUtf8_ValidBytes_ReturnsCorrectString()
    {
        var result = _testBytesUtf8.GetStringByUtf8();
        Assert.Equal(TestString, result);
    }

    [Fact]
    public void GetStringByAscii_ValidBytes_ReturnsCorrectString()
    {
        var result = _testBytesAscii.GetStringByAscii();
        Assert.Equal("ASCII test", result);
    }

    [Fact]
    public void GetStringByUnicode_ValidBytes_ReturnsCorrectString()
    {
        var result = _testBytesUnicode.GetStringByUnicode();
        Assert.Equal(TestString, result);
    }

    [Fact]
    public void GetStringByBigEndianUnicode_ValidBytes_ReturnsCorrectString()
    {
        var bytes = Encoding.BigEndianUnicode.GetBytes(TestString);
        var result = bytes.GetStringByBigEndianUnicode();
        Assert.Equal(TestString, result);
    }

    [Fact]
    public void GetStringByUtf32_ValidBytes_ReturnsCorrectString()
    {
        var bytes = Encoding.UTF32.GetBytes(TestString);
        var result = bytes.GetStringByUtf32();
        Assert.Equal(TestString, result);
    }
    #endregion

    #region Default Encoding Tests
    [Fact]
    public void GetStringByDefault_ValidBytes_UsesSystemDefaultEncoding()
    {
        var bytes = Encoding.Default.GetBytes(TestString);
        var result = bytes.GetStringByDefault();
        Assert.Equal(TestString, result);
    }
    #endregion

    #region Parameterized GetString Tests
    [Theory]
    [InlineData(null, "UTF-8")]
    [InlineData(typeof(UnicodeEncoding), "Unicode")]
    [InlineData(typeof(UTF32Encoding), "UTF-32")]
    public void GetString_WithDifferentEncodings_UsesCorrectEncoding(
        Type encodingType,
        string expectedEncodingName
    )
    {
        var encoding =
            encodingType == null ? null : (Encoding)Activator.CreateInstance(encodingType);

        var bytes = (encoding ?? Encoding.UTF8).GetBytes(TestString);
        var result = bytes.GetString(encoding);

        Assert.Equal(TestString, result);
        Assert.Contains(expectedEncodingName, (encoding ?? Encoding.UTF8).EncodingName);
    }

    [Fact]
    public void GetString_WithNullEncoding_UsesUtf8AsDefault()
    {
        var bytes = Encoding.UTF8.GetBytes(TestString);
        var result = bytes.GetString(null);
        Assert.Equal(TestString, result);
    }
    #endregion

    #region Special Case Tests
    [Fact]
    public void GetStringByAscii_WithNonAsciiCharacters_ReplacesInvalidChars()
    {
        var bytes = Encoding.ASCII.GetBytes(TestString);
        var result = bytes.GetStringByAscii();
        Assert.NotEqual(TestString, result); // Should replace 世 and 界
        Assert.DoesNotContain("世", result);
        Assert.DoesNotContain("界", result);
    }

    [Fact]
    public void GetStringByDefault_CrossPlatformBehavior_ConsistentResults()
    {
        var bytes = Encoding.Default.GetBytes("Test");
        var result = bytes.GetStringByDefault();
        Assert.Equal("Test", result); // Should work consistently across platforms
    }
    #endregion
}
