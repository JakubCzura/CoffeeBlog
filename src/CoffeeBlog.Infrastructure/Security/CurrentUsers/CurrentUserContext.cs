using CoffeeBlog.Application.Interfaces.Security.CurrentUsers;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CoffeeBlog.Infrastructure.Security.CurrentUsers;

internal class CurrentUserContext(IHttpContextAccessor _httpContextAccessor) : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = _httpContextAccessor;

    /// <exception cref="UserUnauthorizedException">When user is unauthorized.</exception>
    public CurrentAuthorizedUser GetCurrentAuthorizedUser()
    {
        ClaimsPrincipal? user = (_httpContextAccessor.HttpContext?.User) ?? throw new UserUnauthorizedException();

        if (user.Identity?.IsAuthenticated == false)
        {
            throw new UserUnauthorizedException();
        }

        int id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
        string username = user.FindFirstValue(ClaimTypes.Name)!;
        string email = user.FindFirstValue(ClaimTypes.Email)!;

        return new CurrentAuthorizedUser(id, username, email);
    }
}