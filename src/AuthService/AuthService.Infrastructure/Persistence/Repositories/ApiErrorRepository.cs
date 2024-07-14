using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="ApiError"/>.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
internal class ApiErrorRepository(AuthServiceDbContext _authServiceDbContext)
    : DbEntityBaseRepository<ApiError>(_authServiceDbContext), IApiErrorRepository
{
}