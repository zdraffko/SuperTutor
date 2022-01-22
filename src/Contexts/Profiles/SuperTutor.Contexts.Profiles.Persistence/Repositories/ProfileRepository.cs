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

    public void Add(TutorProfile tutorProfile) => profilesDbContext.Profiles.Add(tutorProfile);

    public async Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles
            .Include(tutorProfile => tutorProfile.RedactionComments)
            .SingleOrDefaultAsync(tutorProfile => tutorProfile.Id == tutorProfileId, cancellationToken);

    public async Task<IEnumerable<TutorProfile>> GetAllForUser(UserId userId, CancellationToken cancellationToken)
        => await profilesDbContext.Profiles
            .Include(tutorProfile => tutorProfile.RedactionComments)
            .Where(tutorProfile => tutorProfile.UserId == userId)
            .ToListAsync(cancellationToken);

    public void Remove(TutorProfile tutorProfile) => profilesDbContext.Profiles.Remove(tutorProfile);
}
