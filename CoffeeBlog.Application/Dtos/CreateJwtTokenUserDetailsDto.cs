namespace CoffeeBlog.Application.Dtos;

public record CreateJwtTokenUserDetailsDto(int Id,
                                           string Email,
                                           string Username);