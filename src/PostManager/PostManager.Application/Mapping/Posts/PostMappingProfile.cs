using AutoMapper;
using PostManager.Application.Commands.Posts.CreatePost;
using PostManager.Domain.Entities;

namespace PostManager.Application.Mapping.Posts;

/// <summary>
/// Mapping profile for <see cref="Post"/>.
/// </summary>
public class PostMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public PostMappingProfile()
    {
        CreateMap<CreatePostCommand, Post>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom((src, dest, destMember, context) => context.Items[nameof(Post.UserId)]))
            .ForMember(dest => dest.PostComments, opt => opt.Ignore());
    }
}