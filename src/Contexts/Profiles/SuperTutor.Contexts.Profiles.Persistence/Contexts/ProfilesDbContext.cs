using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;

namespace SuperTutor.Contexts.Profiles.Persistence.Contexts;

public class ProfilesDbContext : DbContext, ITutorProfilesDbContext, IStudentProfilesDbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options) { }

    public DbSet<TutorProfile> TutorProfiles { get; set; } = default!;

    public DbSet<TutorProfileRedactionComment> RedactionComments { get; set; } = default!;

    public DbSet<StudentProfile> StudentProfiles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("profiles");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
