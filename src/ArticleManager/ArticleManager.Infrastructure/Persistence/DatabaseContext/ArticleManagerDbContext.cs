using ArticleManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ArticleManager.Infrastructure.Persistence.DatabaseContext;

public class ArticleManagerDbContext(DbContextOptions<ArticleManagerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Article> Articles { get; set; }

    public DbSet<ArticleComment> ArticleComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}