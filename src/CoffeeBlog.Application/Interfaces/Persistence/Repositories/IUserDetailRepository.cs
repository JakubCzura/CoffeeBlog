using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="UserDetail"/>.
/// </summary>
public interface IUserDetailRepository : IDbEntityBaseRepository<UserDetail>
{
}