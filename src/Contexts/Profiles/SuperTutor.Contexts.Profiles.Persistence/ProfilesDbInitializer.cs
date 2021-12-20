using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

namespace SuperTutor.Contexts.Profiles.Persistence;

public class ProfilesDbInitializer : DefaultDbInitializer
{
    public ProfilesDbInitializer(ProfilesDbContext dbContext) : base(dbContext)
    {
    }
}
