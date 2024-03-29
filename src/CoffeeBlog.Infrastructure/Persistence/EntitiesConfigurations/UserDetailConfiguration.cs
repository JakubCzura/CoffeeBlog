﻿using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

internal class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.ToTable("UserDetail");

        builder.Property(x => x.LastSuccessfullSignIn).IsRequired()
                                                      .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.LastFailedSignIn).IsRequired(false);

        builder.Property(x => x.LastUsernameChange).IsRequired(false);

        builder.Property(x => x.LastPasswordChange).IsRequired(false);

        builder.Property(x => x.LastEmailChange).IsRequired(false);

        builder.Property(x => x.UserId).IsRequired();
    }
}