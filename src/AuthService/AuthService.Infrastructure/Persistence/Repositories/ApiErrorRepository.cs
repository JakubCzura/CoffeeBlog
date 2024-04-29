using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class ApiErrorRepository(AuthServiceDbContext authServiceDbContext)
    : DbEntityBaseRepository<ApiError>(authServiceDbContext), IApiErrorRepository
{
}