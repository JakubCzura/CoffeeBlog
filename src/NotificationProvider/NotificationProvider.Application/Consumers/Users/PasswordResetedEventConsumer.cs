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
/// Consumer of <see cref="PasswordResetedEvent"/> event.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="_apiErrorRepository">Interface to perform api error operations in database.</param>
/// <param name="_emailMessageDetailRepository">Interface to perform email message detail operations in database.</param>
/// <param name="_emailMessageFactory">Interface to create e-mail messages.</param>
/// <param name="_emailServiceProvider">Interface to send e-mails.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class PasswordResetedEventConsumer(ILogger<PasswordResetedEventConsumer> _logger,
                                          IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                          IApiErrorRepository _apiErrorRepository,
                                          IEmailMessageDetailRepository _emailMessageDetailRepository,
                                          IEmailMessageFactory _emailMessageFactory,
                                          IEmailServiceProvider _emailServiceProvider,
                                          IMapper _mapper)
    : EventConsumerBase<PasswordResetedEvent, PasswordResetedEventConsumer>(_logger, _eventConsumerDetailRepository, _apiErrorRepository)
{
    private readonly IEmailMessageDetailRepository _emailMessageDetailRepository = _emailMessageDetailRepository;
    private readonly IEmailMessageFactory _emailMessageFactory = _emailMessageFactory;
    private readonly IEmailServiceProvider _emailServiceProvider = _emailServiceProvider;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Consumes <see cref="PasswordResetedEvent"/> event.<br/>
    /// Sends e-mail to user to inform that password has been reseted.
    /// </summary>
    /// <param name="context">Event's consumer.</param>
    /// <returns><see cref="Task"/></returns>
    public override async Task ConsumeEvent(ConsumeContext<PasswordResetedEvent> context)
    {
        IEmailMessage message = _emailMessageFactory.CreatePasswordResetedEmailMessage(context.Message.Email,
                                                                                       context.Message.Username);
        await _emailServiceProvider.SendEmailAsync(message, default);

        EmailMessageDetail emailMessageDetail = _mapper.Map<EmailMessageDetail>(message);
        await _emailMessageDetailRepository.CreateAsync(emailMessageDetail, default);
    }
}