using EventBus.Domain.Events.AuthService.Users;
using EventBus.Domain.Events.Consumers;
using MassTransit;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Consumers.Users;

internal sealed class PasswordResetTokenSentEventConsumer(IEmailMessageFactory _emailMessageFactory,
                                                          IEmailServiceProvider _emailServiceProvider) : IEventConsumer<PasswordResetTokenSentEvent>
{
    private readonly IEmailMessageFactory _emailMessageFactory = _emailMessageFactory;
    private readonly IEmailServiceProvider _emailServiceProvider = _emailServiceProvider;

    public Task Consume(ConsumeContext<PasswordResetTokenSentEvent> context)
    {
        IEmailMessage message = _emailMessageFactory.CreatePasswordResetEmailMessage(context.Message.Email,
                                                                                     context.Message.Username,
                                                                                     context.Message.Token,
                                                                                     context.Message.ExpirationDate);

        return _emailServiceProvider.SendEmailAsync(message);
    }
}