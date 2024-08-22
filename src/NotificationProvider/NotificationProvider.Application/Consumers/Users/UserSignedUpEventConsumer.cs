using AutoMapper;
using EventBus.Domain.Events.AuthService.Users;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationProvider.Application.Consumers.Basics;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Consumers.Users;

/// <summary>
/// Consumer of <see cref="UserSignedUpEvent"/> event.
/// </summary>
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="apiErrorRepository">Interface to perform api error operations in database.</param>
/// <param name="emailMessageDetailRepository">Interface to perform email message detail operations in database.</param>
/// <param name="emailMessageFactory">Interface to create e-mail messages.</param>
/// <param name="emailServiceProvider">Interface to send e-mails.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
public sealed class UserSignedUpEventConsumer(ILogger<UserSignedUpEventConsumer> logger,
                                              IEventConsumerDetailRepository eventConsumerDetailRepository,
                                              IApiErrorRepository apiErrorRepository,
                                              IEmailMessageDetailRepository emailMessageDetailRepository,
                                              IEmailMessageFactory emailMessageFactory,
                                              IEmailServiceProvider emailServiceProvider,
                                              IMapper mapper)
    : EventConsumerBase<UserSignedUpEvent, UserSignedUpEventConsumer>(logger, eventConsumerDetailRepository, apiErrorRepository)
{
    /// <summary>
    /// Consumes <see cref="UserSignedUpEvent"/> event.<br/>
    /// Sends welcome e-mail to user who has successfully signed up.
    /// </summary>
    /// <param name="context">Event's consumer.</param>
    /// <returns><see cref="Task"/></returns>
    public override async Task ConsumeEvent(ConsumeContext<UserSignedUpEvent> context)
    {
        IEmailMessage message = emailMessageFactory.CreateWelcomeEmailMessage(context.Message.Email,
                                                                               context.Message.Username);
        await emailServiceProvider.SendEmailAsync(message, default);

        EmailMessageDetail emailMessageDetail = mapper.Map<EmailMessageDetail>(message);
        await emailMessageDetailRepository.CreateAsync(emailMessageDetail, default);
    }
}