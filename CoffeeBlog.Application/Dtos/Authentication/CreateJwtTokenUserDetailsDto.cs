namespace CoffeeBlog.Application.Dtos.Authentication;

public record CreateJwtTokenUserDetailsDto(int Id,
                                           string Username,
                                           string Email);