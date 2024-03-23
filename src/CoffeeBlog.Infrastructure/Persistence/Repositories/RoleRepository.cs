using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class RoleRepository(CoffeeBlogDbContext _coffeeBlogDbContext) : DbEntityBaseRepository<Role>(_coffeeBlogDbContext), IRoleRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = _coffeeBlogDbContext;

    public async Task<List<string>> GetAllRolesNamesByUserId(int userId,
                                                             CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.Users.Where(user => user.Id == userId)
                                           .Include(user => user.Roles)
                                           .SelectMany(user => user.Roles)
                                           .Select(userRole => userRole.Name)
                                           .ToListAsync(cancellationToken);
}