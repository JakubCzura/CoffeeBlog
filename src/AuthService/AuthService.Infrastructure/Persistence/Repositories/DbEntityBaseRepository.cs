using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities.DbEntitiesBase;
using AuthService.Domain.Exceptions;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext;
    private readonly DbSet<T> _dbSet;

    public DbEntityBaseRepository(CoffeeBlogDbContext coffeeBlogDbContext)
    {
        _coffeeBlogDbContext = coffeeBlogDbContext;
        _dbSet = _coffeeBlogDbContext.Set<T>();
    }

    public async Task<int> CreateAsync(T entity,
                                       CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(T)} was null");
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        return await _coffeeBlogDbContext.SaveChangesAsync(cancellationToken);
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
        return _coffeeBlogDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(int id,
                                       CancellationToken cancellationToken = default)
        => await _dbSet.Where(entity => entity.Id == id)
                       .ExecuteDeleteAsync(cancellationToken);
}