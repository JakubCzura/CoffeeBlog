﻿using Microsoft.EntityFrameworkCore;
using PostManager.Domain.Entities;
using System.Reflection;

namespace PostManager.Infrastructure.Persistence.DatabaseContext;

/// <summary>
/// Database context.
/// </summary>
/// <param name="dbContextOptions">Configuration for database context.</param>
public class PostManagerDbContext(DbContextOptions<PostManagerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<ApiError> ApiErrors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}