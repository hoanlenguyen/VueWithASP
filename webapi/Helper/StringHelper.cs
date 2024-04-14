namespace webapi.Helper
{
    public static class StringHelper
    {
        public static bool IsNotNullOrEmpty(this string? value) => !string.IsNullOrEmpty(value);
    }
}
