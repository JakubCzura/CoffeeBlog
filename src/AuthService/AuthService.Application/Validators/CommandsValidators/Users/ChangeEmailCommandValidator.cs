using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Commands.Users;
using FluentValidation;

namespace AuthService.Application.Validators.CommandsValidators.Users;

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