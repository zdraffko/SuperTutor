using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.Profiles;

namespace SuperTutor.Contexts.Profiles.Persistence;

internal class ProfilesDbContext : DbContext
{
    public DbSet<Profile> Profiles { get; set; }
}
