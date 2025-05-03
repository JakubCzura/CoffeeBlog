using FluentResults;
using MediatR;
using MongoDB.Bson;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;

namespace NotificationProvider.Application.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;

public class CancelNewsletterSubscriptionCommandHandler(INewsletterSubscriptionRepository newsletterSubscriberRepository)
    : IRequestHandler<CancelNewsletterSubscriptionCommand, Result<ResponseBase>>
{
    public async Task<Result<ResponseBase>> Handle(CancelNewsletterSubscriptionCommand request, CancellationToken cancellationToken)
    {
        await newsletterSubscriberRepository.DeleteAsync(new ObjectId(request.Id), cancellationToken);
        return Result.Ok();
    }
}