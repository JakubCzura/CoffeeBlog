using FluentValidation;
using Shared.Application.AuthService.Validators.SharedValidators;

namespace Shared.Application.AuthService.Commands.Users.ChangeEmail;

/// <summary>
/// Validator to validate <see cref="ChangeEmailCommand"/>.
/// </summary>
public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public ChangeEmailCommandValidator()
        => RuleFor(x => x.NewEmail).SetValidator(new EmailValidator());
}