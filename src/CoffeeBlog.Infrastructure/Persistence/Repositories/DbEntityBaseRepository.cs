﻿using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities.DbEntitiesBase;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

public class DbEntityBaseRepository<T> : IDbEntityBaseRepository<T> where T : DbEntityBase
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext;
    private readonly DbSet<T> _dbSet;

    public DbEntityBaseRepository(CoffeeBlogDbContext coffeeBlogDbContext)
    {
        _coffeeBlogDbContext = coffeeBlogDbContext;
        _dbSet = _coffeeBlogDbContext.Set<T>();
    }

    public async Task<int> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(T)} was null");
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        return await _coffeeBlogDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken)
        => await _dbSet.AsNoTracking()
                       .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        => _dbSet.AsNoTracking()
                 .ToListAsync(cancellationToken);

    public Task<int> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new NullEntityException($"Provided entity {typeof(T)} was null");
        }

        _dbSet.Update(entity);
        return _coffeeBlogDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        => await _dbSet.Where(entity => entity.Id == id)
                       .ExecuteDeleteAsync(cancellationToken);
}