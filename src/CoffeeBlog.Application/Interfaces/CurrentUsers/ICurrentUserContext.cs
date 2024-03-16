using CoffeeBlog.Domain.Models.Users;

namespace CoffeeBlog.Application.Interfaces.CurrentUsers;

/// <summary>
/// Interface to deliver information about current signed in, authorized user.
/// </summary>
public interface ICurrentUserContext
{
    /// <summary>
    /// Returns information about current authorized user.
    /// </summary>
    /// <returns>Information about user who has successfully signed in and is authorized.</returns>
    public CurrentAuthorizedUser GetCurrentAuthorizedUser();
}