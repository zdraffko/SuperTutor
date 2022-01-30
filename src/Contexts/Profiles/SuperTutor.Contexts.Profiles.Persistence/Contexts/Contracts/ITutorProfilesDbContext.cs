using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;

namespace SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;

public interface ITutorProfilesDbContext
{
    public DbSet<TutorProfile> TutorProfiles { get; set; }
}
