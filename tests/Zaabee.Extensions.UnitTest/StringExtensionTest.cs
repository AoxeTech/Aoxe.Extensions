using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;
using Zaabee.Extensions.Commons;

namespace Zaabee.Extensions.UnitTest
{
    public class StringExtensionTest
    {
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
            var stringList = new List<string> {"Alice", "Bob", "Carol", "Dave", "Eve"};
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
            Assert.True(BytesEqual(str.ToUtf8Bytes(), Encoding.UTF8.GetBytes(str)));

        [Theory]
        [InlineData("Alice")]
        [InlineData("Bob")]
        [InlineData("Carol")]
        [InlineData("Dave")]
        [InlineData("Eve")]
        public void ToAsciiBytesTest(string str) =>
            Assert.True(BytesEqual(str.ToAsciiBytes(), Encoding.ASCII.GetBytes(str)));

        [Theory]
        [InlineData("Alice")]
        [InlineData("Bob")]
        [InlineData("Carol")]
        [InlineData("Dave")]
        [InlineData("Eve")]
        public void ToBigEndianUnicodeBytesTest(string str) =>
            Assert.True(BytesEqual(str.ToBigEndianUnicodeBytes(), Encoding.BigEndianUnicode.GetBytes(str)));

        [Theory]
        [InlineData("Alice")]
        [InlineData("Bob")]
        [InlineData("Carol")]
        [InlineData("Dave")]
        [InlineData("Eve")]
        public void ToDefaultBytesTest(string str) =>
            Assert.True(BytesEqual(str.ToDefaultBytes(), Encoding.Default.GetBytes(str)));

        [Theory]
        [InlineData("Alice")]
        [InlineData("Bob")]
        [InlineData("Carol")]
        [InlineData("Dave")]
        [InlineData("Eve")]
        public void ToUtf32BytesTest(string str) =>
            Assert.True(BytesEqual(str.ToUtf32Bytes(), Encoding.UTF32.GetBytes(str)));

        [Theory]
        [InlineData("Alice")]
        [InlineData("Bob")]
        [InlineData("Carol")]
        [InlineData("Dave")]
        [InlineData("Eve")]
        public void ToUnicodeBytesTest(string str) =>
            Assert.True(BytesEqual(str.ToUnicodeBytes(), Encoding.Unicode.GetBytes(str)));

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
        public void ParseFloatTest(float value) => Assert.Equal(value, value.ToString(CultureInfo.InvariantCulture).ParseFloat());

        [Theory]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        public void ParseDoubleTest(double value) => Assert.Equal(value, value.ToString(CultureInfo.InvariantCulture).ParseDouble());
        
