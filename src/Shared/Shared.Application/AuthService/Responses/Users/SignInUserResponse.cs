namespace Shared.Application.AuthService.Responses.Users;

/// <summary>
/// Response to return after user was signed in.
/// </summary>
public class SignInUserResponse
{
    public string JwtToken { get; set; } = string.Empty;
}