using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Enums;

namespace NotificationProvider.Domain.Models.Emails;

public record SendEmailMessageResult(EmailMessage EmailMessage,
                                     EmailMessageStatus EmailMessageStatus,
                                     int? SmtpErrorCode = null,
                                     string? ErrorMessage = null);