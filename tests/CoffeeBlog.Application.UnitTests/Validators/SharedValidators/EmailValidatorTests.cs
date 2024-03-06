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

    public static IEnumerable<object[]> Validate_should_Fail_when_EmailIsIncorrect_Data()
    {
        yield return new object[] { "" };
        yield return new object[] { " " };
        yield return new object[] { "s" };
        yield return new object[] { " d" };
        yield return new object[] { "d s" };
        yield return new object[] { "dd s" };
        yield return new object[] { "@ s" };
        yield return new object[] { "@" };
        yield return new object[] { "@ " };
        yield return new object[] { "@D" };
        yield return new object[] { "email." };
        yield return new object[] { "email.com" };
        yield return new object[] { "email.@" };
        yield return new object[] { new string('k', 321) };
    }

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