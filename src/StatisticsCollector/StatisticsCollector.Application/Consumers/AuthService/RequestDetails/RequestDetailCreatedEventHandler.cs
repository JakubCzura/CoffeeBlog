using EventBus.Domain.Events.AuthService.RequestDetails;
using EventBus.Domain.Events.Consumers;
using MassTransit;

namespace StatisticsCollector.Application.Consumers.AuthService.RequestDetails;

internal sealed class RequestDetailCreatedEventHandler : IEventConsumer<RequestDetailCreatedEvent>
{
    public Task Consume(ConsumeContext<RequestDetailCreatedEvent> context)
    {
        //TODO: write data to database
        throw new NotImplementedException();
    }
}