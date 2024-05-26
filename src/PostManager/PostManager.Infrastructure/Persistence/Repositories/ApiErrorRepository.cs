using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Domain.Entities;
using PostManager.Infrastructure.Persistence.DatabaseContext;

namespace PostManager.Infrastructure.Persistence.Repositories;

internal class ApiErrorRepository(PostManagerDbContext _postManagerDbContext)
    : DbEntityBaseRepository<ApiError>(_postManagerDbContext), IApiErrorRepository
{
}