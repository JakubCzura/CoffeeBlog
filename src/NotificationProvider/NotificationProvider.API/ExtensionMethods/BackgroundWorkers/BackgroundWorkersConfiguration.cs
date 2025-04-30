using Hangfire;
using NotificationProvider.Infrastructure.BackgroundWorkers;

namespace NotificationProvider.API.ExtensionMethods.BackgroundWorkers;

public static class BackgroundWorkersConfiguration
{
    public static IApplicationBuilder UseBackgroundWorkers(this WebApplication app)
    {
        string emailSendingProcessorName = "email-sending-processor";

        IRecurringJobManager jobManager = app.Services.GetRequiredService<IRecurringJobManager>();
        var isEmailProcessorShouldRun = bool.TryParse(app.Configuration["BackgroundWorkers:EmailSendingProcessor:IsActive"], out var result) && result;
        if (isEmailProcessorShouldRun)
        {
            jobManager.AddOrUpdate<EmailSendingProcessor>(emailSendingProcessorName, job => job.SendEmailsAsync(), app.Configuration["BackgroundWorkers:EmailSendingProcessor:Schedule"]);
        }
        else
        {
            jobManager.RemoveIfExists(emailSendingProcessorName);
        }

        return app;
    }
}