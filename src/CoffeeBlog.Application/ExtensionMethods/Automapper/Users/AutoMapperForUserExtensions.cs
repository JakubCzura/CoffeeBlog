using AutoMapper;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.ExtensionMethods.Automapper.Users;

/// <summary>
/// Extension methods for <see cref="IMapper"/>.
/// </summary>
public static class AutoMapperForUserExtensions
{
    /// <summary>
    /// Maps <see cref="SignUpUserCommand"/> to <see cref="User"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="SignUpUserCommand"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="createUserCommand">Request command to create a new user.</param>
    /// <param name="hashedPassword">User's password after it was hashed .</param>
    /// <returns>Instance of <see cref="User"/></returns>
    public static User Map<T>(this IMapper mapper,
                              SignUpUserCommand createUserCommand,
                              string hashedPassword) where T : User
        => mapper.Map<User>(createUserCommand, opt =>
        {
            opt.Items[MappingConstants.HashedPassword] = hashedPassword;
        });
}