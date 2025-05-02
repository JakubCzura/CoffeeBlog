using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;

public class CancelNewsletterSubscriptionCommand : IRequest<Result<ResponseBase>>
{
    public string Id { get; set; } = string.Empty;
}