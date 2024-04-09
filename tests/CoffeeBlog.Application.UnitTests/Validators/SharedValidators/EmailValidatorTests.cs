using AuthService.Application.Validators.SharedValidators;
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

    public static TheoryData<string> Validate_should_Fail_when_EmailIsIncorrect_Data => new()
    {
         "",
         " ",
         "s",
         " d",
         "d s",
         "dd s",
         "@ s",
         "@" ,
         "@ " ,
         "@D" ,
         "email." ,
         "email.com" ,
         "email.@" ,
         new string('k', 321)
    };

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_EmailIsIncorrect_Data))]
    public void Validate_should_Fail_when_EmailIsIncorrect(string email)
        => _emailValidator.TestValidate(email)
                          .ShouldHaveAnyValidationError();

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_EmailIsNull()
        => _emailValidator.Invoking(x => x.TestValidate(null!))
                          .Should()
                          .Throw<ArgumentNullException>();
}