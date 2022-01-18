using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

public  class ProfileRepository : IProfileRepository
{
    private readonly ProfilesDbContext profilesDbContext;

    public ProfileRepository(ProfilesDbContext profilesDbContext)
    {
        this.profilesDbContext = profilesDbContext;
    }

    public void Add(Profile profile) => profilesDbContext.Profiles.Add(profile);

    public async Task<Profile?> GetById(ProfileId profileId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles
            .Include(profile => profile.RedactionComments)
            .SingleOrDefaultAsync(profile => profile.Id == profileId, cancellationToken);

    public async Task<IEnumerable<Profile>> GetAllForUser(UserId userId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles
            .Include(profile => profile.RedactionComments)
            .Where(profile => profile.UserId == userId)
            .ToListAsync(cancellationToken);

    public void Remove(Profile profile) => profilesDbContext.Profiles.Remove(profile);
}
