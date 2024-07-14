using ArticleManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ArticleManager.Infrastructure.Persistence.DatabaseContext;

/// <summary>
/// Database context.
/// </summary>
/// <param name="dbContextOptions">Configuration for database context.</param>
public class ArticleManagerDbContext(DbContextOptions<ArticleManagerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleComment> ArticleComments { get; set; }
    public DbSet<ApiError> ApiErrors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}