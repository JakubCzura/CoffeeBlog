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
internal class DbEntityBaseRepository<TEntity> : IDbEntityBaseRepository<TEntity> where TEntity : DbEntityBase
{
    private readonly NotificationProviderDbContext _notificationProviderDbContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Constructor with database context.
    /// </summary>
    /// <param name="notificationProviderDbContext">Database context.</param>
    public DbEntityBaseRepository(NotificationProviderDbContext notificationProviderDbContext)
    {
        _notificationProviderDbContext = notificationProviderDbContext;
        _dbSet = _notificationProviderDbContext.Set<TEntity>();
    }

    public async Task CreateAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)

    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _notificationProviderDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(ObjectId id,
                                   CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task<int> UpdateAsync(TEntity entity,
                                       CancellationToken cancellationToken = default)

    {
        _dbSet.Update(entity);
        return await _notificationProviderDbContext.SaveChangesAsync(cancellationToken);
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
        return await _notificationProviderDbContext.SaveChangesAsync(cancellationToken);
    }
}