using AuthService.Application.Commands.Users.SignUpUser;
using AuthService.Domain.Resources;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AuthService.Application.UnitTests.Commands.Users.SignUpUser;

public class SignUpUserCommandValidatorTests
{
    private readonly SignUpUserCommandValidator _createUserCommandValidator = new();

    [Fact]
    public void Validate_should_Pass_when_CommandIsCorrect()
    {
        //Arrange
        SignUpUserCommand command = new(
            "Johny",
            "jemail@email.com",
            "KJPa!2Dsd1@",
            "KJPa!2Dsd1@"
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_should_Fail_when_UsernameIsIncorrect()
    {
        //Arrange
        SignUpUserCommand command = new(
            new string('k', 200),
            "myemail@email.com",
            "KJPa!2Dsd1@",
            "KJPa!2Dsd1@"
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.UsernameCantContainMoreThan100Characters);
    }

    [Fact]
    public void Validate_should_Fail_when_EmailIsIncorrect()
    {
        //Arrange
        SignUpUserCommand command = new(
            "Johny",
            "myemail.com",
            "KJPa!2Dsd1@",
            "KJPa!2Dsd1@"
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.EmailMustBeInValidFormat);
    }

    [Fact]
    public void Validate_should_Fail_when_EmailIsSameAsUsername()
    {
        //Arrange
        SignUpUserCommand command = new(
            "myemail@mail.com",
            "myemail@mail.com",
            "KJPa!2Dsd1@",
            "KJPa!2Dsd1@"
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.EmailMustBeDifferentFromUsername);
    }

    [Fact]
    public void Validate_should_Fail_when_PasswordIsIncorrect()
    {
        //Arrange
        SignUpUserCommand command = new(
            "Johny",
            "myemail@emai.com",
            "Kkkk@d<",
            "Kkkk@d<"
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.PasswordMustContainAtLeastOneDigit);
    }

    [Fact]
    public void Validate_should_Fail_when_ConfirmPasswordIsIncorrect()
    {
        //Arrange
        SignUpUserCommand command = new(
            "Johny",
            "myemail@email.com",
            "Kkkk@d<",
            ""
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.ConfirmPasswordIsRequired);
    }

    [Fact]
    public void Validate_should_Fail_when_ConfirmPasswordIsDifferentFromPassword()
    {
        //Arrange
        SignUpUserCommand command = new(
            "Johny",
            "myemail@email.com",
            "Kkkk@d<2",
            "Kkkk@d<22222222222"
        );

        //Act
        TestValidationResult<SignUpUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.PasswordAndConfirmPasswordMustMatch);
    }
}