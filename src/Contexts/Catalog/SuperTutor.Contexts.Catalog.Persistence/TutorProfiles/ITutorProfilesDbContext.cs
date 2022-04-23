using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;

namespace SuperTutor.Contexts.Catalog.Persistence.TutorProfiles;

public interface ITutorProfilesDbContext
{
    public DbSet<TutorProfile> TutorProfiles { get; }
}
