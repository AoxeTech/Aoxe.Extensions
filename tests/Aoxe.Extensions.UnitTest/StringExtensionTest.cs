namespace Aoxe.Extensions.UnitTest;

public class StringExtensionTest
{
    [Fact]
    public void TrimStartTest()
    {
        const string target = "apple,banana,pear";
        const string trimString = "apple";
        Assert.Equal(",banana,pear", target.TrimStart(trimString));
        Assert.Equal(target, target.TrimStart(string.Empty));
    }

    [Fact]
    public void TrimEndTest()
    {
        const string target = "apple,banana,pear";
        const string trimString = "pear";
        Assert.Equal("apple,banana,", target.TrimEnd(trimString));
        Assert.Equal(target, target.TrimEnd(string.Empty));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Alice")]
    public void IsNullOrEmptyTest(string? str) =>
        Assert.Equal(str.IsNullOrEmpty(), string.IsNullOrEmpty(str));

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Alice")]
    public void IsNullOrWhiteSpaceTest(string? str) =>
        Assert.Equal(str.IsNullOrWhiteSpace(), string.IsNullOrWhiteSpace(str));

    [Theory]
    [InlineData(" ")]
    [InlineData(",")]
    [InlineData(" or ")]
    public void StringJoinTest(string separator)
    {
        var stringList = new List<string> { "Alice", "Bob", "Carol", "Dave", "Eve" };
        Assert.Equal(stringList.JoinToString(separator), string.Join(separator, stringList));
    }

    #region Bytes

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void GetUtf8BytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.GetUtf8Bytes(), Encoding.UTF8.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void GetAsciiBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.GetAsciiBytes(), Encoding.ASCII.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void GetBigEndianUnicodeBytesTest(string str) =>
        Assert.True(
            TestHelper.BytesEqual(
                str.GetBigEndianUnicodeBytes(),
                Encoding.BigEndianUnicode.GetBytes(str)
            )
        );

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void GetDefaultBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.GetDefaultBytes(), Encoding.Default.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void GetUtf32BytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.GetUtf32Bytes(), Encoding.UTF32.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void GetUnicodeBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.GetUnicodeBytes(), Encoding.Unicode.GetBytes(str)));

    #endregion

    #region Parse

    [Theory]
    [InlineData(sbyte.MaxValue)]
    [InlineData(sbyte.MinValue)]
    [InlineData(0)]
    public void ParseSbyteTest(sbyte value) => Assert.Equal(value, value.ToString().ParseSbyte());

    [Theory]
    [InlineData(byte.MaxValue)]
    [InlineData(byte.MinValue)]
    [InlineData(0)]
    public void ParseByteTest(byte value) => Assert.Equal(value, value.ToString().ParseByte());

    [Theory]
    [InlineData(short.MaxValue)]
    [InlineData(short.MinValue)]
    [InlineData(0)]
    public void ParseShortTest(short value) => Assert.Equal(value, value.ToString().ParseShort());

    [Theory]
    [InlineData(ushort.MaxValue)]
    [InlineData(ushort.MinValue)]
    [InlineData(0)]
    public void ParseUshortTest(ushort value) =>
        Assert.Equal(value, value.ToString().ParseUshort());

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    [InlineData(0)]
    public void ParseIntTest(int value) => Assert.Equal(value, value.ToString().ParseInt());

    [Theory]
    [InlineData(uint.MaxValue)]
    [InlineData(uint.MinValue)]
    [InlineData(0)]
    public void ParseUintTest(uint value) => Assert.Equal(value, value.ToString().ParseUint());

    [Theory]
    [InlineData(long.MaxValue)]
    [InlineData(long.MinValue)]
    [InlineData(0)]
    public void ParseLongTest(long value) => Assert.Equal(value, value.ToString().ParseLong());

    [Theory]
    [InlineData(ulong.MaxValue)]
    [InlineData(ulong.MinValue)]
    [InlineData(0)]
    public void ParseUlongTest(ulong value) => Assert.Equal(value, value.ToString().ParseUlong());

    [Theory]
    [InlineData(float.MaxValue)]
    [InlineData(float.MinValue)]
    [InlineData(0)]
    public void ParseFloatTest(float value)
    {
#if NET48
        Assert.Equal(value, value.ToString("R").ParseFloat());
#else
        Assert.Equal(value, value.ToString(CultureInfo.InvariantCulture).ParseFloat());
#endif
    }

    [Theory]
    [InlineData(double.MaxValue)]
    [InlineData(double.MinValue)]
    [InlineData(0)]
    public void ParseDoubleTest(double value)
    {
#if NET48
        Assert.Equal(value, value.ToString("R").ParseDouble());
#else
        Assert.Equal(value, value.ToString(CultureInfo.InvariantCulture).ParseDouble());
#endif
    }

    [Fact]
    public void ParseDecimalTest()
    {
        Assert.Equal(
            decimal.MaxValue,
            decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ParseDecimal()
        );
        Assert.Equal(
            decimal.MinValue,
            decimal.MinValue.ToString(CultureInfo.InvariantCulture).ParseDecimal()
        );
        Assert.Equal(0M, 0M.ToString(CultureInfo.InvariantCulture).ParseDecimal());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ParseBoolTest(bool value) => Assert.Equal(value, value.ToString().ParseBool());

    [Fact]
    public void ParseDateTimeTest()
    {
        var maxDateTime = new DateTime(9999, 12, 31, 23, 59, 59);
        var minDateTime = new DateTime(0001, 01, 01, 00, 00, 00);
        Assert.Equal(
            maxDateTime,
            maxDateTime.ToString(CultureInfo.InvariantCulture).ParseDateTime()
        );
        Assert.Equal(
            minDateTime,
            minDateTime.ToString(CultureInfo.InvariantCulture).ParseDateTime()
        );
    }

    [Fact]
    public void ParseDateTimeOffsetTest()
    {
        var maxDateTimeOffset = new DateTimeOffset(9999, 12, 31, 23, 59, 59, TimeSpan.Zero);
        var minDateTimeOffset = new DateTimeOffset(0001, 01, 01, 00, 00, 00, TimeSpan.Zero);
        Assert.Equal(maxDateTimeOffset, maxDateTimeOffset.ToString().ParseDateTimeOffset());
        Assert.Equal(minDateTimeOffset, minDateTimeOffset.ToString().ParseDateTimeOffset());
    }

    [Fact]
    public void ParseEnumTest()
    {
        Assert.Equal(TestEnum.Create, TestEnum.Create.ToString().ParseEnum(typeof(TestEnum)));
        Assert.Equal(
            TestEnum.Create,
            TestEnum.Create.ToString().ToLower().ParseEnum(typeof(TestEnum), true)
        );
        Assert.Throws<ArgumentException>(
            () => TestEnum.Create.ToString().ToLower().ParseEnum(typeof(TestEnum))
        );
    }

    #endregion

    #region TryParse

    [Fact]
    public void TryParseSbyteTest()
    {
        Assert.Equal(sbyte.MaxValue, sbyte.MaxValue.ToString().TryParseSbyte());
        Assert.Equal(sbyte.MinValue, sbyte.MinValue.ToString().TryParseSbyte());
        Assert.Equal(0, 0.ToString().TryParseSbyte());
        Assert.Equal(default, string.Empty.TryParseSbyte());
        Assert.Equal(1, string.Empty.TryParseSbyte(1));
    }

    [Fact]
    public void TryParseByteTest()
    {
        Assert.Equal(byte.MaxValue, byte.MaxValue.ToString().TryParseByte());
        Assert.Equal(byte.MinValue, byte.MinValue.ToString().TryParseByte());
        Assert.Equal(0, 0.ToString().TryParseByte());
        Assert.Equal(default, string.Empty.TryParseByte());
        Assert.Equal(1, string.Empty.TryParseByte(1));
    }

    [Fact]
    public void TryParseShortTest()
    {
        Assert.Equal(short.MaxValue, short.MaxValue.ToString().TryParseShort());
        Assert.Equal(short.MinValue, short.MinValue.ToString().TryParseShort());
        Assert.Equal(0, 0.ToString().TryParseShort());
        Assert.Equal(default, string.Empty.TryParseShort());
        Assert.Equal(1, string.Empty.TryParseShort(1));
    }

    [Fact]
    public void TryParseUshortTest()
    {
        Assert.Equal(ushort.MaxValue, ushort.MaxValue.ToString().TryParseUshort());
        Assert.Equal(ushort.MinValue, ushort.MinValue.ToString().TryParseUshort());
        Assert.Equal(0, 0.ToString().TryParseUshort());
        Assert.Equal(default, string.Empty.TryParseUshort());
        Assert.Equal(1, string.Empty.TryParseUshort(1));
    }

    [Fact]
    public void TryParseIntTest()
    {
        Assert.Equal(int.MaxValue, int.MaxValue.ToString().TryParseInt());
        Assert.Equal(int.MinValue, int.MinValue.ToString().TryParseInt());
        Assert.Equal(0, 0.ToString().TryParseInt());
        Assert.Equal(default, string.Empty.TryParseInt());
        Assert.Equal(1, string.Empty.TryParseInt(1));
    }

    [Fact]
    public void TryParseUintTest()
    {
        Assert.Equal(uint.MaxValue, uint.MaxValue.ToString().TryParseUint());
        Assert.Equal(uint.MinValue, uint.MinValue.ToString().TryParseUint());
        Assert.Equal(0U, 0.ToString().TryParseUint());
        Assert.Equal(default, string.Empty.TryParseUint());
        Assert.Equal(1U, string.Empty.TryParseUint(1));
    }

    [Fact]
    public void TryParseLongTest()
    {
        Assert.Equal(long.MaxValue, long.MaxValue.ToString().TryParseLong());
        Assert.Equal(long.MinValue, long.MinValue.ToString().TryParseLong());
        Assert.Equal(0L, 0.ToString().TryParseLong());
        Assert.Equal(default, string.Empty.TryParseLong());
        Assert.Equal(1L, string.Empty.TryParseLong(1));
    }

    [Fact]
    public void TryParseUlongTest()
    {
        Assert.Equal(ulong.MaxValue, ulong.MaxValue.ToString().TryParseUlong());
        Assert.Equal(ulong.MinValue, ulong.MinValue.ToString().TryParseUlong());
        Assert.Equal(0UL, 0.ToString().TryParseUlong());
        Assert.Equal(default, string.Empty.TryParseUlong());
        Assert.Equal(1UL, string.Empty.TryParseUlong(1));
    }

    [Fact]
    public void TryParseFloatTest()
    {
#if NET48
        Assert.Equal(float.MaxValue, float.MaxValue.ToString("R").TryParseFloat());
        Assert.Equal(float.MinValue, float.MinValue.ToString("R").TryParseFloat());
#else
        Assert.Equal(
            float.MaxValue,
            float.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseFloat()
        );
        Assert.Equal(
            float.MinValue,
            float.MinValue.ToString(CultureInfo.InvariantCulture).TryParseFloat()
        );
#endif
        Assert.Equal(0F, 0.ToString().TryParseFloat());
        Assert.Equal(default, string.Empty.TryParseFloat());
        Assert.Equal(1F, string.Empty.TryParseFloat(1));
    }

    [Fact]
    public void TryParseDoubleTest()
    {
#if NET48
        Assert.Equal(double.MaxValue, double.MaxValue.ToString("R").TryParseDouble());
        Assert.Equal(double.MinValue, double.MinValue.ToString("R").TryParseDouble());
#else
        Assert.Equal(
            double.MaxValue,
            double.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseDouble()
        );
        Assert.Equal(
            double.MinValue,
            double.MinValue.ToString(CultureInfo.InvariantCulture).TryParseDouble()
        );
#endif
        Assert.Equal(0D, 0.ToString().TryParseDouble());
        Assert.Equal(default, string.Empty.TryParseDouble());
        Assert.Equal(1D, string.Empty.TryParseDouble(1));
    }

    [Fact]
    public void TryParseDecimalTest()
    {
        Assert.Equal(
            decimal.MaxValue,
            decimal.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseDecimal()
        );
        Assert.Equal(
            decimal.MinValue,
            decimal.MinValue.ToString(CultureInfo.InvariantCulture).TryParseDecimal()
        );
        Assert.Equal(0M, 0.ToString().TryParseDecimal());
        Assert.Equal(default, string.Empty.TryParseDecimal());
        Assert.Equal(1M, string.Empty.TryParseDecimal(1));
    }

    [Fact]
    public void TryParseBoolTest()
    {
        Assert.True(true.ToString().TryParseBool());
        Assert.False(false.ToString().TryParseBool());
        Assert.False(0.ToString().TryParseBool());
        Assert.False(string.Empty.TryParseBool());
        Assert.True(string.Empty.TryParseBool(true));
    }

    [Fact]
    public void TryParseDateTimeTest()
    {
        var maxDateTime = new DateTime(9999, 12, 31, 23, 59, 59);
        var minDateTime = new DateTime(0001, 01, 01, 00, 00, 00);
        Assert.Equal(
            maxDateTime,
            maxDateTime.ToString(CultureInfo.InvariantCulture).TryParseDateTime()
        );
        Assert.Equal(
            minDateTime,
            minDateTime.ToString(CultureInfo.InvariantCulture).TryParseDateTime()
        );
        Assert.Equal(default, string.Empty.TryParseDateTime());
        Assert.Equal(maxDateTime, string.Empty.TryParseDateTime(maxDateTime));
        Assert.Equal(minDateTime, string.Empty.TryParseDateTime(minDateTime));
    }

    [Fact]
    public void TryParseDateTimeOffsetTest()
    {
        var maxDateTimeOffset = new DateTimeOffset(9999, 12, 31, 23, 59, 59, TimeSpan.Zero);
        var minDateTimeOffset = new DateTimeOffset(0001, 01, 01, 00, 00, 00, TimeSpan.Zero);
        Assert.Equal(maxDateTimeOffset, maxDateTimeOffset.ToString().TryParseDateTimeOffset());
        Assert.Equal(minDateTimeOffset, minDateTimeOffset.ToString().TryParseDateTimeOffset());
        Assert.Equal(default, string.Empty.TryParseDateTimeOffset());
        Assert.Equal(maxDateTimeOffset, string.Empty.TryParseDateTimeOffset(maxDateTimeOffset));
        Assert.Equal(minDateTimeOffset, string.Empty.TryParseDateTimeOffset(minDateTimeOffset));
    }

    [Fact]
    public void TryParseEnumTest() =>
        Assert.Equal(TestEnum.Create, TestEnum.Create.ToString().TryParseEnum<TestEnum>());

    #endregion

    #region Base64

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void Base64Test(string value)
    {
        Assert.Equal(value, value.ToBase64String().DecodeBase64());
        Assert.Equal(value.ToBase64Bytes(), value.ToBase64String().GetBytes());
        Assert.True(
            TestHelper.BytesEqual(value.GetBytes(), value.ToBase64String().FromBase64ToBytes())
        );
    }

    #endregion

    #region Truncate

    [Fact]
    public void Truncate_NullInput_ReturnsNull()
    {
        string? input = null;
        var result = input.Truncate(5);
        Assert.Null(result);
    }

    [Fact]
    public void Truncate_EmptyInput_ReturnsEmptyString()
    {
        var input = string.Empty;
        var result = input.Truncate(5);
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Truncate_MaxLengthLessThanOrEqualToZero_ThrowsArgumentException(int maxLength)
    {
        var input = "Test";
        Assert.Throws<ArgumentException>(() => input.Truncate(maxLength));
    }

    [Fact]
    public void Truncate_InputLengthLessThanOrEqualToMaxLength_ReturnsInput()
    {
        var input = "Hello";
        var result = input.Truncate(5);
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Truncate_SuffixLengthGreaterThanOrEqualToMaxLength_ReturnsTruncatedSuffix()
    {
        var input = "Hello, World!";
        var result = input.Truncate(3);
        Assert.Equal("...", result);
    }

    [Fact]
    public void Truncate_InputLengthGreaterThanMaxLength_ReturnsTruncatedInputWithSuffix()
    {
        var input = "Hello, World!";
        var result = input.Truncate(8);
        Assert.Equal("Hello...", result);
    }

    [Fact]
    public void Truncate_CustomSuffix_ReturnsTruncatedInputWithCustomSuffix()
    {
        var input = "Hello, World!";
        var result = input.Truncate(9, "--");
        Assert.Equal("Hello, --", result);
    }

    #endregion

    #region stream

    [Fact]
    public void ToMemoryStreamTest()
    {
        const string str = "Alice";
        var ms = str.ToMemoryStream();
        var result = ms.ReadString();
        Assert.Equal(str, result);
    }

    [Fact]
    public void WriteToTest()
    {
        const string str = "Alice";
        var ms = new MemoryStream();
        str.WriteTo(ms);
        var result = ms.ReadString();
        Assert.Equal(str, result);
    }

    [Fact]
    public async Task WriteToAsyncTest()
    {
        const string str = "Alice";
        var ms = new MemoryStream();
        await str.WriteToAsync(ms);
        var result = await ms.ReadStringAsync();
        Assert.Equal(str, result);
    }

    [Fact]
    public void TryWriteToTest()
    {
        const string str = "Alice";
        var ms = new MemoryStream();
        str.TryWriteTo(ms);
        var result = ms.ReadString();
        Assert.Equal(str, result);
    }

    [Fact]
    public async Task TryWriteToTestAsync()
    {
        const string str = "Alice";
        var ms = new MemoryStream();
        await str.TryWriteToAsync(ms);
        var result = await ms.ReadStringAsync();
        Assert.Equal(str, result);
    }

    #endregion

    [Theory]
    [InlineData("123,3234.aada：asdhaslkd", "1233234aadaasdhaslkd")]
    public void GetLetterOrDigitTest(string first, string second) =>
        Assert.Equal(first.FilterLettersAndDigits(), second);

    [Theory]
    [InlineData("abcdefg", "a", "1", "1bcdefg")]
    [InlineData("     ", " ", "", "")]
    public void TryReplaceTest(string str, string first, string second, string result) =>
        Assert.Equal(str.SafeReplace(first, second), result);

    [Theory]
    [InlineData("1F3870BE274F6C49B3E31A0C6728957F")]
    public void HexTest(string hexString)
    {
        var bytes = hexString.FromHex();
        var result = bytes.ToHexString();
        Assert.Equal(hexString, result);
    }

    [Theory]
    [InlineData("apple")]
    public void HexBytesTest(string str)
    {
        var hexString = str.ToHexString();

        var hexBytes = hexString.FromHex();
        var result = hexBytes.ToHexString();
        Assert.Equal(hexString, result);
    }

    [Theory]
    [InlineData("apple")]
    public void HexStringTest(string str)
    {
        var hexString = str.ToHexString();
        var result = hexString.FromHexToString();
        Assert.Equal(str, result);
    }
}
