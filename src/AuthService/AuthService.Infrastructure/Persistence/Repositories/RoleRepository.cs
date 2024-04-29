using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class RoleRepository(AuthServiceDbContext _authServiceDbContext)
    : DbEntityBaseRepository<Role>(_authServiceDbContext), IRoleRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<List<string>> GetAllRolesNamesByUserId(int userId,
                                                             CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Id == userId)
                                           .Include(user => user.Roles)
                                           .SelectMany(user => user.Roles)
                                           .Select(userRole => userRole.Name)
                                           .ToListAsync(cancellationToken);
}