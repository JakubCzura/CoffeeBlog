using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="Role"/>.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
internal class RoleRepository(AuthServiceDbContext _authServiceDbContext)
    : BaseRepository<Role>(_authServiceDbContext), IRoleRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<List<string>> GetAllRolesNamesByUserId(int userId,
                                                             CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}