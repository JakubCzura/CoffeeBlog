using FluentValidation;
using Shared.Application.AuthService.Validators.SharedValidators;

namespace Shared.Application.AuthService.Commands.Users.GenerateForgottenPasswordResetToken;

/// <summary>
/// Validator to validate <see cref="GenerateForgottenPasswordResetTokenCommand"/>.
/// </summary>
public class GenerateForgottenPasswordResetTokenCommandValidator : AbstractValidator<GenerateForgottenPasswordResetTokenCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public GenerateForgottenPasswordResetTokenCommandValidator()
        => RuleFor(x => x.Email).SetValidator(new EmailValidator());
}