using Microsoft.EntityFrameworkCore;
using PostManager.Domain.Entities;
using System.Reflection;

namespace PostManager.Infrastructure.Persistence.DatabaseContext;

public class PostManagerDbContext(DbContextOptions<PostManagerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<PostComment> PostComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}