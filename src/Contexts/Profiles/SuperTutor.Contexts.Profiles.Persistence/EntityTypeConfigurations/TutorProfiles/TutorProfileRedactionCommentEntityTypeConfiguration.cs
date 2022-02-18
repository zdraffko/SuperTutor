using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Constants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.TutorProfiles;

internal class TutorProfileRedactionCommentEntityTypeConfiguration : IEntityTypeConfiguration<TutorProfileRedactionComment>
{
    public void Configure(EntityTypeBuilder<TutorProfileRedactionComment> builder)
    {
        builder.ToTable("TutorProfileRedactionComments");

        builder.HasKey(tutorProfileRedactionComment => tutorProfileRedactionComment.Id);

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.Id)
            .HasConversion(
                redactionCommentId => redactionCommentId.Value,
                redactionCommentIdValue => new TutorProfileRedactionCommentId(redactionCommentIdValue))
            .IsRequired();

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.TutorProfileId)
            .HasConversion(
                tutorProfileId => tutorProfileId.Value,
                profileIdValue => new TutorProfileId(profileIdValue))
            .IsRequired();

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.CreatedByAdminId)
            .HasConversion(
                adminId => adminId.Value,
                adminIdValue => new AdminId(adminIdValue))
            .IsRequired();

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.CreationDate).IsRequired();

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.Content)
            .HasMaxLength(TutorProfileRedactionCommentConstants.ContentMaxLength)
            .IsRequired();

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.SettledDate);

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.SettledByAdminId)
            .HasConversion(
                adminId => adminId!.Value,
                adminIdValue => new AdminId(adminIdValue));

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.Status)
            .HasConversion(
                status => status.Value,
                statusValue => Enumeration.FromValue<TutorProfileRedactionCommentStatus>(statusValue)!)
            .IsRequired();

        builder.Property(tutorProfileRedactionComment => tutorProfileRedactionComment.LastUpdateDate).IsRequired();

        builder.Ignore(tutorProfileRedactionComment => tutorProfileRedactionComment.IsSettled);
    }
}
