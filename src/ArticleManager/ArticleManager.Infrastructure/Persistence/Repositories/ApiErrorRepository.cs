using ArticleManager.Application.Interfaces.Persistence.Repositories;
using ArticleManager.Domain.Entities;
using ArticleManager.Infrastructure.Persistence.DatabaseContext;

namespace ArticleManager.Infrastructure.Persistence.Repositories;

internal class ApiErrorRepository(ArticleManagerDbContext _articleManagerDbContext)
    : DbEntityBaseRepository<ApiError>(_articleManagerDbContext), IApiErrorRepository
{
}