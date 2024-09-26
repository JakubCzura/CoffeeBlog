using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthService.Infrastructure.Persistence.DatabaseContext;

/// <summary>
/// Database context.
/// </summary>
/// <param name="dbContextOptions">Configuration for database context.</param>
public class AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> dbContextOptions) : IdentityDbContext<User, IdentityRole<int>, int>(dbContextOptions)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<EventConsumerDetail> EventConsumerDetails { get; set; }
    public DbSet<RequestDetail> RequestDetails { get; set; }

    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<UserToRole> UserToRoles { get; set; }
    public DbSet<UserLastPassword> UserLastPasswords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}