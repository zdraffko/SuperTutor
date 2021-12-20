using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public interface IProfileRepository : IAggregateRootRepository<Profile>
{
    void Add(Profile profile);

    Task<Profile?> GetById(ProfileId profileId, CancellationToken cancellationToken);
}
