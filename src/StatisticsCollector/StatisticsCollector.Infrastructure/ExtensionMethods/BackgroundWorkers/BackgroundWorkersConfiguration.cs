using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using StatisticsCollector.Infrastructure.BackgroundWorkers;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.BackgroundWorkers;

public static class BackgroundWorkersConfiguration
{
    public static IServiceCollection ConfigureBackgroundWorkers(this IServiceCollection services,
                                                                IConfiguration configuration)
    {
        services.AddQuartz(options =>
        {
            JobKey applicationDiagnosticsCollectorJobName = JobKey.Create(nameof(UsersDiagnosticsCollector));

            options.AddJob<UsersDiagnosticsCollector>(applicationDiagnosticsCollectorJobName)
                   .AddTrigger(trigger => trigger.ForJob(applicationDiagnosticsCollectorJobName)
                   .StartAt(DateBuilder.TomorrowAt(0, 30, 0))
                   .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
        });

        services.AddQuartzHostedService(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}