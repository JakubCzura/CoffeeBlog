using Microsoft.EntityFrameworkCore;
using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Domain.Entities.Basics;
using PostManager.Domain.Exceptions;
using PostManager.Infrastructure.Persistence.DatabaseContext;

namespace PostManager.Infrastructure.Persistence.Repositories;

internal class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly PostManagerDbContext _postManagerDbContext;
    private readonly DbSet<T> _dbSet;

    public DbEntityBaseRepository(PostManagerDbContext postManagerDbContext)
    {
        _postManagerDbContext = postManagerDbContext;
        _dbSet = _postManagerDbContext.Set<T>();
    }

    public async Task<int> CreateAsync(T entity,
                                       CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(T)} was null");
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        return await _postManagerDbContext.SaveChangesAsync(cancellationToken);
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
        return _postManagerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(int id,
                                       CancellationToken cancellationToken = default)
        => await _dbSet.Where(entity => entity.Id == id)
                       .ExecuteDeleteAsync(cancellationToken);
}