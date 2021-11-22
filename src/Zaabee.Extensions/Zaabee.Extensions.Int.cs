namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static IEnumerator<int> GetEnumerator(this int dec) =>
        Enumerable.Range(0, dec).GetEnumerator();

    public static string ToString(this int dec, NumerationSystem numerationSystem) =>
        dec.ToString((int) numerationSystem);

    public static string ToString(this int dec, int fromBase)
    {
        var stack = new Stack<int>();
        var sb = new StringBuilder();

        if (dec < 0)
        {
            sb.Append('-');
            dec = Math.Abs(dec);
        }

        while (dec > 0)
        {
            stack.Push(dec % fromBase);
            dec /= fromBase;
        }

        while (stack.Count > 0) sb.Append(Consts.LetterAndDigit[stack.Pop()]);
        return sb.ToString();
    }
}