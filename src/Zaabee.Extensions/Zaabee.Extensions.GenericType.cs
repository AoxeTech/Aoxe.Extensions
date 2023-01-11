namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
#if NETSTANDARD2_0
    public static Dictionary<string, object> ConvertToDictionary<T>(this T obj) =>
#else
    public static Dictionary<string, object?> ConvertToDictionary<T>(this T obj) =>
#endif
        obj?.GetType()
            .GetProperties()
            .ToDictionary(p => p.Name, p => p.GetValue(obj, null))
        ??
#if NETSTANDARD2_0
        new Dictionary<string, object>();
#else
        new Dictionary<string, object?>();
#endif

    public static DataTable ConvertToDataTable<T>(this IEnumerable<T> data)
    {
        var properties = TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
    }
}