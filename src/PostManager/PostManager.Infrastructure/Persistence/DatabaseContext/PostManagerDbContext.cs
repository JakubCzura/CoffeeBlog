using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PostManager.Infrastructure.Persistence.DatabaseContext;

public class PostManagerDbContext(DbContextOptions<PostManagerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}