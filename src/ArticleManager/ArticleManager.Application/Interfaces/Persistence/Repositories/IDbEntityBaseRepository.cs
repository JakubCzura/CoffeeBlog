using ArticleManager.Domain.Entities.Basics;

namespace ArticleManager.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Generic interface to perform CRUD operations in database.
/// </summary>
/// <typeparam name="TEntity">Entity in database.</typeparam>
public interface IDbEntityBaseRepository<TEntity>
    where TEntity : DbEntityBase
{
    /// <summary>
    /// Adds new entity to database.
    /// </summary>
    /// <param name="entity">Entity to add to database.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Number of state entries written to database.</returns>
    Task<int> CreateAsync(TEntity entity,
                          CancellationToken cancellationToken);

    /// <summary>
    /// Returns entity from database by id.
    /// </summary>
    /// <param name="id">Entity's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Entity if found, otherwise null.</returns>
    Task<TEntity?> GetAsync(int id,
                      CancellationToken cancellationToken);

    /// <summary>
    /// Returns all rows for specified entity's type from database.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>List of all entities for specified type.</returns>
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Updates entity in database.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Number of state entries written to database.</returns>
    Task<int> UpdateAsync(TEntity entity,
                          CancellationToken cancellationToken);

    /// <summary>
    /// Deletes entity in database by id.
    /// </summary>
    /// <param name="id">Entity's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Number of rows deleted in database.</returns>
    Task<int> DeleteAsync(int id,
                          CancellationToken cancellationToken);
}