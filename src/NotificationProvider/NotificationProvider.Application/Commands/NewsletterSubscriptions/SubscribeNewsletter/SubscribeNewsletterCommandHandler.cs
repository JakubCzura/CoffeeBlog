using MediatR;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

namespace NotificationProvider.Application.Commands.NewsletterSubscriptions.SubscribeNewsletter;

public class SubscribeNewsletterCommandHandler : IRequestHandler<SubscribeNewsletterCommand, ResponseBase>
{
    public Task<ResponseBase> Handle(SubscribeNewsletterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Implement logic");
    }
}