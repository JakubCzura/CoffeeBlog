using EventBus.Domain.Events.Consumers;
using EventBus.Domain.Events.Users;
using MassTransit;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Consumers.Users;

internal sealed class UserSignedUpEventConsumer(IEmailMessageFactory _emailMessageFactory,
                                                IEmailServiceProvider _emailServiceProvider) : IEventConsumer<UserSignedUpEvent>
{
    private readonly IEmailMessageFactory _emailMessageFactory = _emailMessageFactory;
    private readonly IEmailServiceProvider _emailServiceProvider = _emailServiceProvider;

    public Task Consume(ConsumeContext<UserSignedUpEvent> context)
    {
        IEmailMessage message = _emailMessageFactory.CreateWelcomeEmailMessage(context.Message.Email,
                                                                               context.Message.Username);
        
        return _emailServiceProvider.SendEmailAsync(message);
    }
}