using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

public interface IAggregateRootRepository<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
{
}
