namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static Exception GetInmostException(this Exception ex)
    {
        if (ex.InnerException is null) return ex;
        var inmostException = ex.InnerException;
        while (inmostException.InnerException is not null)
            inmostException = inmostException.InnerException;
        return inmostException;
    }
}