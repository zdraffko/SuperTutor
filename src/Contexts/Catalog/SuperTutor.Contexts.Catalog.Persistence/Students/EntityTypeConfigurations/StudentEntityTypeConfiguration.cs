using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Catalog.Domain.Students;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.StudentProfiles;

internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(student => student.Id);

        builder.Property(student => student.Id)
            .HasConversion(
                studentId => studentId.Value,
                studentIdValue => new StudentId(studentIdValue))
            .IsRequired();

        builder.OwnsMany(student => student.FavoriteFilters, ownedBuilder =>
        {
            ownedBuilder.ToTable("FavoriteFilters");

            ownedBuilder.Property(favoriteFilter => favoriteFilter.StudentId)
                .HasConversion(
                    studentId => studentId.Value,
                    studentIdValue => new StudentId(studentIdValue))
                .IsRequired();

            ownedBuilder.Property(favoriteFilter => favoriteFilter.Filter).IsRequired();
        });

        builder.Navigation(student => student.FavoriteFilters).Metadata.SetField("favoriteFilters");
        builder.Navigation(student => student.FavoriteFilters).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
