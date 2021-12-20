using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.Profiles;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

internal  class ProfileRepository : IProfileRepository
{
    private readonly ProfilesDbContext profilesDbContext;

    public ProfileRepository(ProfilesDbContext profilesDbContext)
    {
        this.profilesDbContext = profilesDbContext;
    }

    public void Add(Profile profile) => profilesDbContext.Add(profile);

    public async Task<Profile?> GetById(ProfileId profileId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles.SingleOrDefaultAsync(profile => profile.Id.Value == profileId.Value, cancellationToken);
}
