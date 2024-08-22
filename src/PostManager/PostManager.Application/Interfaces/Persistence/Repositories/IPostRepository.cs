using PostManager.Domain.Entities;

namespace PostManager.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="Post"/>.
/// </summary>
public interface IPostRepository : IBaseRepository<Post>
{
}