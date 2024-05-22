using ArticleManager.Application.ExtensionMethods.ActionContext;
using FluentAssertions;
using Mvc = Microsoft.AspNetCore.Mvc;

namespace ArticleManager.Application.UnitTests.ExtensionMethods.ActionContext;

public class ActionContextExtensionsTests
{
    [Fact]
    public void GetJoinedErrorsMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_when_ErrorsAreSpecified()
    {
        // Arrange
        Mvc.ActionContext actionContext = new();
        actionContext.ModelState.AddModelError("key1", "Error 1");
        actionContext.ModelState.AddModelError("key2", "Error 2");

        // Act
        string result = actionContext.GetJoinedErrorsMessages();

        // Assert
        result.Should().Be("Error 1;Error 2");
    }

    [Fact]
    public void GetJoinedErrorsMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_when_ErrorsAreSpecifiedAndCustomDelimiterIsSpecified()
    {
        // Arrange
        Mvc.ActionContext actionContext = new();
        actionContext.ModelState.AddModelError("key1", "Error 1");
        actionContext.ModelState.AddModelError("key2", "Error 2");

        // Act
        string result = actionContext.GetJoinedErrorsMessages(',');

        // Assert
        result.Should().Be("Error 1,Error 2");
    }

    [Fact]
    public void GetJoinedErrorsMessages_should_ReturnEmptyString_when_ErrorsAreNotSpecified()
    {
        // Arrange
        Mvc.ActionContext actionContext = new();

        // Act
        string result = actionContext.GetJoinedErrorsMessages(',');

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetJoinedErrorsMessages_should_ThrowNullReferenceException_when_ActionContextIsNull()
    {
        // Arrange
        Mvc.ActionContext actionContext = null!;

        // Act
        Action action = () => actionContext.GetJoinedErrorsMessages();

        // Assert
        action.Should().Throw<NullReferenceException>();
    }
}
