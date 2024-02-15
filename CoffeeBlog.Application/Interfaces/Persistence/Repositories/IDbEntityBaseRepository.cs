using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Generic interface to perform CRUD operations in database
/// </summary>
/// <typeparam name="T">Entity in database</typeparam>
public interface IDbEntityBaseRepository<T> where T : DbEntityBase
{
    Task<int> CreateAsync(T entity, CancellationToken cancellationToken);

    Task<T?> GetAsync(int id, CancellationToken cancellationToken);

    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<int> UpdateAsync(T entity, CancellationToken cancellationToken);

    Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
}