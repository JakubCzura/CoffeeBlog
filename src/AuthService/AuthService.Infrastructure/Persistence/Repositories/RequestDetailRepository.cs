using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class RequestDetailRepository(AuthServiceDbContext authServiceDbContext) 
    : DbEntityBaseRepository<RequestDetail>(authServiceDbContext), IRequestDetailRepository
{
}