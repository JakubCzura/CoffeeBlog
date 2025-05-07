using Microsoft.EntityFrameworkCore;
using NotificationProvider.Domain.Entities;
using System.Reflection;

namespace NotificationProvider.Infrastructure.Persistence.DatabaseContext;

/// <summary>
/// Database context.
/// </summary>
/// <param name="dbContextOptions">Configuration for database context.</param>
public class NotificationProviderDbContext : DbContext
{
    public NotificationProviderDbContext(DbContextOptions<NotificationProviderDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        // Set AutoTransactionBehavior to Never
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<EventConsumerDetail> EventConsumerDetails { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}