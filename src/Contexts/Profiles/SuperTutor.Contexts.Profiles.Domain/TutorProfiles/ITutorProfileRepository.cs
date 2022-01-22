using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles;

public interface ITutorProfileRepository : IAggregateRootRepository<TutorProfile>
{
    void Add(TutorProfile tutorProfile);

    Task<TutorProfile?> GetById(TutorProfileId tutorProfileId, CancellationToken cancellationToken);

    Task<IEnumerable<TutorProfile>> GetAllForTutor(TutorId tutorId, CancellationToken cancellationToken);

    void Remove(TutorProfile tutorProfile);
}
