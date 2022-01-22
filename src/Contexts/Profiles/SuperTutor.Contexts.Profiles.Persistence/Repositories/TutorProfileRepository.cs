using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

public  class TutorProfileRepository : ITutorProfileRepository
{
    private readonly ProfilesDbContext profilesDbContext;

    public TutorProfileRepository(ProfilesDbContext profilesDbContext)
    {
        this.profilesDbContext = profilesDbContext;
    }

    public void Add(TutorProfile tutorProfile) => profilesDbContext.TutorProfiles.Add(tutorProfile);

    public async Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken)
        => await profilesDbContext.TutorProfiles
            .Include(tutorProfile => tutorProfile.RedactionComments)
            .SingleOrDefaultAsync(tutorProfile => tutorProfile.Id == tutorProfileId, cancellationToken);

    public async Task<IEnumerable<TutorProfile>> GetAllForUser(UserId userId, CancellationToken cancellationToken)
        => await profilesDbContext.TutorProfiles
            .Include(tutorProfile => tutorProfile.RedactionComments)
            .Where(tutorProfile => tutorProfile.UserId == userId)
            .ToListAsync(cancellationToken);

    public void Remove(TutorProfile tutorProfile) => profilesDbContext.TutorProfiles.Remove(tutorProfile);
}
