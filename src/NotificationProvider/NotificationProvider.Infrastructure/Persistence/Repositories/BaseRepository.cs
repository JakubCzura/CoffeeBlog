using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities.Basics;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic repository to perform CRUD operations in database.
/// </summary>
/// <typeparam name="TEntity">Entity in database.</typeparam>
/// <param name="notificationProviderDbContext">Database context.</param>
internal class BaseRepository<TEntity>(NotificationProviderDbContext notificationProviderDbContext)
    : IBaseRepository<TEntity> where TEntity : DbEntityBase
{
    private readonly DbSet<TEntity> _dbSet = notificationProviderDbContext.Set<TEntity>();

    public async Task<ObjectId> CreateAsync(TEntity entity,
                                            CancellationToken cancellationToken = default)

    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await notificationProviderDbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<TEntity?> GetAsync(ObjectId id,
                                         CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
                       .ToListAsync(cancellationToken);

    public async Task<int> UpdateAsync(TEntity entity,
                                       CancellationToken cancellationToken = default)

    {
        _dbSet.Update(entity);
        return await notificationProviderDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateRangeAsync(List<TEntity> entities,
                                            CancellationToken cancellationToken = default)

    {
        _dbSet.UpdateRange(entities);
        return await notificationProviderDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(ObjectId id,
                                       CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return 0;
        }

        _dbSet.Remove(entity);
        return await notificationProviderDbContext.SaveChangesAsync(cancellationToken);
    }
}