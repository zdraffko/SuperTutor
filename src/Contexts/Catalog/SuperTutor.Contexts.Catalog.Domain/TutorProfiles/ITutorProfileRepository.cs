using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.TutorProfiles;

public interface ITutorProfileRepository : IAggregateRootRepository<TutorProfile>
{
    void Add(TutorProfile tutorProfile);

    void Remove(TutorProfile tutorProfile);

    Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken);
}
