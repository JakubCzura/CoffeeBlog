using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

public class CoffeeBlogDbContext(DbContextOptions<CoffeeBlogDbContext> options) : DbContext(options)
{
    public DbSet<ApiErrorEntity> ApiErrors { get; set; }
    public DbSet<RequestDetailEntity> RequestDetails { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserDetailEntity> UserDetails { get; set; }
    public DbSet<UserToRoleEntity> UserToRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}