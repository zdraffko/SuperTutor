using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Constants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.Profiles;

internal class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<Profile>
{
    private const string Comma = ",";

    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.HasKey(profile => profile.Id);

        builder.Property(profile => profile.Id)
            .HasConversion(
                profileId => profileId.Value,
                profileIdValue => new ProfileId(profileIdValue))
            .IsRequired();

        builder.Property(profile => profile.UserId)
            .HasConversion(
                userId => userId.Value,
                userIdValue => new UserId(userIdValue))
            .IsRequired();

        builder.Property(profile => profile.About)
            .HasMaxLength(ProfileConstants.AboutMaxLength)
            .IsRequired();

        builder.Property(profile => profile.TutoringSubject)
            .HasConversion(
                tutoringSubject => tutoringSubject.Value,
                tutoringSubjectValue => Enumeration.FromValue<TutoringSubject>(tutoringSubjectValue)!)
            .IsRequired();

        var tutoringGradesValueComparer = new ValueComparer<HashSet<TutoringGrade>>(
                    (tutoringGradesOne, tutoringGradesTwo) => tutoringGradesOne!.SequenceEqual(tutoringGradesTwo!),
                    tutoringGrades => tutoringGrades.Aggregate(0, (accumulatorValue, tutoringGrade) => HashCode.Combine(accumulatorValue, tutoringGrade.GetHashCode())),
                    tutoringGrades => tutoringGrades.ToHashSet());
        builder.Ignore(profile => profile.TutoringGrades);
        builder.Property<HashSet<TutoringGrade>>("tutoringGrades")
            .HasColumnName("TutoringGrades")
            .HasConversion(
                tutoringGrades => string.Join(Comma, tutoringGrades.Select(tutoringGrade => tutoringGrade.Value)),
                commaSeparatedTutoringGradeValues => Enumeration
                    .FromValues<TutoringGrade>(
                        commaSeparatedTutoringGradeValues
                            .Split(Comma, StringSplitOptions.RemoveEmptyEntries)
                            .Select(stringTutoringGradeValue => int.Parse(stringTutoringGradeValue))
                     )
                    .ToHashSet()
            )
            .IsRequired()
            .Metadata.SetValueComparer(tutoringGradesValueComparer);
            
        builder.Property(profile => profile.RateForOneHour)
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(profile => profile.Status)
            .HasConversion(
                status => status.Value,
                statusValue => Enumeration.FromValue<Status>(statusValue)!)
            .IsRequired();

        builder.Property(profile => profile.CreationDate).IsRequired();

        builder.Property(profile => profile.LastUpdateDate).IsRequired();

        builder.Property(profile => profile.LastApprovalDate);

        builder.Property(profile => profile.LastApprovalAdminId)
            .HasConversion(
                adminId => adminId!.Value,
                adminIdValue => new AdminId(adminIdValue));

        builder.Property(profile => profile.LastModificationDate);

        builder.Property(profile => profile.LastRedactionRequestDate);

        builder.Property(profile => profile.LastRedactionRequestAdminId)
            .HasConversion(
                adminId => adminId!.Value,
                adminIdValue => new AdminId(adminIdValue));

        builder.HasMany(profile => profile.RedactionComments)
            .WithOne()
            .HasForeignKey(redactionComment => redactionComment.ProfileId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.Navigation(profile => profile.RedactionComments).HasField("redactionComments");
    }
}
