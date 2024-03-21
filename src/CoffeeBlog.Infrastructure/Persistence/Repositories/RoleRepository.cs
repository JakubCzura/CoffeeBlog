using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class RoleRepository(CoffeeBlogDbContext _coffeeBlogDbContext) : DbEntityBaseRepository<Role>(_coffeeBlogDbContext), IRoleRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = _coffeeBlogDbContext;

    public async Task<IEnumerable<Role>> GetAllByUserId(int userId,
                                                        CancellationToken cancellationToken = default)
        => (await _coffeeBlogDbContext.Users.AsNoTracking()
                                            .Include(user => user.Roles)
                                            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken))?.Roles ?? Enumerable.Empty<Role>();
}