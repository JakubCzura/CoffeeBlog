using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities.Basics;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly NotificationProviderDbContext _notificationProviderDbContext;
    private readonly DbSet<T> _dbSet;

    public DbEntityBaseRepository(NotificationProviderDbContext notificationProviderDbContext)
    {
        _notificationProviderDbContext = notificationProviderDbContext;
        _dbSet = _notificationProviderDbContext.Set<T>();
    }

    public async Task CreateAsync(T entity,
                                  CancellationToken cancellationToken)

    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _notificationProviderDbContext.SaveChangesAsync(cancellationToken);
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
        return await _notificationProviderDbContext.SaveChangesAsync(cancellationToken);
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
        return await _notificationProviderDbContext.SaveChangesAsync(cancellationToken);
    }
}