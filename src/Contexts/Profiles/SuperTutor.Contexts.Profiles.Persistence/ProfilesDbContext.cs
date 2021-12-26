using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments;

namespace SuperTutor.Contexts.Profiles.Persistence;

public class ProfilesDbContext : DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; } = default!;

    public DbSet<RedactionComment> RedactionComments { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("profiles");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
