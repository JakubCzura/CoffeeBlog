namespace CoffeeBlog.Domain.Models.Users;


/// <summary>
/// Information about current signed in user.
/// </summary>
/// <param name="Id">User's id.</param>
/// <param name="Username">User's username.</param>
/// <param name="Email">User's email.</param>
public record CurrentAuthorizedUser(int Id, string Username, string Email);