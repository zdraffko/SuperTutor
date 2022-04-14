using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;
using SuperTutor.Contexts.Catalog.Persistence.FavoriteFilters;

namespace SuperTutor.Contexts.Catalog.Persistence.Shared;

public class CatalogDbContext : DbContext, IFavoriteFilterDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<FavoriteFilter> FavoriteFilters { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
