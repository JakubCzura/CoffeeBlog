using Microsoft.EntityFrameworkCore;
using StatisticsCollector.Domain.Entities;
using System.Reflection;

namespace StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

public class StatisticsCollectorDbContext(DbContextOptions<StatisticsCollectorDbContext> options) : DbContext(options)
{
    public DbSet<RequestDetail> RequestDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}