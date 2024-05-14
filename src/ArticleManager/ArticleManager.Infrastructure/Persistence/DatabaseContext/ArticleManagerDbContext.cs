using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ArticleManager.Infrastructure.Persistence.DatabaseContext;

public class ArticleManagerDbContext(DbContextOptions<ArticleManagerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}