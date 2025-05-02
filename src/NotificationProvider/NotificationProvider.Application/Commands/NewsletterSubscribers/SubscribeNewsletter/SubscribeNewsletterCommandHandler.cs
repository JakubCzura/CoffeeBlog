using FluentResults;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Helpers;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Enums;
using NotificationProvider.Domain.SettingsOptions.Email;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

namespace NotificationProvider.Application.Commands.NewsletterSubscribers.SubscribeNewsletter;

/// <summary>
/// Command handler to insert newsletter subscription. It's related to <see cref="SubscribeNewsletterCommand"/>.
/// </summary>
/// <param name="_emailOptions">Appsettings for e-mail.</param>
/// <param name="newsletterSubscriberRepository">Interface to perform newsletter subscriber operations in database.</param>
/// <param name="emailMessageRepository">Interface to perform e-mail message operations in database.</param>
/// <param name="emailMessageFactory">Interface to create e-mail message.</param>
/// <param name="dateTimeProvider">Interface to get current date and time.</param>
public class SubscribeNewsletterCommandHandler(IOptions<EmailOptions> _emailOptions,
                                               INewsletterSubscriberRepository newsletterSubscriberRepository,
                                               IEmailMessageRepository emailMessageRepository,
                                               IEmailMessageFactory emailMessageFactory,
                                               IDateTimeProvider dateTimeProvider)
    : IRequestHandler<SubscribeNewsletterCommand, Result<ResponseBase>>
{
    private readonly EmailOptions _emailOptions = _emailOptions.Value;

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
        ObjectId subscriberId = await newsletterSubscriberRepository.CreateAsync(newsletterSubscriber, cancellationToken);

        string body = emailMessageFactory.CreateSubscribeNewsletterBody(request.Email, subscriberId.ToString());

        EmailMessage emailMessage = new()
        {
            SenderName = _emailOptions.CoffeeBlog.SenderName,
            SenderEmail = _emailOptions.CoffeeBlog.Email,
            RecipientEmail = request.Email,
            Subject = "Confirm your Coffee Blog newsletter subscription",
            Body = body,
            MessageStatus = EmailMessageStatus.Queued,
            CreatedAt = dateTimeProvider.UtcNow,
            SentAt = dateTimeProvider.UtcNow
        };

        await emailMessageRepository.CreateAsync(emailMessage, cancellationToken);

        return Result.Ok(new ResponseBase());
    }
}