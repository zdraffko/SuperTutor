using SuperTutor.Contexts.Catalog.Domain.Students.Constants;
using SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Catalog.Domain.Students.Invariants;

public class StudentCanNotHaveMoreThanTheMaximumAllowedFavoriteFiltersInvariant : Invariant
{
    private readonly HashSet<FavoriteFilter> studentsFavoriteFilters;

    public StudentCanNotHaveMoreThanTheMaximumAllowedFavoriteFiltersInvariant(HashSet<FavoriteFilter> studentsFavoriteFilters)
        : base($"The student can not have more than '{StudentConstants.MaximumAllowedFavoriteFilters}' favorite filters") => this.studentsFavoriteFilters = studentsFavoriteFilters;

    public override bool IsValid() => studentsFavoriteFilters.Count <= StudentConstants.MaximumAllowedFavoriteFilters;
}
