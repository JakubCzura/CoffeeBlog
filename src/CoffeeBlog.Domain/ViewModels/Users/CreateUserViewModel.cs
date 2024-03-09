namespace CoffeeBlog.Domain.ViewModels.Users;

public record CreateUserViewModel(int UserId,
                                  string Username,
                                  string Email,
                                  string JwtToken);