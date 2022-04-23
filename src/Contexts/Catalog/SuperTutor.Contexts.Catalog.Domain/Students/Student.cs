using SuperTutor.Contexts.Catalog.Domain.Students.Invariants;
using SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.Students;

public class Student : Entity<StudentId, Guid>, IAggregateRoot
{
    private readonly HashSet<FavoriteFilter> favoriteFilters;

    public Student(StudentId id) : base(id) => favoriteFilters = new HashSet<FavoriteFilter>();

    public IReadOnlyCollection<FavoriteFilter> FavoriteFilters => favoriteFilters;

    public void AddFavoriteFilter(FavoriteFilter newFavoriteFilter)
    {
        CheckInvariant(new StudentMustHaveOnlyUniqueFavoriteFiltersInvariant(newFavoriteFilter, favoriteFilters));

        favoriteFilters.Add(newFavoriteFilter);

        CheckInvariant(new StudentCanNotHaveMoreThanTheMaximumAllowedFavoriteFiltersInvariant(favoriteFilters));
    }

    public void RemoveFavoriteFilter(string filter)
    {
        var favoriteFilterForRemoval = favoriteFilters.SingleOrDefault(favoriteFilter => favoriteFilter.Filter == filter);
        if (favoriteFilterForRemoval is null)
        {
            return;
        }

        favoriteFilters.Remove(favoriteFilterForRemoval);
    }
}
