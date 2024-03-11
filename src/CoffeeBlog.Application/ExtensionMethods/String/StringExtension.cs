namespace CoffeeBlog.Application.ExtensionMethods.String;

public static class StringExtension
{
    public static string ToCamelCase(this string value)
        => string.IsNullOrEmpty(value) ? value : char.ToLowerInvariant(value[0]) + value[1..];
}