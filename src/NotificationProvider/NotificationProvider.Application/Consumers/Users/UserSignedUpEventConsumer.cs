using EventBus.Domain.Events.Consumers;
using EventBus.Domain.Events.Users;
using MassTransit;

namespace NotificationProvider.Application.Consumers.Users;

public class UserSignedUpEventConsumer : IEventConsumer<UserSignedUpEvent>
{
    public Task Consume(ConsumeContext<UserSignedUpEvent> context)
    {
        //TODO: implement, send welcome e-mail
        throw new NotImplementedException();
    }
}