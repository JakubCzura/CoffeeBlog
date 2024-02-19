using CoffeeBlog.Application.Dtos.Authentication;
using System.Security.Claims;

namespace CoffeeBlog.Application.Interfaces.Security.Authentication;

public interface IJwtService
{
    public string CreateToken(CreateJwtTokenUserDetailsDto createJwtTokenUserDetailsDto,
                              IEnumerable<string>? roles = null,
                              IEnumerable<Claim>? claims = null);
}