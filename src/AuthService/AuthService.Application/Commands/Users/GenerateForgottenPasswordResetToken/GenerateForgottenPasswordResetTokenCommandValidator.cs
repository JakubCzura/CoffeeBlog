using AuthService.Application.Validators.SharedValidators;
using FluentValidation;

namespace AuthService.Application.Commands.Users.GenerateForgottenPasswordResetToken;

/// <summary>
/// Validator to validate <see cref="GenerateForgottenPasswordResetTokenCommand"/>.
/// </summary>
public class GenerateForgottenPasswordResetTokenCommandValidator : AbstractValidator<GenerateForgottenPasswordResetTokenCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public GenerateForgottenPasswordResetTokenCommandValidator()
        => RuleFor(x => x.UserEmail).SetValidator(new EmailValidator());
}