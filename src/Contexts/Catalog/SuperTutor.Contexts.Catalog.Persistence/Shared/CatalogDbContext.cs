using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Persistence.Students;
using SuperTutor.Contexts.Catalog.Persistence.TutorProfiles;

namespace SuperTutor.Contexts.Catalog.Persistence.Shared;

public class CatalogDbContext : DbContext, IStudentsDbContext, ITutorProfilesDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; } = default!;

    public DbSet<TutorProfile> TutorProfiles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
