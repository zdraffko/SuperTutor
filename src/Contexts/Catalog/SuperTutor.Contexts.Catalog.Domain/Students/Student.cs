using SuperTutor.Contexts.Catalog.Domain.Students.Invariants;
using SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.Students;

public class Student : Entity<StudentId, Guid>, IAggregateRoot
{
    private readonly HashSet<FavoriteFilter> favoriteFilters;

    public Student() : base(new StudentId(Guid.NewGuid())) => favoriteFilters = new HashSet<FavoriteFilter>();

    public IReadOnlyCollection<FavoriteFilter> FavoriteFilters => favoriteFilters;

    public void AddFavoriteFilter(FavoriteFilter newFavoriteFilter)
    {
        CheckInvariant(new StudentMustHaveOnlyUniqueFavoriteFiltersInvariant(newFavoriteFilter, favoriteFilters));

        favoriteFilters.Add(newFavoriteFilter);

        CheckInvariant(new StudentCanNotHaveMoreThanTheMaximumAllowedFavoriteFiltersInvariant(favoriteFilters));
    }
}
