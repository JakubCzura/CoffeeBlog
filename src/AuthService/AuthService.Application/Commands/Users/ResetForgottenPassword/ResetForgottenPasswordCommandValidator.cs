using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Resources;
using FluentValidation;

namespace AuthService.Application.Commands.Users.ResetForgottenPassword;

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

        RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage(ValidatorMessages.ConfirmPasswordIsRequired)
                                          .Equal(x => x.NewPassword).WithMessage(ValidatorMessages.PasswordAndConfirmPasswordMustMatch);

        RuleFor(x => x.ForgottenPasswordResetToken).NotEmpty().WithMessage(ValidatorMessages.TokenToResetPasswordIsRequired);
    }
}