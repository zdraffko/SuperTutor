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

        builder.OwnsOne(tutorProfile => tutorProfile.TutoringSubject, ownedBuilder =>
        {
            ownedBuilder.Property(tutoringSubject => tutoringSubject.Value).IsRequired();
            ownedBuilder.Property(tutoringSubject => tutoringSubject.Name).IsRequired();
        });
        builder.Navigation(tutorProfile => tutorProfile.TutoringSubject);

        builder.OwnsMany(tutorProfile => tutorProfile.TutoringGrades, ownedBuilder =>
        {
            ownedBuilder.ToTable("TutorProfileTutoringGrades");

            ownedBuilder.Property(tutoringGrade => tutoringGrade.Value).IsRequired();
            ownedBuilder.Property(tutoringGrade => tutoringGrade.Name).IsRequired();
        });
        builder.Navigation(tutorProfile => tutorProfile.TutoringGrades).Metadata.SetField("tutoringGrades");
        builder.Navigation(tutorProfile => tutorProfile.TutoringGrades).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(tutorProfile => tutorProfile.RateForOneHour)
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(tutorProfile => tutorProfile.IsActive).IsRequired();
    }
}
