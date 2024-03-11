using AutoMapper;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;

namespace CoffeeBlog.Application.ExtensionMethods.AutoMapper;

/// <summary>
/// Extension methods for <see cref="IMapper"/>.
/// </summary>
public static class AutoMapperExtensions
{
    public static CreateUserViewModel Map<T>(this IMapper mapper,
                                             User user,
                                             string jwtToken) where T : CreateUserViewModel
        => mapper.Map<CreateUserViewModel>(user, opt =>
        {
            opt.Items[nameof(CreateUserViewModel.JwtToken)] = jwtToken;
        });
}