using SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Catalog.Domain.Students.Invariants;

public class StudentMustHaveOnlyUniqueFavoriteFiltersInvariant : Invariant
{
    private readonly FavoriteFilter newFavoriteFilter;
    private readonly HashSet<FavoriteFilter> studentsFavoriteFilters;

    public StudentMustHaveOnlyUniqueFavoriteFiltersInvariant(FavoriteFilter newFavoriteFilter, HashSet<FavoriteFilter> studentsFavoriteFilters)
        : base($"The student already has the same filter in his favorites")
    {
        this.newFavoriteFilter = newFavoriteFilter;
        this.studentsFavoriteFilters = studentsFavoriteFilters;
    }

    public override bool IsValid() => !studentsFavoriteFilters.Contains(newFavoriteFilter);
}
