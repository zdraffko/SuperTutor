using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.TutorProfiles;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.TutorProfiles.Repositories;

internal class TutorProfileRepository : ITutorProfileRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;

    public TutorProfileRepository(ITutorProfilesDbContext tutorProfilesDbContext) => this.tutorProfilesDbContext = tutorProfilesDbContext;

    public void Add(TutorProfile tutorProfile) => tutorProfilesDbContext.TutorProfiles.Add(tutorProfile);

    public void Remove(TutorProfile tutorProfile) => tutorProfilesDbContext.TutorProfiles.Remove(tutorProfile);

    public async Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken)
        => await tutorProfilesDbContext.TutorProfiles.SingleOrDefaultAsync(tutorProfile => tutorProfile.Id == tutorProfileId, cancellationToken);
}
