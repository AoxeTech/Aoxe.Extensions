namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static T CastTo<T>(this object obj) =>
        (T)obj;
}