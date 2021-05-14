namespace Zaabee.Extensions
{
    public static partial class ZaabeeExtension
    {
        public static bool IsNull<T>(this T? param) where T : struct => param is null;

        public static bool IsNotNull<T>(this T? param) where T : struct => param is not null;

        public static bool IsNullOrDefault<T>(this T? param) where T : struct => param is null || param.Equals(default);

        public static bool IsNull<T>(this T param) where T : class => param is null;

        public static bool IsNotNull<T>(this T param) where T : class => param is not null;

        public static bool IsNullOrDefault<T>(this T param) where T : class => param is null || param.Equals(default);

        public static T TryGetValue<T>(this T? param) where T : struct => param ?? default;
    }
}