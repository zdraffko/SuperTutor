using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Constants;

namespace SuperTutor.Contexts.Catalog.Persistence.TutorProfiles.EntityTypeConfigurations;

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

        builder.Property(tutorProfile => tutorProfile.About)
            .HasMaxLength(TutorProfileConstants.AboutMaxLength)
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.TutoringSubject).IsRequired();

        builder.Ignore(tutorProfile => tutorProfile.TutoringGrades);
        builder.Property<List<int>>("tutoringGrades")
            .HasColumnName("TutoringGrades")
            .HasConversion(
                tutoringGrades => string.Join(",", tutoringGrades.Select(tutoringGrade => tutoringGrade)),
                commaSeparatedTutoringGradeValues => commaSeparatedTutoringGradeValues
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(stringTutoringGradeValue => int.Parse(stringTutoringGradeValue)).ToList()
            )
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.RateForOneHour)
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.IsActive).IsRequired();
    }
}
