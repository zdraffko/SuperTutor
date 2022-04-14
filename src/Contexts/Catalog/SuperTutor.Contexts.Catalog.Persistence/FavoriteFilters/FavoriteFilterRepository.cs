using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;

namespace SuperTutor.Contexts.Catalog.Persistence.FavoriteFilters;

internal class FavoriteFilterRepository : IFavoriteFilterRepository
{
    private readonly IFavoriteFilterDbContext favoriteFilterDbContext;

    public FavoriteFilterRepository(IFavoriteFilterDbContext favoriteFilterDbContext) => this.favoriteFilterDbContext = favoriteFilterDbContext;

    public void Add(FavoriteFilter favoriteFilter) => favoriteFilterDbContext.FavoriteFilters.Add(favoriteFilter);
}
