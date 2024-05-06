using Microsoft.EntityFrameworkCore;
using NotificationProvider.Domain.Entities;
using System.Reflection;

namespace NotificationProvider.Infrastructure.Persistence.DatabaseContext;

public class NotificationProviderDbContext(DbContextOptions<NotificationProviderDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<EmailMessageDetail> EmailMessageDetails { get; set; }
    public DbSet<EventConsumerDetail> EventConsumerDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}