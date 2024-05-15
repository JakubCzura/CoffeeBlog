using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Resources;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AuthService.Application.UnitTests.Validators.SharedValidators;

public class EmailValidatorTests
{
    private readonly EmailValidator _emailValidator = new();

    [Fact]
    public void Validate_should_Pass_when_EmailIsCorrect()
        => _emailValidator.TestValidate("johny@emailprovider.com")
                          .ShouldNotHaveAnyValidationErrors();

    public static TheoryData<string, string> Validate_should_Fail_when_EmailIsIncorrect_Data => new()
    {
        { "", ValidatorMessages.EmailIsRequired },
        { " ", ValidatorMessages.EmailIsRequired },
        { "s", ValidatorMessages.EmailMustBeInValidFormat },
        { " d", ValidatorMessages.EmailMustBeInValidFormat },
        { "d s", ValidatorMessages.EmailMustBeInValidFormat },
        { "dd s", ValidatorMessages.EmailMustBeInValidFormat },
        { "Q s", ValidatorMessages.EmailMustBeInValidFormat },
        { "Q", ValidatorMessages.EmailMustBeInValidFormat },
        { "Q ", ValidatorMessages.EmailMustBeInValidFormat },
        { "QD", ValidatorMessages.EmailMustBeInValidFormat },
        { "email.", ValidatorMessages.EmailMustBeInValidFormat },
        { "email.com", ValidatorMessages.EmailMustBeInValidFormat },
        { "email.Q", ValidatorMessages.EmailMustBeInValidFormat },
        { new string('k', 321), ValidatorMessages.EmailCantContainMoreThan320Characters }
    };

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_EmailIsIncorrect_Data))]
    public void Validate_should_Fail_when_EmailIsIncorrect(string email, 
                                                           string errorMessage)
        => _emailValidator.TestValidate(email)
                          .ShouldHaveAnyValidationError()
                          .WithErrorMessage(errorMessage);

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_EmailIsNull()
        => _emailValidator.Invoking(x => x.TestValidate(null!))
                          .Should()
                          .Throw<ArgumentNullException>();
}