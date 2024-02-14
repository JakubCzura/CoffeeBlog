using CoffeeBlog.Domain.Entities.Errors;
using CoffeeBlog.Domain.Entities.Requests;
using CoffeeBlog.Domain.Entities.Roles;
using CoffeeBlog.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

public class CoffeeBlogDbContext(DbContextOptions<CoffeeBlogDbContext> options) : DbContext(options)
{
    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<RequestDetail> RequestDetails { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<UserToRole> UserToRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}