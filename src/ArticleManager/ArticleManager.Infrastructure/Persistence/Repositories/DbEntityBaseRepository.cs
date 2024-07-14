using ArticleManager.Application.Interfaces.Persistence.Repositories;
using ArticleManager.Domain.Entities.Basics;
using ArticleManager.Domain.Exceptions;
using ArticleManager.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ArticleManager.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic repository to perform CRUD operations in database.
/// </summary>
/// <typeparam name="TEntity">Entity in database.</typeparam>
internal class DbEntityBaseRepository<TEntity> : IDbEntityBaseRepository<TEntity> where TEntity : DbEntityBase
{
    private readonly ArticleManagerDbContext _articleManagerDbContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Constructor with database context.
    /// </summary>
    /// <param name="articleManagerDbContext">Database context.</param>
    public DbEntityBaseRepository(ArticleManagerDbContext articleManagerDbContext)
    {
        _articleManagerDbContext = articleManagerDbContext;
        _dbSet = _articleManagerDbContext.Set<TEntity>();
    }

    public async Task<int> CreateAsync(TEntity entity,
                                       CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(TEntity)} was null");
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        return await _articleManagerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(int id,
                                   CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
                       .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => _dbSet.AsNoTracking()
                 .ToListAsync(cancellationToken);

    public Task<int> UpdateAsync(TEntity entity,
                                 CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(TEntity)} was null");
        }

        _dbSet.Update(entity);
        return _articleManagerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(int id,
                                       CancellationToken cancellationToken = default)
        => await _dbSet.Where(entity => entity.Id == id)
                       .ExecuteDeleteAsync(cancellationToken);
}