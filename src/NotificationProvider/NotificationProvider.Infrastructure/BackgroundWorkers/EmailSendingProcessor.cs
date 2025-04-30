using Microsoft.Extensions.Logging;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Infrastructure.BackgroundWorkers;

public class EmailSendingProcessor(IEmailMessageRepository emailMessageDetailRepository,
                                   IEmailSender emailServiceProvider,
                                   ILogger<EmailSendingProcessor> logger)
{
    public async Task SendEmailsAsync()
    {
        List<EmailMessage> emailMessages = await emailMessageDetailRepository
            .GetMessagesToSendAsync(default);

        Dictionary<EmailMessage, Task<SendEmailMessageResult>> sendEmailMessagesTasks =
            emailMessages.ToDictionary(emailMessage => emailMessage,
                                       emailMessage => emailServiceProvider
                                           .SendEmailAsync(emailMessage, default));

        SendEmailMessageResult[] sendEmailMessageResults =
            await Task.WhenAll(sendEmailMessagesTasks.Values);

        List<EmailMessage> messagesToUpdate = [];

        foreach (SendEmailMessageResult sendEmailMessageResult in sendEmailMessageResults)
        {
            if (sendEmailMessageResult.EmailMessageStatus == Domain.Enums.EmailMessageStatus.Sent)
            {
                sendEmailMessageResult.EmailMessage.MessageStatus = sendEmailMessageResult.EmailMessageStatus;
                sendEmailMessageResult.EmailMessage.SentAt = DateTime.UtcNow;

                messagesToUpdate.Add(sendEmailMessageResult.EmailMessage);

                logger.LogInformation("Email with id {Id} sent successfully.", sendEmailMessageResult.EmailMessage.Id);
            }
            else
            {
                sendEmailMessageResult.EmailMessage.SendErrorCount++;
                sendEmailMessageResult.EmailMessage.SmtpErrorCode = sendEmailMessageResult.SmtpErrorCode;
                sendEmailMessageResult.EmailMessage.ErrorMessage = sendEmailMessageResult.ErrorMessage;
                sendEmailMessageResult.EmailMessage.MessageStatus = sendEmailMessageResult.EmailMessageStatus;
                sendEmailMessageResult.EmailMessage.SentAt = DateTime.UtcNow;

                messagesToUpdate.Add(sendEmailMessageResult.EmailMessage);

                logger.LogError("Failed to send email with id {Id}. Error: {Error}", sendEmailMessageResult.EmailMessage.Id, sendEmailMessageResult.ErrorMessage);
            }
        }

        await emailMessageDetailRepository.UpdateRangeAsync(messagesToUpdate, default);
    }
}