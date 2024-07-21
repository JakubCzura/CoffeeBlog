using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities.Basics;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic repository to perform CRUD operations in database.
/// </summary>
/// <typeparam name="TEntity">Entity in database.</typeparam>
internal class DbEntityBaseRepository<TEntity> : IDbEntityBaseRepository<TEntity> where TEntity : DbEntityBase
{
    private readonly StatisticsCollectorDbContext _statisticsCollectorDbContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Constructor with database context.
    /// </summary>
    /// <param name="statisticsCollectorDbContext">Database context.</param>
    public DbEntityBaseRepository(StatisticsCollectorDbContext statisticsCollectorDbContext)
    {
        _statisticsCollectorDbContext = statisticsCollectorDbContext;
        _dbSet = _statisticsCollectorDbContext.Set<TEntity>();
    }

    public async Task<ObjectId> CreateAsync(TEntity entity,
                                            CancellationToken cancellationToken)

    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _statisticsCollectorDbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<TEntity?> GetAsync(ObjectId id,
                                   CancellationToken cancellationToken)
        => await _dbSet.AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbSet.AsNoTracking()
                       .ToListAsync(cancellationToken);

    public async Task<int> UpdateAsync(TEntity entity,
                                       CancellationToken cancellationToken)

    {
        _dbSet.Update(entity);
        return await _statisticsCollectorDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(ObjectId id,
                                       CancellationToken cancellationToken)
    {
        TEntity? entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return 0;
        }

        _dbSet.Remove(entity);
        return await _statisticsCollectorDbContext.SaveChangesAsync(cancellationToken);
    }
}