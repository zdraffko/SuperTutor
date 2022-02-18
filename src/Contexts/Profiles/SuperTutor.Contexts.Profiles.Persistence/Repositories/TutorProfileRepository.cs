using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

public class TutorProfileRepository : ITutorProfileRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;

    public TutorProfileRepository(ITutorProfilesDbContext tutorProfilesDbContext) => this.tutorProfilesDbContext = tutorProfilesDbContext;

    public void Add(TutorProfile tutorProfile) => tutorProfilesDbContext.TutorProfiles.Add(tutorProfile);

    public async Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken)
        => await tutorProfilesDbContext.TutorProfiles
            .Include(tutorProfile => tutorProfile.RedactionComments)
            .SingleOrDefaultAsync(tutorProfile => tutorProfile.Id == tutorProfileId, cancellationToken);

    public async Task<IEnumerable<TutorProfile>> GetAllForTutor(TutorId tutorId, CancellationToken cancellationToken)
        => await tutorProfilesDbContext.TutorProfiles
            .Include(tutorProfile => tutorProfile.RedactionComments)
            .Where(tutorProfile => tutorProfile.TutorId == tutorId)
            .ToListAsync(cancellationToken);

    public void Remove(TutorProfile tutorProfile) => tutorProfilesDbContext.TutorProfiles.Remove(tutorProfile);
}
