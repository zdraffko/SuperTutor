using EventStore.Client;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;

public class AggregateRootEventsRepository<TAggregateRoot, TAggregateRootIdentifier, TAggregateRootIdentifierValue> : IAggregateRootEventsRepository<TAggregateRoot, TAggregateRootIdentifier, TAggregateRootIdentifierValue>
    where TAggregateRoot : AggregateRoot<TAggregateRootIdentifier, TAggregateRootIdentifierValue>
    where TAggregateRootIdentifier : Identifier<TAggregateRootIdentifierValue>
    where TAggregateRootIdentifierValue : struct
{
    private readonly EventStoreClient eventStoreClient;
    private readonly IDomainEventSerializer domainEventSerializer;

    public AggregateRootEventsRepository(EventStoreClient eventStoreClient, IDomainEventSerializer domainEventSerializer)
    {
        this.eventStoreClient = eventStoreClient;
        this.domainEventSerializer = domainEventSerializer;
    }

    public async Task Add(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
    {
        if (aggregateRoot.DomainEvents.Count == 0)
        {
            return;
        }

        var streamName = GetStreamName(typeof(TAggregateRoot), aggregateRoot.Id);

        var eventsData = new List<EventData>();
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            var serializationResult = domainEventSerializer.Serialize(domainEvent);
            if (serializationResult.IsSuccess)
            {
                eventsData.Add(serializationResult.Value);
            }
        }

        await eventStoreClient.AppendToStreamAsync(
            streamName,
            StreamState.NoStream,
            eventsData,
            cancellationToken: cancellationToken
        );
    }

    public async Task<TAggregateRoot?> Load(TAggregateRootIdentifier aggregateRootIdentifier, CancellationToken cancellationToken)
    {
        var streamName = GetStreamName(typeof(TAggregateRoot), aggregateRootIdentifier);

        var readStreamResult = eventStoreClient.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start, cancellationToken: cancellationToken);
        if (await readStreamResult.ReadState == ReadState.StreamNotFound)
        {
            return null;
        }

        if (Activator.CreateInstance(typeof(TAggregateRoot), true) is not TAggregateRoot aggregateRoot)
        {
            return null;
        }

        await foreach (var resolvedEvent in readStreamResult)
        {
            var deserializationResult = domainEventSerializer.Deserialize(resolvedEvent);
            if (deserializationResult.IsSuccess)
            {
                aggregateRoot.ApplyDomainEvent(deserializationResult.Value);
            }
        }

        return aggregateRoot;
    }

    public async Task Update(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
    {
        if (aggregateRoot.DomainEvents.Count == 0)
        {
            return;
        }

        var streamName = GetStreamName(typeof(TAggregateRoot), aggregateRoot.Id);

        var eventsData = new List<EventData>();
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            var serializationResult = domainEventSerializer.Serialize(domainEvent);
            if (serializationResult.IsSuccess)
            {
                eventsData.Add(serializationResult.Value);
            }
        }

        await eventStoreClient.AppendToStreamAsync(
            streamName,
            StreamState.StreamExists,
            eventsData,
            cancellationToken: cancellationToken
        );
    }

    private static string GetStreamName(Type aggregateRootType, TAggregateRootIdentifier aggregateRootIdentifier)
        => $"supertutor_{aggregateRootType.Name}-{aggregateRootIdentifier.Value}";
}
