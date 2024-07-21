using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities.Basics;
using AuthService.Domain.Exceptions;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic repository to perform CRUD operations in database.
/// </summary>
/// <typeparam name="TEntity">Entity in database.</typeparam>
internal class DbEntityBaseRepository<TEntity> : IDbEntityBaseRepository<TEntity> where TEntity : DbEntityBase
{
    private readonly AuthServiceDbContext _authServiceDbContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Constructor with database context.
    /// </summary>
    /// <param name="authServiceDbContext">Database context.</param>
    public DbEntityBaseRepository(AuthServiceDbContext authServiceDbContext)
    {
        _authServiceDbContext = authServiceDbContext;
        _dbSet = _authServiceDbContext.Set<TEntity>();
    }

    public async Task<int> CreateAsync(TEntity entity,
                                       CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(TEntity)} was null");
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        await _authServiceDbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
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
        return _authServiceDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(int id,
                                       CancellationToken cancellationToken = default)
        => await _dbSet.Where(entity => entity.Id == id)
                       .ExecuteDeleteAsync(cancellationToken);
}