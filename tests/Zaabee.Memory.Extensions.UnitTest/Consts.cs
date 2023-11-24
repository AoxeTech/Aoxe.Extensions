namespace Zaabee.Memory.Extensions.UnitTest;

public static class Consts
{
    internal static readonly char[] Chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    internal static readonly MemoryStream MemoryStream = new("zaabee".ToBase64Bytes());
}
