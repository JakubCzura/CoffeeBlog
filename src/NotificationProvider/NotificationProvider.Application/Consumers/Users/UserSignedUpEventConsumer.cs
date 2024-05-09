using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationProvider.Application.Consumers.Basics;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Models.Emails;

//Event consumer's namespace must be the same as event's namespace
namespace EventBus.Domain.Events.AuthService.Users;

public sealed class UserSignedUpEventConsumer(ILogger<UserSignedUpEventConsumer> _logger,
                                                IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                IApiErrorRepository _apiErrorRepository,
                                                IEmailMessageDetailRepository _emailMessageDetailRepository,
                                                IEmailMessageFactory _emailMessageFactory,
                                                IEmailServiceProvider _emailServiceProvider,
                                                IMapper _mapper)
    : EventConsumerBase<UserSignedUpEvent, UserSignedUpEventConsumer>(_logger, _eventConsumerDetailRepository, _apiErrorRepository)
{
    private readonly IEmailMessageDetailRepository _emailMessageDetailRepository = _emailMessageDetailRepository;
    private readonly IEmailMessageFactory _emailMessageFactory = _emailMessageFactory;
    private readonly IEmailServiceProvider _emailServiceProvider = _emailServiceProvider;
    private readonly IMapper _mapper = _mapper;

    public override async Task ConsumeEvent(ConsumeContext<UserSignedUpEvent> context)
    {
        IEmailMessage message = _emailMessageFactory.CreateWelcomeEmailMessage(context.Message.Email,
                                                                               context.Message.Username);

        await _emailServiceProvider.SendEmailAsync(message, default);

        EmailMessageDetail emailMessageDetail = _mapper.Map<EmailMessageDetail>(message);

        await _emailMessageDetailRepository.CreateAsync(emailMessageDetail, default);
    }
}