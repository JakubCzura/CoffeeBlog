using ArticleManager.Application.Interfaces.Persistence.Repositories;
using ArticleManager.Domain.Entities;
using ArticleManager.Infrastructure.Persistence.DatabaseContext;

namespace ArticleManager.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="ApiError"/>.
/// </summary>
/// <param name="articleManagerDbContext">Database context.</param>
internal class ApiErrorRepository(ArticleManagerDbContext articleManagerDbContext) : BaseRepository<ApiError>(articleManagerDbContext), IApiErrorRepository
{
}