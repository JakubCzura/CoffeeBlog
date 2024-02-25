using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

public class UserRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<UserEntity>(coffeeBlogDbContext), IUserRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = coffeeBlogDbContext;

    /// <summary>
    /// Returns a user by e-mail or username.
    /// </summary>
    /// <param name="usernameOrEmail">User's e-mail or username.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>User if found, otherwise null.</returns>
    public async Task<UserEntity?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                                             CancellationToken cancellationToken)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Email == usernameOrEmail || x.Username == usernameOrEmail, cancellationToken);

    /// <summary>
    /// Checks if given email and username are unique in database and they are not equal.
    /// <para>Usernames and e-mails are unique in database. Moreover there must not be any username equal to any e-mail as it can cause many issues.
    /// That's why email are compared with username in the query.</para>
    /// <para>If this method returns false then new user must not be created in database.</para>
    /// </summary>
    /// <param name="username">User's username.</param>
    /// <param name="email">User's e-mail.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>True if email and username are unique and different, otherwise false.</returns>
    public async Task<bool> AreUsernameAndEmailUniqueAndDifferentAsync(string username,
                                                                       string email,
                                                                       CancellationToken cancellationToken)
        => email != username && await _coffeeBlogDbContext.Users.AsNoTracking()
                                                                .AllAsync(x => x.Email != email
                                                                            && x.Email != username
                                                                            && x.Username != email
                                                                            && x.Username != username, cancellationToken);
}