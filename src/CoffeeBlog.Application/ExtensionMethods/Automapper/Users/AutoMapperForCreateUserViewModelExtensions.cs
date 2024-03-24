using AutoMapper;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;

namespace CoffeeBlog.Application.ExtensionMethods.Automapper.Users;

/// <summary>
/// Extension methods for <see cref="IMapper"/>.
/// </summary>
public static class AutoMapperForCreateUserViewModelExtensions
{
    /// <summary>
    /// Maps <see cref="User"/> to <see cref="SignUpUserViewModel"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="SignUpUserViewModel"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="user">User entity.</param>
    /// <param name="jwtToken">JWT token for authorized user.</param>
    /// <returns>Instance of <see cref="SignUpUserViewModel"/></returns>
    public static SignUpUserViewModel Map<T>(this IMapper mapper,
                                             User user,
                                             string jwtToken) where T : SignUpUserViewModel
        => mapper.Map<SignUpUserViewModel>(user, opt =>
        {
            opt.Items[nameof(SignUpUserViewModel.JwtToken)] = jwtToken;
        });
}