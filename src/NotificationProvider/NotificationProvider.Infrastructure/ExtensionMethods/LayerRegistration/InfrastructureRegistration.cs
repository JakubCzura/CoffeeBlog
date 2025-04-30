using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Helpers;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Infrastructure.Email;
using NotificationProvider.Infrastructure.ExtensionMethods.Database;
using NotificationProvider.Infrastructure.Factories.Emails;
using NotificationProvider.Infrastructure.Helpers;
using NotificationProvider.Infrastructure.Persistence.Repositories;

namespace NotificationProvider.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddScoped<IEmailMessageFactory, EmailMessageFactory>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
        services.AddScoped<IEventConsumerDetailRepository, EventConsumerDetailRepository>();
        services.AddScoped<INewsletterSubscriberRepository, NewsletterSubscriberRepository>();

        MongoUrlBuilder mongoUrlBuilder = new(configuration.GetValue<string>("Database:ConnectionString"));
        MongoClient mongoClient = new(mongoUrlBuilder.ToMongoUrl());

        services.AddHangfire(cfg => cfg
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMongoStorage(mongoClient, configuration.GetValue<string>("Database:DatabaseName"), new MongoStorageOptions
            {
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection,
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy(),
                },
                Prefix = "notifications.hangfire",
                CheckConnection = true
            })
        );

        services.AddHangfireServer(serverOptions =>
        {
            serverOptions.ServerName = "Notifications.Hangfire";
        });

        return services;
    }
}