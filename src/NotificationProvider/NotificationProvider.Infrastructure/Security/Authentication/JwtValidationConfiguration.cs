﻿using NotificationProvider.Application.ExtensionMethods.Collections;
using NotificationProvider.Domain.SettingsOptions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace NotificationProvider.Infrastructure.Security.Authentication;

/// <summary>
/// Configuration of JWT validation.
/// </summary>
/// <param name="_authenticationOptions">Settings for authentication.</param>
internal sealed class JwtValidationConfiguration(IOptions<AuthenticationOptions> _authenticationOptions) : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions = _authenticationOptions.Value;

    /// <summary>
    /// Configures JWT validation.
    /// </summary>
    /// <param name="name">The name of the options instance being configured.</param>
    /// <param name="options">The options instance to configure.</param>
    public void Configure(string? name,
                          JwtBearerOptions options)
        => Configure(options);

    /// <summary>
    /// Configures JWT validation.
    /// </summary>
    /// <param name="options">The options instance to configure.</param>
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = _authenticationOptions.Jwt.ValidateIssuer,
            ValidIssuer = _authenticationOptions.Jwt.Issuer,
            ValidateAudience = _authenticationOptions.Jwt.ValidateAudience,
            ValidAudience = _authenticationOptions.Jwt.Audience,
            ValidateLifetime = _authenticationOptions.Jwt.ValidateLifetime,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Jwt.SecretKey)),
            ValidateIssuerSigningKey = _authenticationOptions.Jwt.ValidateIssuerSigningKey
        };
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = context =>
            {
                string? userId = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                string? username = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                string? email = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                //Don't authorize user without valid user's details in JWT
                if (new string?[] { userId, username, email }.IsAnyElementNullOrWhiteSpace())
                {
                    context?.Fail("Unauthorized. User's claims had invalid value");
                    return Task.CompletedTask;
                }
                return Task.CompletedTask;
            }
        };
    }
}