using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;
using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Persistence.EntityTypeConfigurations.StudentProfiles;

internal class FavoriteFilterEntityTypeConfiguration : IEntityTypeConfiguration<FavoriteFilter>
{
    public void Configure(EntityTypeBuilder<FavoriteFilter> builder)
    {
        builder.ToTable("FavoriteFilters");

        builder.HasKey(favoriteFilter => favoriteFilter.Id);

        builder.Property(favoriteFilter => favoriteFilter.Id)
            .HasConversion(
                favoriteFilterId => favoriteFilterId.Value,
                favoriteFilterIdValue => new FavoriteFilterId(favoriteFilterIdValue))
            .IsRequired();

        builder.Property(favoriteFilter => favoriteFilter.StudentId)
            .HasConversion(
                studentId => studentId.Value,
                studentIdValue => new StudentId(studentIdValue))
            .IsRequired();

        builder.Property(favoriteFilter => favoriteFilter.CreationDate).IsRequired();
    }
}
