using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class UserLastPasswordRepository(CoffeeBlogDbContext _coffeeBlogDbContext) : DbEntityBaseRepository<UserLastPassword>(_coffeeBlogDbContext), IUserLastPasswordRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = _coffeeBlogDbContext;

    public async Task<List<UserLastPassword>> GetUserLastPasswordsAsync(int userId,
                                                                        CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.UserLastPasswords.AsNoTracking()
                                                       .Where(userLastPassword => userLastPassword.UserId == userId)
                                                       .ToListAsync(cancellationToken);
}