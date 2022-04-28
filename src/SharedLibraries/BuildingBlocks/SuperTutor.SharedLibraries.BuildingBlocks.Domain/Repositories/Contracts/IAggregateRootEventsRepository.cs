using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

public interface IAggregateRootEventsRepository<TAggregateRoot, TAggregateRootIdentifier, TAggregateRootIdentifierValue>
    where TAggregateRoot : AggregateRoot<TAggregateRootIdentifier, TAggregateRootIdentifierValue>
    where TAggregateRootIdentifier : Identifier<TAggregateRootIdentifierValue>
    where TAggregateRootIdentifierValue : struct
{
    Task Add(TAggregateRoot aggregateRoot, CancellationToken cancellationToken);

    Task<TAggregateRoot?> Load(TAggregateRootIdentifier aggregateRootIdentifier, CancellationToken cancellationToken);
}
