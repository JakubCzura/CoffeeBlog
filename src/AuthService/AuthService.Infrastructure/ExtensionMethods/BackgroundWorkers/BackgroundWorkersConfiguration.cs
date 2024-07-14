using AuthService.Domain.SettingsOptions.BanRemovalService;
using AuthService.Infrastructure.BackgroundWorkers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace AuthService.Infrastructure.ExtensionMethods.BackgroundWorkers;

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
        BanRemovalServiceOptions banRemovalServiceOptions = configuration.GetSection(BanRemovalServiceOptions.AppsettingsKey)
                                                                         .Get<BanRemovalServiceOptions>()!;

        services.AddQuartz(options =>
        {
            JobKey banRemovalServiceJobName = JobKey.Create(nameof(BanRemovalService));

            options.AddJob<BanRemovalService>(banRemovalServiceJobName)
                   .AddTrigger(trigger => trigger.ForJob(banRemovalServiceJobName)
                   .StartAt(DateBuilder.TomorrowAt(banRemovalServiceOptions.StartHour, banRemovalServiceOptions.StartMinute, banRemovalServiceOptions.StartSecond))
                   .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(banRemovalServiceOptions.IntervalInHours).RepeatForever()));
        });

        services.AddQuartzHostedService(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}