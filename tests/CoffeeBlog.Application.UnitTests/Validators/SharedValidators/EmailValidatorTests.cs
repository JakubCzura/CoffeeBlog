using CoffeeBlog.Application.Validators.SharedValidators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CoffeeBlog.Application.UnitTests.Validators.SharedValidators;

public class EmailValidatorTests
{
    private readonly EmailValidator _emailValidator = new();

    [Fact]
    public void Validate_should_Pass_when_EmailIsCorrect()
        => _emailValidator.TestValidate("johny@emailprovider.com")
                          .ShouldNotHaveAnyValidationErrors();

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("s")]
    [InlineData(" d")]
    [InlineData("d s")]
    [InlineData("dd s")]
    [InlineData("@ s")]
    [InlineData("@")]
    [InlineData("@ ")]
    [InlineData("@D")]
    [InlineData("email.")]
    [InlineData("email.com")]
    [InlineData("email.@")]
    public void Validate_should_Fail_when_EmailIsIncorrect(string email)
        => _emailValidator.TestValidate(email)
                          .ShouldHaveAnyValidationError();

    [Fact]
    public void Validate_should_Fail_when_EmailIsLongerThanMaxLengthAllowed()
        => _emailValidator.TestValidate(new string('k', 321))
                          .ShouldHaveAnyValidationError();

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_EmailIsNull()
        => _emailValidator.Invoking(x => x.TestValidate(null!))
                          .Should()
                          .Throw<ArgumentNullException>();
}