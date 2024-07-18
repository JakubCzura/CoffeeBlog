using AutoMapper;
using FluentAssertions;
using PostManager.Application.Commands.Posts.CreatePost;
using PostManager.Application.Mapping.Posts;
using PostManager.Domain.Entities;

namespace PostManager.Application.UnitTests.Mapping.RequestDetails;

public class PostMappingProfileTests
{
    private readonly IMapper _mapper;

    public PostMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<PostMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapCreatePostCommandToPost()
    {
        //Arrange
        int userId = 1;

        CreatePostCommand createPostCommand = new(
            "Johny",
            "Super movie",
            "Hello it's me, nice to see you"
        );

        //Act
        Post result = _mapper.Map<Post>(createPostCommand, opt =>
        {
            opt.Items[nameof(Post.UserId)] = userId;
        });

        //Assert
        result.Title.Should().Be(createPostCommand.Title);
        result.Subtitle.Should().Be(createPostCommand.Subtitle);
        result.Content.Should().Be(createPostCommand.Content);
        result.UserId.Should().Be(userId);
    }

    [Fact]
    public void Map_should_ThrowAutoMapperMappingException_when_AdditionalPropertiesAreNotSpecified()
    {
        //Arrange
        CreatePostCommand createPostCommand = new(
            "Title",
            "Subtitle",
            "Content"
        );

        //Act
        Func<Post> action = () => _mapper.Map<Post>(createPostCommand);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}