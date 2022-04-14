using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;

namespace SuperTutor.Contexts.Catalog.Persistence.FavoriteFilters;

public interface IFavoriteFilterDbContext
{
    public DbSet<FavoriteFilter> FavoriteFilters { get; }
}
