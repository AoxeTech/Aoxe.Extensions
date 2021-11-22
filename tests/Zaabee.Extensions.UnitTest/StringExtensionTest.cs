namespace Zaabee.Extensions.UnitTest;

public class StringExtensionTest
{
    [Fact]
    public void TrimStart()
    {
        const string target = "apple,banana,pear";
        const string trimString = "apple";
        Assert.Equal(",banana,pear", target.TrimStart(trimString));
        Assert.Equal(target, target.TrimStart(string.Empty));
    }

    [Fact]
    public void TrimEnd()
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
    public void IsNullOrEmptyTest(string str) =>
        Assert.Equal(str.IsNullOrEmpty(), string.IsNullOrEmpty(str));

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Alice")]
    public void IsNullOrWhiteSpaceTest(string str) =>
        Assert.Equal(str.IsNullOrWhiteSpace(), string.IsNullOrWhiteSpace(str));

    [Theory]
    [InlineData(" ")]
    [InlineData(",")]
    [InlineData(" or ")]
    public void StringJoinTest(string separator)
    {
        var stringList = new List<string> { "Alice", "Bob", "Carol", "Dave", "Eve" };
        Assert.Equal(stringList.StringJoin(separator), string.Join(separator, stringList));
    }

    #region Bytes

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void ToUtf8BytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.ToUtf8Bytes(), Encoding.UTF8.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void ToAsciiBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.ToAsciiBytes(), Encoding.ASCII.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void ToBigEndianUnicodeBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.ToBigEndianUnicodeBytes(), Encoding.BigEndianUnicode.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void ToDefaultBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.ToDefaultBytes(), Encoding.Default.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void ToUtf32BytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.ToUtf32Bytes(), Encoding.UTF32.GetBytes(str)));

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Carol")]
    [InlineData("Dave")]
    [InlineData("Eve")]
    public void ToUnicodeBytesTest(string str) =>
        Assert.True(TestHelper.BytesEqual(str.ToUnicodeBytes(), Encoding.Unicode.GetBytes(str)));

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
    public void ParseUshortTest(ushort value) => Assert.Equal(value, value.ToString().ParseUshort());

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
        Assert.Equal(decimal.MaxValue, decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ParseDecimal());
        Assert.Equal(decimal.MinValue, decimal.MinValue.ToString(CultureInfo.InvariantCulture).ParseDecimal());
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
        Assert.Equal(maxDateTime, maxDateTime.ToString(CultureInfo.InvariantCulture).ParseDateTime());
        Assert.Equal(minDateTime, minDateTime.ToString(CultureInfo.InvariantCulture).ParseDateTime());
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
        Assert.Equal(TestEnum.Create, TestEnum.Create.ToString().ToLower().ParseEnum(typeof(TestEnum), true));
        Assert.Throws<ArgumentException>(() => TestEnum.Create.ToString().ToLower().ParseEnum(typeof(TestEnum)));
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
#if  NET48
        Assert.Equal(float.MaxValue, float.MaxValue.ToString("R").TryParseFloat());
        Assert.Equal(float.MinValue, float.MinValue.ToString("R").TryParseFloat());
#else
            Assert.Equal(float.MaxValue, float.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseFloat());
            Assert.Equal(float.MinValue, float.MinValue.ToString(CultureInfo.InvariantCulture).TryParseFloat());
