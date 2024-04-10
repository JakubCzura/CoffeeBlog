using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Email message for user who wants to reset password.
/// </summary>
/// <param name="To">User's email.</param>
/// <param name="Subject">Email's subject about resetting password.</param>
/// <param name="Body">Email's body with information how to reset password, for example it can contain token to reset password.</param>
public record PasswordResetEmailMessage(string To,
                                        string? Subject,
                                        string? Body) : IEmailMessage;