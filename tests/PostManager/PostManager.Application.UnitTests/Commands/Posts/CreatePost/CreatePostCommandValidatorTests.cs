using FluentValidation.TestHelper;
using PostManager.Application.Commands.Posts.CreatePost;
using PostManager.Domain.Resources;

namespace PostManager.Application.UnitTests.Commands.Posts.CreatePost;

public class CreatePostCommandValidatorTests
{
    private readonly CreatePostCommandValidator _createPostCommandValidator = new();

    [Fact]
    public void Validate_should_Pass_when_CommandIsCorrect()
    {
        //Arrange
        CreatePostCommand command = new(
            "My dog",
            "My dog is very cute",
            "Hello, my dog is very cute. I love him very much."
        );

        //Act
        TestValidationResult<CreatePostCommand> result = _createPostCommandValidator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_should_Fail_when_TitleIsIncorrect()
    {
        //Arrange
        CreatePostCommand command = new(
            new string('k', 101),
            "My dog is very cute",
            "Hello, my dog is very cute. I love him very much."
        );

        //Act
        TestValidationResult<CreatePostCommand> result = _createPostCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
              .WithErrorMessage(ValidatorMessages.TitleCantContainMoreThan100Characters);
    }

    [Fact]
    public void Validate_should_Fail_when_SubtitleIsIncorrect()
    {
        //Arrange
        CreatePostCommand command = new(
            "My dog",
            new string('k', 101),
            "Hello, my dog is very cute. I love him very much."
        );

        //Act
        TestValidationResult<CreatePostCommand> result = _createPostCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Subtitle)
              .WithErrorMessage(ValidatorMessages.SubtitleCantContainMoreThan100Characters);
    }

    [Fact]
    public void Validate_should_Fail_when_ContentIsIncorrect()
    {
        //Arrange
        CreatePostCommand command = new(
            "My dog",
            "My dog is very cute",
            new string('k', 5001)
        );

        //Act
        TestValidationResult<CreatePostCommand> result = _createPostCommandValidator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Content)
              .WithErrorMessage(ValidatorMessages.ContentCantContainMoreThan5000Characters);
    }
}