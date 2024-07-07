using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace ApiGateway.Infrastructure.Security.Authentication;

internal sealed class JwtAuthenticationConfiguration : IConfigureNamedOptions<AuthenticationOptions>
{
    public void Configure(string? name, AuthenticationOptions options)
        => Configure(options);

    public void Configure(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}