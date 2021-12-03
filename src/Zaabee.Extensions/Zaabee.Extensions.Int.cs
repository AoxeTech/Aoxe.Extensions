namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static IEnumerator<int> GetEnumerator(this int count) =>
        count.Range().GetEnumerator();

    public static IEnumerable<int> Range(this int count, int start = 0) =>
        Enumerable.Range(start, count);
}