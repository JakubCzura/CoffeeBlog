using Mvc = Microsoft.AspNetCore.Mvc;

namespace AuthService.Application.ExtensionMethods.ActionContext;

/// <summary>
/// Extension methods for <see cref="Mvc.ActionContext"/>.
/// </summary>
public static class ActionContextExtensions
{
    /// <summary>
    /// Returns joined messages of errors as string.
    /// </summary>
    /// <param name="actionContext">Action context of request.</param>
    /// <param name="delimiter">Delimiter between messages.</param>
    /// <returns>Errors' joined messages as string.</returns>
    public static string GetJoinedErrorsMessages(this Mvc.ActionContext actionContext, char delimiter = ';')
        => string.Join(delimiter, actionContext.ModelState.Values.SelectMany(x => x.Errors)
                                                                 .Select(x => x.ErrorMessage));
}