using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace WebUserInterface.Handlers;

public class AuthTokenHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
                                                                 CancellationToken cancellationToken)
    {
        string accessToken = (await httpContextAccessor.HttpContext!.GetTokenAsync("access_token"))!;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}