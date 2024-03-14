namespace CoffeeBlog.Domain.ViewModels.Users;

/// <summary>
/// View model to return after a new user was created.
/// </summary>
/// <param name="UserId">User's id.</param>
/// <param name="Username">User's username.</param>
/// <param name="Email">User's e-mail</param>
/// <param name="JwtToken">User's JWT token for authorization purposes.</param>
public record CreateUserViewModel(int UserId,
                                  string Username,
                                  string Email,
                                  string JwtToken);