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
    /// Maps <see cref="User"/> to <see cref="CreateUserViewModel"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="CreateUserViewModel"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="user">User entity.</param>
    /// <param name="jwtToken">JWT token for authorized user.</param>
    /// <returns></returns>
    public static CreateUserViewModel Map<T>(this IMapper mapper,
                                             User user,
                                             string jwtToken) where T : CreateUserViewModel
        => mapper.Map<CreateUserViewModel>(user, opt =>
        {
            opt.Items[nameof(CreateUserViewModel.JwtToken)] = jwtToken;
        });
}