#endif
        Assert.Equal(0F, 0.ToString().TryParseFloat());
        Assert.Equal(default, string.Empty.TryParseFloat());
        Assert.Equal(1F, string.Empty.TryParseFloat(1));
    }

    [Fact]
    public void TryParseDoubleTest()
    {
#if  NET48
        Assert.Equal(double.MaxValue, double.MaxValue.ToString("R").TryParseDouble());
        Assert.Equal(double.MinValue, double.MinValue.ToString("R").TryParseDouble());
#else
            Assert.Equal(double.MaxValue, double.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseDouble());
            Assert.Equal(double.MinValue, double.MinValue.ToString(CultureInfo.InvariantCulture).TryParseDouble());
#endif
        Assert.Equal(0D, 0.ToString().TryParseDouble());
        Assert.Equal(default, string.Empty.TryParseDouble());
        Assert.Equal(1D, string.Empty.TryParseDouble(1));
    }

    [Fact]
    public void TryParseDecimalTest()
    {
        Assert.Equal(decimal.MaxValue, decimal.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseDecimal());
        Assert.Equal(decimal.MinValue, decimal.MinValue.ToString(CultureInfo.InvariantCulture).TryParseDecimal());
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
        Assert.Equal(maxDateTime, maxDateTime.ToString(CultureInfo.InvariantCulture).TryParseDateTime());
        Assert.Equal(minDateTime, minDateTime.ToString(CultureInfo.InvariantCulture).TryParseDateTime());
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
        Assert.Equal(value, value.ToBase64String().FromBase64ToString());
        Assert.Equal(value.ToBase64Bytes(), value.ToBase64String().ToBytes());
        Assert.True(TestHelper.BytesEqual(value.ToBytes(), value.ToBase64String().FromBase64ToBytes()));
    }

    #endregion

    #region Format

    [Theory]
    [InlineData("{0:D3}", 2, "002")]
    [InlineData("{0:G}", 2, "2")]
    [InlineData("{0:N}", 250000, "250,000.00")]
    [InlineData("{0:X000}", 12, "C")]
    [InlineData("{0:000.000}", 12.2, "012.200")]
    [InlineData("Hello {0}", "John", "Hello John")]
    public void FormatTestWithOneArg(string format, object arg0, string result)
    {
        Assert.Equal(result, format.Format(arg0));
    }

    [Theory]
    [InlineData("{0} {1}", "Hello", "John", "Hello John")]
    public void FormatTestWithTwoArgs(string format, object arg0, object arg1, string result)
    {
        Assert.Equal(result, format.Format(arg0, arg1));
    }

    #endregion

    [Theory]
    [InlineData("123,3234.aada：asdhaslkd", "1233234aadaasdhaslkd")]
    public void GetLetterOrDigitTest(string first, string second) =>
        Assert.Equal(first.GetLetterOrDigit(), second);


    [Theory]
    [InlineData("abcdefg", "a", "1", "1bcdefg")]
    [InlineData("     ", " ", "", "")]
    public void TryReplaceTest(string str, string first, string second, string result) =>
        Assert.Equal(str.TryReplace(first, second), result);

    [Theory]
    [InlineData(int.MaxValue, NumerationSystem.Binary)]
    [InlineData(int.MaxValue, NumerationSystem.Decimalism)]
    [InlineData(int.MaxValue, NumerationSystem.Duotricemary)]
    [InlineData(int.MaxValue, NumerationSystem.Hexadecimal)]
    [InlineData(int.MaxValue, NumerationSystem.Octal)]
    [InlineData(int.MaxValue, NumerationSystem.SixtyTwoAry)]
    [InlineData(int.MaxValue, NumerationSystem.ThirtySixAry)]
    [InlineData(int.MinValue + 1, NumerationSystem.Binary)]
    [InlineData(int.MinValue + 1, NumerationSystem.Decimalism)]
    [InlineData(int.MinValue + 1, NumerationSystem.Duotricemary)]
    [InlineData(int.MinValue + 1, NumerationSystem.Hexadecimal)]
    [InlineData(int.MinValue + 1, NumerationSystem.Octal)]
    [InlineData(int.MinValue + 1, NumerationSystem.SixtyTwoAry)]
    [InlineData(int.MinValue + 1, NumerationSystem.ThirtySixAry)]
    public void ToIntTest(int value, NumerationSystem numerationSystem)
    {
        var str = value.ToString(numerationSystem);
        var result = str.ToInt(numerationSystem);
        Assert.Equal(value, result);
        Assert.Equal(0, "".ToInt(numerationSystem));
        Assert.Throws<ArgumentException>(() => "!@#".ToInt(numerationSystem));
    }

    [Theory]
    [InlineData(long.MaxValue, NumerationSystem.Binary)]
    [InlineData(long.MaxValue, NumerationSystem.Decimalism)]
    [InlineData(long.MaxValue, NumerationSystem.Duotricemary)]
    [InlineData(long.MaxValue, NumerationSystem.Hexadecimal)]
    [InlineData(long.MaxValue, NumerationSystem.Octal)]
    [InlineData(long.MaxValue, NumerationSystem.SixtyTwoAry)]
    [InlineData(long.MaxValue, NumerationSystem.ThirtySixAry)]
    [InlineData(long.MinValue + 1, NumerationSystem.Binary)]
    [InlineData(long.MinValue + 1, NumerationSystem.Decimalism)]
    [InlineData(long.MinValue + 1, NumerationSystem.Duotricemary)]
    [InlineData(long.MinValue + 1, NumerationSystem.Hexadecimal)]
    [InlineData(long.MinValue + 1, NumerationSystem.Octal)]
    [InlineData(long.MinValue + 1, NumerationSystem.SixtyTwoAry)]
    [InlineData(long.MinValue + 1, NumerationSystem.ThirtySixAry)]
    public void ToLongTest(long value, NumerationSystem numerationSystem)
    {
        var str = value.ToString(numerationSystem);
        var result = str.ToLong(numerationSystem);
        Assert.Equal(value, result);
        Assert.Equal(0, "".ToLong(numerationSystem));
        Assert.Throws<ArgumentException>(() => "!@#".ToLong(numerationSystem));
    }
}