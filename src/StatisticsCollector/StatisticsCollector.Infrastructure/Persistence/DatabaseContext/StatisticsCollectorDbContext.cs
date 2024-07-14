using Microsoft.EntityFrameworkCore;
using StatisticsCollector.Domain.Entities;
using System.Reflection;

namespace StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

/// <summary>
/// Database context.
/// </summary>
/// <param name="dbContextOptions">Configuration for database context.</param>
public class StatisticsCollectorDbContext(DbContextOptions<StatisticsCollectorDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<EventConsumerDetail> EventConsumerDetails { get; set; }
    public DbSet<RequestDetail> RequestDetails { get; set; }
    public DbSet<UsersDiagnostics> UsersDiagnostics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}