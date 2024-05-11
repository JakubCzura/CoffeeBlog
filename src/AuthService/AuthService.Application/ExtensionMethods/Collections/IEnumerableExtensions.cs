using FluentResults;

namespace AuthService.Application.ExtensionMethods.Collections;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Checks if any element in the collection is null, empty or whitespace.
    /// </summary>
    /// <param name="elements">Collection of strings.</param>
    /// <returns>True if any element in the collection is null, empty or whitespace, otherwise false.</returns>
    public static bool IsAnyElementNullOrWhiteSpace(this IEnumerable<string?> elements)
        => elements.Any(string.IsNullOrWhiteSpace);

    /// <summary>
    /// Returns joined messages of errors as string.
    /// </summary>
    /// <param name="errors">Collection of errors.</param>
    /// <param name="delimiter">Delimiter between messages.</param>
    /// <returns>Errors' joined messages as string.</returns>
    public static string GetJoinedMessages(this IEnumerable<IError> errors, char delimiter = ';')
        => string.Join(delimiter, errors.Select(error => error.Message));
}