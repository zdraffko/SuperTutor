using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;

public interface IFavoriteFilterRepository : IAggregateRootRepository<FavoriteFilter>
{
    void Add(FavoriteFilter favoriteFilter);
}
