namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> collections)
    {
        switch (source)
        {
            case List<T> list:
                list.AddRange(collections);
                break;
            default:
            {
                foreach (var item in collections)
                    source.Add(item);
                break;
            }
        }
    }

    public static int IndexOf<T>(
        this IEnumerable<T?> source,
        T? value,
        IEqualityComparer<T?>? comparer = null
    )
    {
        var index = 0;
        comparer ??= EqualityComparer<T?>.Default; // or pass in as a parameter
        foreach (var item in source)
        {
            if (comparer.Equals(item, value))
                return index;
            index++;
        }

        return -1;
    }

    public static bool NotContains<T>(this IEnumerable<T> source, T item) => !source.Contains(item);

    public static List<T?> ToList<T>(this IEnumerable<T?> src, Func<T?, bool>? func) =>
        func is null ? src.ToList() : src.Where(func).ToList();

    public static void ForEach<T>(this IEnumerable<T?> src, Action<T?>? action)
    {
        if (action is null)
            return;
        foreach (var i in src)
            action(i);
    }

    public static IEnumerable<T?> ForEachLazy<T>(this IEnumerable<T?> src, Action<T?>? action)
    {
        if (action is null)
            return src;
        return src.Select(i =>
        {
            action(i);
            return i;
        });
    }

    public static DataTable ConvertToDataTable<T>(this IEnumerable<T> data)
    {
        var properties = TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table
                .Columns
                .Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = item is null ? DBNull.Value : prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
    }
}
