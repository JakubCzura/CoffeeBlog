using AuthService.Domain.Entities;
using AutoMapper;
using Shared.Application.AuthService.Responses.Users;

namespace AuthService.Application.ExtensionMethods.Automapper.Users;

/// <summary>
/// Extension methods for <see cref="IMapper"/>.
/// </summary>
public static class AutoMapperForSignUpUserResponseExtensions
{
    /// <summary>
    /// Maps <see cref="User"/> to <see cref="SignUpUserResponse"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="SignUpUserResponse"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="user">User entity.</param>
    /// <param name="jwtToken">JWT token for authorized user.</param>
    /// <returns>Instance of <see cref="SignUpUserResponse"/></returns>
    public static SignUpUserResponse Map<T>(this IMapper mapper,
                                            User user,
                                            string jwtToken) where T : SignUpUserResponse
        => mapper.Map<SignUpUserResponse>(user, opt =>
        {
            opt.Items[nameof(SignUpUserResponse.JwtToken)] = jwtToken;
        });
}