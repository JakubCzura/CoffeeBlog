using AutoMapper;
using FluentAssertions;
using PostManager.Application.Commands.Posts.CreatePost;
using PostManager.Application.ExtensionMethods.Automapper.Posts;
using PostManager.Application.ExtensionMethods.Automapper.RequestDetails;
using PostManager.Application.Mapping.Posts;
using PostManager.Domain.Entities;

namespace PostManager.Application.UnitTests.ExtensionMethods.Automapper.Posts;

public class AutoMapperForPostExtensionsTests
{
    private readonly IMapper _mapper;

    public AutoMapperForPostExtensionsTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<PostMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapCreatePostCommandToPost_when_AdditionalPropertiesAreSpecified()
    {
        //Arrange
        int userId = 1;

        CreatePostCommand createPostCommand = new(
            "Johny",
            "Super movie",
            "Hello it's me, nice to see you"
        );

        //Act
        Post result = _mapper.Map<Post>(createPostCommand, userId);

        //Assert
        result.Title.Should().Be(createPostCommand.Title);
        result.Subtitle.Should().Be(createPostCommand.Subtitle);
        result.Content.Should().Be(createPostCommand.Content);
        result.UserId.Should().Be(userId);
    }
}