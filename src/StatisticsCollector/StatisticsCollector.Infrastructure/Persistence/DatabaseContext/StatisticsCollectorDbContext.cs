using Microsoft.EntityFrameworkCore;
using StatisticsCollector.Domain.Entities;
using System.Reflection;

namespace StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

public class StatisticsCollectorDbContext(DbContextOptions<StatisticsCollectorDbContext> options) : DbContext(options)
{
    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<EventConsumerDetail> EventConsumerDetails { get; set; }
    public DbSet<RequestDetail> RequestDetails { get; set; }
    public DbSet<UsersDiagnostics> UsersDiagnostics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}