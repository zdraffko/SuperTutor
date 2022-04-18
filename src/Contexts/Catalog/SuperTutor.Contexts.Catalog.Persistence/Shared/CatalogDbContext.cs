using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Catalog.Persistence.FavoriteFilters;

namespace SuperTutor.Contexts.Catalog.Persistence.Shared;

public class CatalogDbContext : DbContext, IStudentDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
