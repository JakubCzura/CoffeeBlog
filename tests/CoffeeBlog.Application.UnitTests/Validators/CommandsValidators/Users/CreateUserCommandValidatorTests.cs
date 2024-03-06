using CoffeeBlog.Application.Validators.CommandsValidators.Users;
using CoffeeBlog.Domain.Commands.Users;
using FluentValidation.TestHelper;

namespace CoffeeBlog.Application.UnitTests.Validators.CommandsValidators.Users;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _createUserCommandValidator = new();

    [Fact]
    public void Validate_should_Pass_when_CommandIsValid()
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
}