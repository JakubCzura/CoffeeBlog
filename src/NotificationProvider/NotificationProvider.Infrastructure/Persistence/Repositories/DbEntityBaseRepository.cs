using MongoDB.Driver;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities.Basics;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly NotificationProviderDbContext _notificationProviderDbContext;
    private readonly IMongoCollection<T> _collection;

    public DbEntityBaseRepository(NotificationProviderDbContext notificationProviderDbContext)
    {
        _notificationProviderDbContext = notificationProviderDbContext;
        _collection = _notificationProviderDbContext.GetCollection<T>(typeof(T).Name);
    }

    public async Task CreateAsync(T entity,
                                  CancellationToken cancellationToken)
        => await _collection.InsertOneAsync(entity, null, cancellationToken);

    public async Task<T?> GetAsync(string id,
                                   CancellationToken cancellationToken)
        => await _collection.Find(x => x.Id == id)
                            .FirstAsync(cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        => await _collection.Find(_ => true)
                            .ToListAsync(cancellationToken);

    public async Task<ReplaceOneResult> UpdateAsync(T entity,
                                                    CancellationToken cancellationToken)
        => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);

    public Task<DeleteResult> DeleteAsync(string id,
                                          CancellationToken cancellationToken)
        => _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
}