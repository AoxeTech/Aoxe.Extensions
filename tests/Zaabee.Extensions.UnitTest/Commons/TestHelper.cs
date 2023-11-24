namespace Zaabee.Extensions.UnitTest.Commons;

internal static class TestHelper
{
    internal static bool BytesEqual(byte[]? first, byte[]? second)
    {
        if (first is null || second is null)
            return false;
        if (first.Length != second.Length)
            return false;
        return !first.Where((t, i) => t != second[i]).Any();
    }
}
