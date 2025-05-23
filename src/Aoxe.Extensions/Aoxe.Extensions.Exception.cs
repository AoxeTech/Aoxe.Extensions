namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static Exception GetInmostException(this Exception ex)
    {
        if (ex is null)
            throw new ArgumentNullException(nameof(ex));
        if (ex.InnerException is null)
            return ex;
        var inmostException = ex.InnerException;
        while (inmostException.InnerException is not null)
            inmostException = inmostException.InnerException;
        return inmostException;
    }
}
