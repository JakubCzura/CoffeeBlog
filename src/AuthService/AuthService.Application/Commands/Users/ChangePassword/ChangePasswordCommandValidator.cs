using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Resources;
using FluentValidation;

namespace AuthService.Application.Commands.Users.ChangePassword;

/// <summary>
/// Validator to validate <see cref="ChangePasswordCommand"/>.
/// </summary>
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage(ValidatorMessages.ConfirmPasswordIsRequired)
                                          .Equal(x => x.NewPassword).WithMessage(ValidatorMessages.PasswordAndConfirmPasswordMustMatch);
    }
}