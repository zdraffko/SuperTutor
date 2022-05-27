using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Catalog.Domain.Tutors;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Tutors.EntityTypeConfigurations;

internal class TutorEntityTypeConfiguration : IEntityTypeConfiguration<Tutor>
{
    public void Configure(EntityTypeBuilder<Tutor> builder)
    {
        builder.ToTable("Tutors");

        builder.HasKey(tutor => tutor.Id);

        builder.Property(tutor => tutor.Id)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(tutor => tutor.FirstName).IsRequired();

        builder.Property(tutor => tutor.LastName).IsRequired();
    }
}
