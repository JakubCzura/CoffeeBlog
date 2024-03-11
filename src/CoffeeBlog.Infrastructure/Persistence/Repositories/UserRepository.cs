using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class UserRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<User>(coffeeBlogDbContext), IUserRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = coffeeBlogDbContext;

    public async Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                                       CancellationToken cancellationToken)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Email == usernameOrEmail || x.Username == usernameOrEmail, cancellationToken);
    public async Task<bool> UsernameExistsAsync(string username,
                                                CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .AnyAsync(x => x.Username == username, cancellationToken);
    public async Task<bool> EmailExistsAsync(string email,
                                             CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .AnyAsync(x => x.Email == email, cancellationToken);
}