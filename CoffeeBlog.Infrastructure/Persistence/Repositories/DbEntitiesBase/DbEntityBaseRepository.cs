using CoffeeBlog.Application.Interfaces.Persistence.Repositories.DbEntitiesBase;
using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories.DbEntitiesBase;

public class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    public Task<int> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}