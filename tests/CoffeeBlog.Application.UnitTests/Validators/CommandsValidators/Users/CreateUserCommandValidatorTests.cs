using CoffeeBlog.Application.Validators.CommandsValidators.Users;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Resources;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CoffeeBlog.Application.UnitTests.Validators.CommandsValidators.Users;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _createUserCommandValidator = new();

    [Fact]
    public void Validate_should_Pass_when_CommandIsCorrect()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = "Johny",
            Email = "jemail@email.com",
            Password = "KJPa!2Dsd1@",
            ConfirmPassword = "KJPa!2Dsd1@"
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_should_Fail_when_UsernameIsIncorrect()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = new string('k', 200),
            Email = "myemail@email.com",
            Password = "KJPa!2Dsd1@",
            ConfirmPassword = "KJPa!2Dsd1@"
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.UsernameCantContainMoreThan100Characters);
    }

    [Fact]
    public void Validate_should_Fail_when_EmailIsIncorrect()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = "Johny",
            Email = "myemail.com",
            Password = "KJPa!2Dsd1@",
            ConfirmPassword = "KJPa!2Dsd1@"
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.EmailMustBeInValidFormat);
    }

    [Fact]
    public void Validate_should_Fail_when_EmailIsSameAsUsername()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = "myemail@mail.com",
            Email = "myemail@mail.com",
            Password = "KJPa!2Dsd1@",
            ConfirmPassword = "KJPa!2Dsd1@"
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.EmailMustBeDifferentFromUsername);
    }

    [Fact]
    public void Validate_should_Fail_when_PasswordIsIncorrect()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = "Johny",
            Email = "myemail@emai.com",
            Password = "Kkkk@d<",
            ConfirmPassword = "Kkkk@d<"
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.PasswordMustContainAtLeastOneDigit);
    }

    [Fact]
    public void Validate_should_Fail_when_ConfirmPasswordIsIncorrect()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = "Johny",
            Email = "myemail@email.com",
            Password = "Kkkk@d<",
            ConfirmPassword = ""
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.ConfirmPasswordIsRequired);
    }

    [Fact]
    public void Validate_should_Fail_when_ConfirmPasswordIsDifferentFromPassword()
    {
        //Arrange
        CreateUserCommand command = new()
        {
            Username = "Johny",
            Email = "myemail@email.com",
            Password = "Kkkk@d<2",
            ConfirmPassword = "Kkkk@d<22222222222"
        };

        //Act
        TestValidationResult<CreateUserCommand> result = _createUserCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        result.Errors.Should().Contain(x => x.ErrorMessage == ValidatorMessages.PasswordAndConfirmPasswordMustMatch);
    }
}