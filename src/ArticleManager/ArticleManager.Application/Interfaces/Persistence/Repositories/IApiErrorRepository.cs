using ArticleManager.Domain.Entities;

namespace ArticleManager.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="ApiError"/>.
/// </summary>
public interface IApiErrorRepository : IBaseRepository<ApiError>
{
}