namespace NotificationProvider.Domain.Constants;

/// <summary>
/// Constants helpful to define validation rules using FluentValidation.
/// </summary>
public static class FluentValidationConstants
{
    /// <summary>
    /// Types that are not auto validated by FluentValidation.
    /// </summary>
    public static readonly IReadOnlyList<Type> TypesExcludedFromAutoValidation =
    [
        typeof(string),
        typeof(int),
        typeof(long),
        typeof(decimal),
        typeof(double),
        typeof(float),
        typeof(DateTime),
        typeof(DateOnly),
        typeof(DateTimeOffset),
        typeof(Guid),
        typeof(bool)
    ];
}