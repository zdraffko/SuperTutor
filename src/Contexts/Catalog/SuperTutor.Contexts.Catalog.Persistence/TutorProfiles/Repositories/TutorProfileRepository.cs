using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;

namespace SuperTutor.Contexts.Catalog.Persistence.TutorProfiles.Repositories;

internal class TutorProfileRepository : ITutorProfileRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;

    public TutorProfileRepository(ITutorProfilesDbContext tutorProfilesDbContext) => this.tutorProfilesDbContext = tutorProfilesDbContext;

    public void Add(TutorProfile tutorProfile) => tutorProfilesDbContext.TutorProfiles.Add(tutorProfile);

}
