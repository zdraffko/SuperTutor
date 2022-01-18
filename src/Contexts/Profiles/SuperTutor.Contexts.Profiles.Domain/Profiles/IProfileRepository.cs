using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public interface IProfileRepository : IAggregateRootRepository<Profile>
{
    void Add(Profile profile);

    Task<Profile?> GetById(ProfileId profileId, CancellationToken cancellationToken);

    Task<IEnumerable<Profile>> GetAllForUser(UserId userId, CancellationToken cancellationToken);

    void Remove(Profile profile);
}
