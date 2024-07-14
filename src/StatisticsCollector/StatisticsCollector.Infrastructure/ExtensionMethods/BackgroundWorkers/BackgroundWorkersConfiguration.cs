using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using StatisticsCollector.Domain.SettingsOptions.UsersDiagnosticsCollector;
using StatisticsCollector.Infrastructure.BackgroundWorkers;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.BackgroundWorkers;

/// <summary>
/// Configuration of background workers.
/// </summary>
public static class BackgroundWorkersConfiguration
{
    /// <summary>
    /// Configures all background workers that work in the microservice.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection ConfigureBackgroundWorkers(this IServiceCollection services,
                                                                IConfiguration configuration)
    {
        UsersDiagnosticsCollectorOptions usersDiagnosticsCollectorOptions = configuration.GetSection(UsersDiagnosticsCollectorOptions.AppsettingsKey)
                                                                                         .Get<UsersDiagnosticsCollectorOptions>()!;

        services.AddQuartz(options =>
        {
            JobKey applicationDiagnosticsCollectorJobName = JobKey.Create(nameof(UsersDiagnosticsCollector));

            options.AddJob<UsersDiagnosticsCollector>(applicationDiagnosticsCollectorJobName)
                   .AddTrigger(trigger => trigger.ForJob(applicationDiagnosticsCollectorJobName)
                   .StartAt(DateBuilder.TomorrowAt(usersDiagnosticsCollectorOptions.StartHour, usersDiagnosticsCollectorOptions.StartMinute, usersDiagnosticsCollectorOptions.StartSecond))
                   .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(usersDiagnosticsCollectorOptions.IntervalInHours).RepeatForever()));
        });

        services.AddQuartzHostedService(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}