namespace CoffeeBlog.Application.ExtensionMethods.String;

public static class StringExtensions
{
    public static string ToCamelCase(this string? value)
        => string.IsNullOrWhiteSpace(value) ? string.Empty : char.ToLowerInvariant(value[0]) + value[1..];
}