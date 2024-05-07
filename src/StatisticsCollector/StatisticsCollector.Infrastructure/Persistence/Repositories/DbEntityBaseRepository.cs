using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities.Basics;
using StatisticsCollector.Domain.Exceptions;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.Persistence.Repositories;

internal class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly StatisticsCollectorDbContext _statisticsCollectorDbContext;
    private readonly DbSet<T> _dbSet;

    public DbEntityBaseRepository(StatisticsCollectorDbContext statisticsCollectorDbContext)
    {
        _statisticsCollectorDbContext = statisticsCollectorDbContext;
        _dbSet = _statisticsCollectorDbContext.Set<T>();
    }

    public async Task CreateAsync(T entity,
                                  CancellationToken cancellationToken)

    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _statisticsCollectorDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(ObjectId id,
                                   CancellationToken cancellationToken)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task<int> UpdateAsync(T entity,
                                       CancellationToken cancellationToken)

    {
        _dbSet.Update(entity);
        return await _statisticsCollectorDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(ObjectId id,
                                       CancellationToken cancellationToken)
    {
        T? entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return 0;
        }

        _dbSet.Remove(entity);
        return await _statisticsCollectorDbContext.SaveChangesAsync(cancellationToken);
    }
}