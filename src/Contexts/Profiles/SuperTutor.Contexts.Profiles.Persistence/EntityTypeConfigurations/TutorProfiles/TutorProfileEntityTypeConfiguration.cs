using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Constants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.Common.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.TutorProfiles;

internal class TutorProfileEntityTypeConfiguration : IEntityTypeConfiguration<TutorProfile>
{
    public void Configure(EntityTypeBuilder<TutorProfile> builder)
    {
        builder.ToTable("TutorProfiles");

        builder.HasKey(tutorProfile => tutorProfile.Id);

        builder.Property(tutorProfile => tutorProfile.Id)
            .HasConversion(
                tutorProfileId => tutorProfileId.Value,
                tutorProfileIdValue => new TutorProfileId(tutorProfileIdValue))
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.TutorId)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.About)
            .HasMaxLength(TutorProfileConstants.AboutMaxLength)
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.TutoringSubject)
            .HasConversion(
                tutoringSubject => tutoringSubject.Value,
                tutoringSubjectValue => Enumeration.FromValue<Subject>(tutoringSubjectValue)!)
            .IsRequired();

        var tutoringGradesValueComparer = new ValueComparer<HashSet<Grade>>(
                    (tutoringGradesOne, tutoringGradesTwo) => tutoringGradesOne!.SequenceEqual(tutoringGradesTwo!),
                    tutoringGrades => tutoringGrades.Aggregate(0, (accumulatorValue, tutoringGrade) => HashCode.Combine(accumulatorValue, tutoringGrade.GetHashCode())),
                    tutoringGrades => tutoringGrades.ToHashSet());
        builder.Ignore(tutorProfile => tutorProfile.TutoringGrades);
        builder.Property<HashSet<Grade>>("tutoringGrades")
            .HasColumnName("TutoringGrades")
            .HasConversion(
                tutoringGrades => string.Join(EntityTypeConfigurationConstants.ConversionValueSeparator, tutoringGrades.Select(tutoringGrade => tutoringGrade.Value)),
                commaSeparatedTutoringGradeValues => Enumeration
                    .FromValues<Grade>(
                        commaSeparatedTutoringGradeValues
                            .Split(EntityTypeConfigurationConstants.ConversionValueSeparator, StringSplitOptions.RemoveEmptyEntries)
                            .Select(stringTutoringGradeValue => int.Parse(stringTutoringGradeValue))
                     )
                    .ToHashSet()
            )
            .IsRequired()
            .Metadata.SetValueComparer(tutoringGradesValueComparer);

        builder.Property(tutorProfile => tutorProfile.RateForOneHour)
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.Status)
            .HasConversion(
                status => status.Value,
                statusValue => Enumeration.FromValue<TutorProfileStatus>(statusValue)!)
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.CreationDate).IsRequired();

        builder.Property(tutorProfile => tutorProfile.LastUpdateDate).IsRequired();

        builder.Property(tutorProfile => tutorProfile.LastApprovalDate);

        builder.Property(tutorProfile => tutorProfile.LastApprovalAdminId)
            .HasConversion(
                adminId => adminId!.Value,
                adminIdValue => new AdminId(adminIdValue));

        builder.Property(tutorProfile => tutorProfile.LastModificationDate);

        builder.Property(tutorProfile => tutorProfile.LastRedactionRequestDate);

        builder.Property(tutorProfile => tutorProfile.LastRedactionRequestAdminId)
            .HasConversion(
                adminId => adminId!.Value,
                adminIdValue => new AdminId(adminIdValue));

        builder.HasMany(tutorProfile => tutorProfile.RedactionComments)
            .WithOne()
            .HasForeignKey(redactionComment => redactionComment.TutorProfileId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.Navigation(tutorProfile => tutorProfile.RedactionComments).HasField("redactionComments");
    }
}
