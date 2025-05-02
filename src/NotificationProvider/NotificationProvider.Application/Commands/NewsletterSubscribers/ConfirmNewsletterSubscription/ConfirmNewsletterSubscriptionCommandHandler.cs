using FluentResults;
using MediatR;
using MongoDB.Bson;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;

namespace NotificationProvider.Application.Commands.NewsletterSubscribers.ConfirmNewsletterSubscription;

public class ConfirmNewsletterSubscriptionCommandHandler(INewsletterSubscriberRepository newsletterSubscriberRepository)
    : IRequestHandler<ConfirmNewsletterSubscriptionCommand, Result<ResponseBase>>
{
    public async Task<Result<ResponseBase>> Handle(ConfirmNewsletterSubscriptionCommand request, CancellationToken cancellationToken)
    {
        await newsletterSubscriberRepository.ConfirmSubscriptionAsync(new ObjectId(request.Id), cancellationToken);
        return Result.Ok();
    }
}