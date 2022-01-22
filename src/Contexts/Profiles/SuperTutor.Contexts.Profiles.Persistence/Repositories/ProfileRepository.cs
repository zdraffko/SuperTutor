using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

public  class ProfileRepository : ITutorProfileRepository
{
    private readonly ProfilesDbContext profilesDbContext;

    public ProfileRepository(ProfilesDbContext profilesDbContext)
    {
        this.profilesDbContext = profilesDbContext;
    }

    public void Add(TutorProfile profile) => profilesDbContext.Profiles.Add(profile);

    public async Task<TutorProfile?> GetById(TutorProfileId profileId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles
            .Include(profile => profile.RedactionComments)
            .SingleOrDefaultAsync(profile => profile.Id == profileId, cancellationToken);

    public async Task<IEnumerable<TutorProfile>> GetAllForUser(UserId userId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles
            .Include(profile => profile.RedactionComments)
            .Where(profile => profile.UserId == userId)
            .ToListAsync(cancellationToken);

    public void Remove(TutorProfile profile) => profilesDbContext.Profiles.Remove(profile);
}
