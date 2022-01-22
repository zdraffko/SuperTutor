using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.Common.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.StudentProfiles;

internal class StudentProfileEntityTypeConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.ToTable("StudentProfiles");

        builder.HasKey(studentProfile => studentProfile.Id);

        builder.Property(studentProfile => studentProfile.Id)
            .HasConversion(
                studentProfileId => studentProfileId.Value,
                studentProfileIdValue => new StudentProfileId(studentProfileIdValue))
            .IsRequired();

        builder.Property(studentProfile => studentProfile.StudentId)
            .HasConversion(
                studentId => studentId.Value,
                studentIdValue => new StudentId(studentIdValue))
            .IsRequired();

        builder.Property(studentProfile => studentProfile.StudyGrade)
            .HasConversion(
                studyGrade => studyGrade.Value,
                studyGradeValue => Enumeration.FromValue<Grade>(studyGradeValue)!)
            .IsRequired();

        var studySubjectsValueComparer = new ValueComparer<HashSet<Subject>>(
                    (studySubjectsOne, studySubjectsTwo) => studySubjectsOne!.SequenceEqual(studySubjectsTwo!),
                    studySubjects => studySubjects.Aggregate(0, (accumulatorValue, studySubject) => HashCode.Combine(accumulatorValue, studySubject.GetHashCode())),
                    studySubjects => studySubjects.ToHashSet());
        builder.Ignore(studentProfile => studentProfile.StudySubjects);
        builder.Property<HashSet<Subject>>("studySubjects")
            .HasColumnName("StudySubjects")
            .HasConversion(
                studySubjects => string.Join(EntityTypeConfigurationConstants.ConversionValueSeparator, studySubjects.Select(studySubject => studySubject.Value)),
                commaSeparatedStudySubjectValues => Enumeration
                    .FromValues<Subject>(
                        commaSeparatedStudySubjectValues
                            .Split(EntityTypeConfigurationConstants.ConversionValueSeparator, StringSplitOptions.RemoveEmptyEntries)
                            .Select(stringStudySubjectValue => int.Parse(stringStudySubjectValue))
                     )
                    .ToHashSet()
            )
            .IsRequired()
            .Metadata.SetValueComparer(studySubjectsValueComparer);

        builder.Property(studentProfile => studentProfile.CreationDate).IsRequired();

        builder.Property(studentProfile => studentProfile.LastUpdateDate).IsRequired();
    }
}
