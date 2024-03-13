using CoffeeBlog.Application.Validators.SharedValidators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CoffeeBlog.Application.UnitTests.Validators.SharedValidators;

public class UsernameValidatorTests
{
    private readonly UsernameValidator _usernameValidator = new();

    public static TheoryData<string> Validate_should_Pass_when_UsernameIsCorrect_Data => new()
    {
        "Johny",
        "J",
        new string('K', 100)
    };

    [Theory]
    [MemberData(nameof(Validate_should_Pass_when_UsernameIsCorrect_Data))]
    public void Validate_should_Pass_when_UsernameIsCorrect(string username)
        => _usernameValidator.TestValidate(username)
                             .ShouldNotHaveAnyValidationErrors();

    public static TheoryData<string> Validate_should_Fail_when_UsernameIsIncorrect_Data => new()
    {
        "" ,
        new string('k', 101)
    };

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_UsernameIsIncorrect_Data))]
    public void Validate_should_Fail_when_UsernameIsIncorrect(string username)
        => _usernameValidator.TestValidate(username)
                             .ShouldHaveAnyValidationError();

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_UsernameIsNull()
        => _usernameValidator.Invoking(x => x.TestValidate(null!))
                             .Should()
                             .Throw<ArgumentNullException>();
}