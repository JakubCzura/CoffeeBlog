namespace CoffeeBlog.Application.Dtos.Authentication;

/// <summary>
/// Details for creating a JWT token.
/// </summary>
/// <param name="UserId">User's id.</param>
/// <param name="Username">User's username.</param>
/// <param name="Email">User's email.</param>
public record CreateJwtTokenDto(int UserId,
                                string Username,
                                string Email);