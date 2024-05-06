using AuthService.Application.Validators.SharedValidators;
using FluentValidation;

namespace AuthService.Application.Commands.Users.ChangeEmail;

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