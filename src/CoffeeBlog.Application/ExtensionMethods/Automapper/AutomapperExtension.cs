using AutoMapper;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;

namespace CoffeeBlog.Application.ExtensionMethods.Automapper;

public static class AutomapperExtension
{
    public static CreateUserViewModel Map<T>(this IMapper mapper, User user, string jwtToken) where T : CreateUserViewModel
        => mapper.Map<CreateUserViewModel>(user, opt =>
        {
            opt.Items[nameof(CreateUserViewModel.JwtToken)] = jwtToken;
        });
}