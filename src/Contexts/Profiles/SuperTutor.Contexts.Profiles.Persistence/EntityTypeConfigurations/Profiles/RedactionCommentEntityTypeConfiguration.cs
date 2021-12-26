﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.Constants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.Profiles;

internal class RedactionCommentEntityTypeConfiguration : IEntityTypeConfiguration<RedactionComment>
{
    public void Configure(EntityTypeBuilder<RedactionComment> builder)
    {
        builder.ToTable("RedactionComments");

        builder.HasKey(redactionComment => redactionComment.Id);

        builder.Property(redactionComment => redactionComment.Id)
            .HasConversion(
                redactionCommentId => redactionCommentId.Value,
                redactionCommentIdValue => new RedactionCommentId(redactionCommentIdValue))
            .IsRequired();

        builder.Property(redactionComment => redactionComment.ProfileId)
            .HasConversion(
                profileId => profileId.Value,
                profileIdValue => new ProfileId(profileIdValue))
            .IsRequired();

        builder.Property(redactionComment => redactionComment.CreatedByAdminId)
            .HasConversion(
                adminId => adminId.Value,
                adminIdValue => new AdminId(adminIdValue))
            .IsRequired();

        builder.Property(redactionComment => redactionComment.CreationDate).IsRequired();

        builder.Property(redactionComment => redactionComment.Content)
            .HasMaxLength(RedactionCommentConstants.ContentMaxLength)
            .IsRequired();

        builder.Property(redactionComment => redactionComment.IsSettled).IsRequired();

        builder.Property(redactionComment => redactionComment.SettledDate);

        builder.Property(redactionComment => redactionComment.SettledByAdminId)
            .HasConversion(
                adminId => adminId!.Value,
                adminIdValue => new AdminId(adminIdValue));

        builder.Property(redactionComment => redactionComment.LastUpdateDate).IsRequired();
    }
}
