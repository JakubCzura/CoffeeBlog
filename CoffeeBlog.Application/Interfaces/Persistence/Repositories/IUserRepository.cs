using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

public interface IUserRepository : IDbEntityBaseRepository<User>
{
}