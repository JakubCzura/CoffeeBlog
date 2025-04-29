using FluentResults;
using MediatR;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

namespace NotificationProvider.Application.Commands.NewsletterSubscribers.SubscribeNewsletter;

/// <summary>
/// Command handler to insert newsletter subscription. It's related to <see cref="SubscribeNewsletterCommand"/>.
/// </summary>
/// <param name="newsletterSubscriberRepository">Interface to perform newsletter subscriber operations in database.</param>
public class SubscribeNewsletterCommandHandler(INewsletterSubscriberRepository newsletterSubscriberRepository)
    : IRequestHandler<SubscribeNewsletterCommand, Result<ResponseBase>>
{
    /// <summary>
    /// Handles request to insert newsletter subscription.
    /// </summary>
    /// <param name="request">Request command with details to insert newsletter subscription.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="ResponseBase"/></returns>
    public async Task<Result<ResponseBase>> Handle(SubscribeNewsletterCommand request, CancellationToken cancellationToken)
    {
        //TODO: implement EmailAlreadyExists error
        if (await newsletterSubscriberRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            throw new NotImplementedException(" implement EmailAlreadyExists error");
            //return Result.Fail<ResponseBase>(new EmailAlreadyExists());
        }

        NewsletterSubscriber newsletterSubscriber = new() { Email = request.Email, AgreeToTerms = request.AgreeToTerms, IsConfirmed = false };
        await newsletterSubscriberRepository.CreateAsync(newsletterSubscriber, cancellationToken);

        return Result.Ok(new ResponseBase());
    }
}