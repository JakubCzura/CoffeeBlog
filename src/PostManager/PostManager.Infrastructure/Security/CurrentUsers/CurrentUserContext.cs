using PostManager.Application.Interfaces.Security.CurrentUsers;
using PostManager.Domain.Exceptions;
using PostManager.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PostManager.Infrastructure.Security.CurrentUsers;

/// <summary>
/// Service to get information about current authorized user who is using the application.
/// </summary>
/// <param name="_httpContextAccessor">Interface for accessing the HTTP context which gives information about authenticated user.</param>
internal class CurrentUserContext(IHttpContextAccessor _httpContextAccessor) : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = _httpContextAccessor;

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