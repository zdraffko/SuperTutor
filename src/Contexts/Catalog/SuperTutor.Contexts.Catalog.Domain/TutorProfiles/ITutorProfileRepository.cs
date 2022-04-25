using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.TutorProfiles;

public interface ITutorProfileRepository : IAggregateRootRepository<TutorProfile>
{
    void Add(TutorProfile student);

    Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken);
}
