using AuthService.Infrastructure.BackgroundWorkers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace AuthService.Infrastructure.ExtensionMethods.BackgroundWorkers;

public static class BackgroundWorkersConfiguration
{
    public static IServiceCollection ConfigureBackgroundWorkers(this IServiceCollection services,
                                                                IConfiguration configuration)
    {
        services.AddQuartz(options =>
        {
            JobKey applicationDiagnosticsCollectorJobName = JobKey.Create(nameof(ApplicationDiagnosticsCollector));

            options.AddJob<ApplicationDiagnosticsCollector>(applicationDiagnosticsCollectorJobName)
                   .AddTrigger(trigger => trigger.ForJob(applicationDiagnosticsCollectorJobName)
                                                 .WithCronSchedule("0 22 * * *"));
        });

        services.AddQuartzHostedService(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}