using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.DatabaseContext;

public class NotificationProviderDbContext(DbContextOptions<NotificationProviderDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<ApiError> ApiErrors { get; set; }
    public DbSet<EmailMessageDetail> EmailMessageDetails { get; set; }
    public DbSet<EventConsumerDetail> EventConsumerDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiError>(entity =>
        {
            entity.ToCollection("ApiError");
        });

        modelBuilder.Entity<EmailMessageDetail>(entity =>
        {
            entity.ToCollection("EmailMessageDetail");
        });

        modelBuilder.Entity<EventConsumerDetail>(entity =>
        {
            entity.ToCollection("EventConsumerDetail");
        });
    }
}