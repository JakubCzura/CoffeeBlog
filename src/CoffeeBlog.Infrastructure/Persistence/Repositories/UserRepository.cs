using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class UserRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<User>(coffeeBlogDbContext), IUserRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = coffeeBlogDbContext;

    public async Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                                       CancellationToken cancellationToken)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .FirstOrDefaultAsync(user => user.Email == usernameOrEmail || user.Username == usernameOrEmail, cancellationToken);

    public async Task<bool> UsernameExistsAsync(string username,
                                                CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .AnyAsync(user => user.Username == username, cancellationToken);

    public async Task<bool> EmailExistsAsync(string email,
                                             CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.AsNoTracking()
                                           .AnyAsync(user => user.Email == email, cancellationToken);

    public async Task<int> UpdateUsernameAsync(int id,
                                               string username,
                                               CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.Where(user => user.Id == id)
                                           .ExecuteUpdateAsync(user => user.SetProperty(property => property.Username, username), cancellationToken);

    public async Task<int> UpdateEmailAsync(int id,
                                            string email,
                                            CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.Where(user => user.Id == id)
                                           .ExecuteUpdateAsync(user => user.SetProperty(property => property.Email, email), cancellationToken);

    public async Task<int> UpdatePasswordAsync(int id,
                                               string password,
                                               CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.Where(user => user.Id == id)
                                           .ExecuteUpdateAsync(user => user.SetProperty(property => property.Password, password), cancellationToken);
}