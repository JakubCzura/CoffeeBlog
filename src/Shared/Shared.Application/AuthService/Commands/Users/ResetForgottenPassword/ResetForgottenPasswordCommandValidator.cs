using FluentValidation;
using Shared.Application.AuthService.Validators.SharedValidators;
using Shared.Domain.AuthService.Resources;

namespace Shared.Application.AuthService.Commands.Users.ResetForgottenPassword;

/// <summary>
/// Validator to validate <see cref="ResetForgottenPasswordCommand"/>.
/// </summary>
public class ResetForgottenPasswordCommandValidator : AbstractValidator<ResetForgottenPasswordCommand>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public ResetForgottenPasswordCommandValidator()
    {
        RuleFor(x => x.Email).SetValidator(new EmailValidator());

        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmNewPassword).NotEmpty()
                                          .WithMessage(ValidatorMessages.ConfirmPasswordIsRequired)
                                          .Equal(x => x.NewPassword)
                                          .WithMessage(ValidatorMessages.PasswordAndConfirmPasswordMustMatch);

        RuleFor(x => x.ForgottenPasswordResetToken).NotEmpty()
                                                   .WithMessage(ValidatorMessages.TokenToResetPasswordIsRequired)
                                                   .MaximumLength(200)
                                                   .WithMessage(ValidatorMessages.TokenCantContainMoreThan200Characters);
    }
}