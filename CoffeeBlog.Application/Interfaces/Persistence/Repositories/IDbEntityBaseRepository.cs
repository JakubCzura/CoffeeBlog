using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Generic interface to perform CRUD operations in database
/// </summary>
/// <typeparam name="T">Entity in database</typeparam>
public interface IDbEntityBaseRepository<T> where T : DbEntityBase
{
    Task<int> CreateAsync(T entity);

    Task<T?> GetAsync(int id);

    Task<List<T>> GetAllAsync();

    Task<int> UpdateAsync(T entity);

    Task<int> DeleteAsync(int id);
}