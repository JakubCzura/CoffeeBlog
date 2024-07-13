using ArticleManager.Application.Interfaces.Persistence.Repositories;
using ArticleManager.Domain.Entities.Basics;
using ArticleManager.Domain.Exceptions;
using ArticleManager.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ArticleManager.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic repository to perform CRUD operations in database.
/// </summary>
/// <typeparam name="T">Entity in database.</typeparam>
internal class DbEntityBaseRepository<T> 
    : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly ArticleManagerDbContext _articleManagerDbContext;
    private readonly DbSet<T> _dbSet;

    public DbEntityBaseRepository(ArticleManagerDbContext articleManagerDbContext)
    {
        _articleManagerDbContext = articleManagerDbContext;
        _dbSet = _articleManagerDbContext.Set<T>();
    }

    public async Task<int> CreateAsync(T entity,
                                       CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(T)} was null");
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        return await _articleManagerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(int id,
                                   CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
                       .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => _dbSet.AsNoTracking()
                 .ToListAsync(cancellationToken);

    public Task<int> UpdateAsync(T entity,
                                 CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(T)} was null");
        }

        _dbSet.Update(entity);
        return _articleManagerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(int id,
                                       CancellationToken cancellationToken = default)
        => await _dbSet.Where(entity => entity.Id == id)
                       .ExecuteDeleteAsync(cancellationToken);
}