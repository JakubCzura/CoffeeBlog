using CoffeeBlog.Domain.Entities.Base;
using CoffeeBlog.Domain.Interfaces.Base;

namespace CoffeeBlog.Infrastructure.Repositories.Base;

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