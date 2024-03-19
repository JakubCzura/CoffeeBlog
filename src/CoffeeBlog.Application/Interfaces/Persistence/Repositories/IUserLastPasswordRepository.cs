using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="UserLastPassword"/>.
/// </summary>
public interface IUserLastPasswordRepository : IDbEntityBaseRepository<UserLastPassword>
{
}