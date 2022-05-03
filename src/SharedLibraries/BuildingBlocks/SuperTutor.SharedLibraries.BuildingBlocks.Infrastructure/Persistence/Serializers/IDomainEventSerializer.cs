using EventStore.Client;
using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

public interface IDomainEventSerializer
{
    Result<DomainEvent> Deserialize(ResolvedEvent resolvedEvent);

    Result<EventData> Serialize(DomainEvent domainEvent);
}
