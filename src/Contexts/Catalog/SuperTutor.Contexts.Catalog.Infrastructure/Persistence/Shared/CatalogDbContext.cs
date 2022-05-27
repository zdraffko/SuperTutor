using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.Tutors;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Students;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.TutorProfiles;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Tutors;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Shared;

public class CatalogDbContext : DbContext, IStudentsDbContext, ITutorProfilesDbContext, ITutorsDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; } = default!;

    public DbSet<TutorProfile> TutorProfiles { get; set; } = default!;

    public DbSet<Tutor> Tutors { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
