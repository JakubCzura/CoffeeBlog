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

internal sealed class PasswordResetTokenSentEventConsumer(ILogger<PasswordResetTokenSentEventConsumer> _logger,
                                                          IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                          IEmailMessageDetailRepository _emailMessageDetailRepository,
                                                          IEmailMessageFactory _emailMessageFactory,
                                                          IEmailServiceProvider _emailServiceProvider,
                                                          IApiErrorRepository _apiErrorRepository,
                                                          IMapper _mapper)
    : EventConsumerBase<PasswordResetTokenSentEvent, PasswordResetTokenSentEventConsumer>(_logger, _eventConsumerDetailRepository, _apiErrorRepository)
{
    private readonly IEmailMessageDetailRepository _emailMessageDetailRepository = _emailMessageDetailRepository;
    private readonly IEmailMessageFactory _emailMessageFactory = _emailMessageFactory;
    private readonly IEmailServiceProvider _emailServiceProvider = _emailServiceProvider;
    private readonly IMapper _mapper = _mapper;

    public override async Task ConsumeEvent(ConsumeContext<PasswordResetTokenSentEvent> context)
    {
        IEmailMessage message = _emailMessageFactory.CreatePasswordResetEmailMessage(context.Message.Email,
                                                                                     context.Message.Username,
                                                                                     context.Message.Token,
                                                                                     context.Message.ExpirationDate);

        await _emailServiceProvider.SendEmailAsync(message, default);

        EmailMessageDetail emailMessageDetail = _mapper.Map<EmailMessageDetail>(message);

        await _emailMessageDetailRepository.CreateAsync(emailMessageDetail, default);
    }
}