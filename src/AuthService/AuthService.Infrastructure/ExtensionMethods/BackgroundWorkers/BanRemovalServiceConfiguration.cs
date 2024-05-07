using AuthService.Infrastructure.BackgroundWorkers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace AuthService.Infrastructure.ExtensionMethods.BackgroundWorkers;

public static class BanRemovalServiceConfiguration
{
    public static IServiceCollection ConfigureBackgroundWorkers(this IServiceCollection services,
                                                                IConfiguration configuration)
    {
        services.AddQuartz(options =>
        {
            JobKey banRemovalServiceJobName = JobKey.Create(nameof(BanRemovalService));

            options.AddJob<BanRemovalService>(banRemovalServiceJobName)
                   .AddTrigger(trigger => trigger.ForJob(banRemovalServiceJobName)
                   .StartAt(DateBuilder.TomorrowAt(0, 5, 0))
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