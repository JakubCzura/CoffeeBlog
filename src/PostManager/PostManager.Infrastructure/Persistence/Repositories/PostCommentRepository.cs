﻿using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Domain.Entities;
using PostManager.Infrastructure.Persistence.DatabaseContext;

namespace PostManager.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="PostComment"/>.
/// </summary>
/// <param name="_postManagerDbContext">Database context.</param>
internal class PostCommentRepository(PostManagerDbContext _postManagerDbContext)
    : BaseRepository<PostComment>(_postManagerDbContext), IPostCommentRepository
{
}