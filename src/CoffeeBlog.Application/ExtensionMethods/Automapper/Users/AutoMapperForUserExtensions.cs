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
    /// Maps <see cref="CreateUserCommand"/> to <see cref="User"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="CreateUserCommand"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="createUserCommand">Request command to create a new user.</param>
    /// <param name="hashedPassword">User's password after it was hashed .</param>
    /// <param name="createdAt">Date and time when user is going to be created.</param>
    /// <returns></returns>
    public static User Map<T>(this IMapper mapper,
                              CreateUserCommand createUserCommand,
                              string hashedPassword,
                              DateTime createdAt) where T : User
        => mapper.Map<User>(createUserCommand, opt =>
        {
            opt.Items[MappingConstants.HashedPassword] = hashedPassword;
            opt.Items[MappingConstants.CreatedAt] = createdAt;
        });
}