        [Fact]
        public void ParseDecimalTest()
        {
            Assert.Equal(decimal.MaxValue, decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ParseDecimal());
            Assert.Equal(decimal.MinValue, decimal.MinValue.ToString(CultureInfo.InvariantCulture).ParseDecimal());
            Assert.Equal(0, 0.ToString().ParseDecimal());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ParseBoolTest(bool value) => Assert.Equal(value, value.ToString().ParseBool());

        [Fact]
        public void ParseDateTimeTest()
        {
            var maxDateTime = new DateTime(9999,12,31,23,59,59);
            var minDateTime = new DateTime(0001,01,01,00,00,00);
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
        public void ParseEnumTest() =>
            Assert.Equal(TestEnum.Create, TestEnum.Create.ToString().ParseEnum(typeof(TestEnum)));

        #endregion

        #region TryParse

        [Fact]
        public void TryParseSbyteTest()
        {
            Assert.Equal(sbyte.MaxValue, sbyte.MaxValue.ToString().TryParseSbyte());
            Assert.Equal(sbyte.MinValue, sbyte.MinValue.ToString().TryParseSbyte());
            Assert.Equal(0, 0.ToString().TryParseSbyte());
            Assert.Equal(0, (sbyte.MaxValue + 1).ToString().TryParseSbyte());
            Assert.Equal(0, (sbyte.MinValue - 1).ToString().TryParseSbyte());
        }

        [Fact]
        public void TryParseByteTest()
        {
            Assert.Equal(byte.MaxValue, byte.MaxValue.ToString().TryParseByte());
            Assert.Equal(byte.MinValue, byte.MinValue.ToString().TryParseByte());
            Assert.Equal(0, 0.ToString().TryParseByte());
            Assert.Equal(0, (byte.MaxValue + 1).ToString().TryParseByte());
            Assert.Equal(0, (byte.MinValue - 1).ToString().TryParseByte());
        }

        [Fact]
        public void TryParseShortTest()
        {
            Assert.Equal(short.MaxValue, short.MaxValue.ToString().TryParseShort());
            Assert.Equal(short.MinValue, short.MinValue.ToString().TryParseShort());
            Assert.Equal(0, 0.ToString().TryParseShort());
            Assert.Equal(0, (short.MaxValue + 1).ToString().TryParseShort());
            Assert.Equal(0, (short.MinValue - 1).ToString().TryParseShort());
        }

        [Fact]
        public void TryParseUshortTest()
        {
            Assert.Equal(ushort.MaxValue, ushort.MaxValue.ToString().TryParseUshort());
            Assert.Equal(ushort.MinValue, ushort.MinValue.ToString().TryParseUshort());
            Assert.Equal(0, 0.ToString().TryParseUshort());
            Assert.Equal(0, (ushort.MaxValue + 1).ToString().TryParseUshort());
            Assert.Equal(0, (ushort.MinValue - 1).ToString().TryParseUshort());
        }

        [Fact]
        public void TryParseIntTest()
        {
            Assert.Equal(int.MaxValue, int.MaxValue.ToString().TryParseInt());
            Assert.Equal(int.MinValue, int.MinValue.ToString().TryParseInt());
            Assert.Equal(0, 0.ToString().TryParseInt());
            Assert.Equal(0, (int.MaxValue + 1L).ToString().TryParseInt());
            Assert.Equal(0, (int.MinValue - 1L).ToString().TryParseInt());
        }

        [Fact]
        public void TryParseUintTest()
        {
            Assert.Equal(uint.MaxValue, uint.MaxValue.ToString().TryParseUint());
            Assert.Equal(uint.MinValue, uint.MinValue.ToString().TryParseUint());
            Assert.Equal(0U, 0.ToString().TryParseUint());
            Assert.Equal(0U, (uint.MaxValue + 1L).ToString().TryParseUint());
            Assert.Equal(0U, (uint.MinValue - 1L).ToString().TryParseUint());
        }

        [Fact]
        public void TryParseLongTest()
        {
            Assert.Equal(long.MaxValue, long.MaxValue.ToString().TryParseLong());
            Assert.Equal(long.MinValue, long.MinValue.ToString().TryParseLong());
            Assert.Equal(0L, 0.ToString().TryParseLong());
            Assert.Equal(0L, (long.MaxValue + 1M).ToString(CultureInfo.InvariantCulture).TryParseLong());
            Assert.Equal(0L, (long.MinValue - 1M).ToString(CultureInfo.InvariantCulture).TryParseLong());
        }

        [Fact]
        public void TryParseUlongTest()
        {
            Assert.Equal(ulong.MaxValue, ulong.MaxValue.ToString().TryParseUlong());
            Assert.Equal(ulong.MinValue, ulong.MinValue.ToString().TryParseUlong());
            Assert.Equal(0M, 0.ToString().TryParseUlong());
            Assert.Equal(0M, (ulong.MaxValue + 1M).ToString(CultureInfo.InvariantCulture).TryParseUlong());
            Assert.Equal(0M, (ulong.MinValue - 1M).ToString(CultureInfo.InvariantCulture).TryParseUlong());
        }

        [Fact]
        public void TryParseFloatTest()
        {
            Assert.Equal(float.MaxValue, float.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseFloat());
            Assert.Equal(float.MinValue, float.MinValue.ToString(CultureInfo.InvariantCulture).TryParseFloat());
            Assert.Equal(0F, 0.ToString().TryParseFloat());
            Assert.Equal(0F, string.Empty.ToString(CultureInfo.InvariantCulture).TryParseFloat());
        }

        [Fact]
        public void TryParseDoubleTest()
        {
            Assert.Equal(double.MaxValue, double.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseDouble());
            Assert.Equal(double.MinValue, double.MinValue.ToString(CultureInfo.InvariantCulture).TryParseDouble());
            Assert.Equal(0D, 0.ToString().TryParseDouble());
            Assert.Equal(0D, string.Empty.ToString(CultureInfo.InvariantCulture).TryParseDouble());
        }

        [Fact]
        public void TryParseDecimalTest()
        {
            Assert.Equal(decimal.MaxValue, decimal.MaxValue.ToString(CultureInfo.InvariantCulture).TryParseDecimal());
            Assert.Equal(decimal.MinValue, decimal.MinValue.ToString(CultureInfo.InvariantCulture).TryParseDecimal());
            Assert.Equal(0M, 0.ToString().TryParseDecimal());
            Assert.Equal(0M, string.Empty.TryParseDecimal());
        }

        [Fact]
        public void TryParseBoolTest()
        {
            Assert.True(true.ToString().TryParseBool());
            Assert.False(false.ToString().TryParseBool());
            Assert.False(0.ToString().TryParseBool());
        }

        [Fact]
        public void TryParseDateTimeTest()
        {
            var maxDateTime = new DateTime(9999,12,31,23,59,59);
            var minDateTime = new DateTime(0001,01,01,00,00,00);
            Assert.Equal(maxDateTime, maxDateTime.ToString(CultureInfo.InvariantCulture).TryParseDateTime());
            Assert.Equal(minDateTime, minDateTime.ToString(CultureInfo.InvariantCulture).TryParseDateTime());
            Assert.Equal(default, string.Empty.TryParseDateTime());
        }

        [Fact]
        public void TryParseDateTimeOffsetTest()
        {
            var maxDateTimeOffset = new DateTimeOffset(9999, 12, 31, 23, 59, 59, TimeSpan.Zero);
            var minDateTimeOffset = new DateTimeOffset(0001, 01, 01, 00, 00, 00, TimeSpan.Zero);
            Assert.Equal(maxDateTimeOffset, maxDateTimeOffset.ToString().TryParseDateTimeOffset());
            Assert.Equal(minDateTimeOffset, minDateTimeOffset.ToString().TryParseDateTimeOffset());
            Assert.Equal(default, string.Empty.TryParseDateTimeOffset());
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
            Assert.Equal(value,value.ToBase64String().FromBase64ToString());
            Assert.Equal(value.ToBase64Bytes(),value.ToBase64String().ToBytes());
            Assert.True(BytesEqual(value.ToBytes(),value.ToBase64String().FromBase64ToBytes()));
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
            Assert.Equal(0,"".ToInt(numerationSystem));
            Assert.Throws<ArgumentException>(()=>"!@#".ToInt(numerationSystem));
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
            Assert.Equal(0,"".ToLong(numerationSystem));
            Assert.Throws<ArgumentException>(()=>"!@#".ToLong(numerationSystem));
        }

        private static bool BytesEqual(byte[] first, byte[] second)
        {
            if (first == null || second == null) return false;
            if (first.Length != second.Length) return false;
            return !first.Where((t, i) => t != second[i]).Any();
        }
    }
}