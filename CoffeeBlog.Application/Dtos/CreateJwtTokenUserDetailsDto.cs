namespace CoffeeBlog.Application.Dtos;

public record CreateJwtTokenUserDetailsDto(int Id,
                                           string Username,
                                           string Email);