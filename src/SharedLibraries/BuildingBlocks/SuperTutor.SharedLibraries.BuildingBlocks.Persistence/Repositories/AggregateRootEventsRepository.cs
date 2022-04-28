using EventStore.Client;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Repositories;

public class AggregateRootEventsRepository<TAggregateRoot, TAggregateRootIdentifier, TAggregateRootIdentifierValue> : IAggregateRootEventsRepository<TAggregateRoot, TAggregateRootIdentifier, TAggregateRootIdentifierValue>
    where TAggregateRoot : AggregateRoot<TAggregateRootIdentifier, TAggregateRootIdentifierValue>
    where TAggregateRootIdentifier : Identifier<TAggregateRootIdentifierValue>
    where TAggregateRootIdentifierValue : struct
{
    private readonly EventStoreClient eventStoreClient;
    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly Assembly domainEventsAssembly;
    private readonly ConcurrentDictionary<string, Type> domainEventTypesCache;

    public AggregateRootEventsRepository(EventStoreClient eventStoreClient)
    {
        this.eventStoreClient = eventStoreClient;

        jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory());
        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());

        domainEventsAssembly = typeof(TAggregateRoot).Assembly;
        domainEventTypesCache = new();
    }

    public async Task Add(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
    {
        var streamName = GetStreamName(typeof(TAggregateRoot), aggregateRoot.Id);

        var events = aggregateRoot.DomainEvents.Select(domainEvent => SerializeEvent(domainEvent));

        var result = await eventStoreClient.AppendToStreamAsync(
            streamName,
            StreamState.NoStream,
            events,
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
            var domainEvent = DeserializeEvent(resolvedEvent);

            aggregateRoot.ApplyDomainEvent(domainEvent);
        }

        return aggregateRoot;
    }

    private static string GetStreamName(Type aggregateRootType, TAggregateRootIdentifier aggregateRootIdentifier)
        => $"{aggregateRootType.Name}_{aggregateRootIdentifier.Value}";

    private DomainEvent DeserializeEvent(ResolvedEvent resolvedEvent)
    {
        var rawDomainEventMetadata = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.Span);
        var domainEventMetadata = JsonSerializer.Deserialize<DomainEventMetadata>(rawDomainEventMetadata, jsonSerializerOptions);

        var rawDomainEvent = Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span);
        var domainEvent = DeserializeDomainEvent(domainEventMetadata.EventType, rawDomainEvent);

        return domainEvent;
    }

    private EventData SerializeEvent(DomainEvent domainEvent)
    {
        var rawDomainEvent = JsonSerializer.SerializeToUtf8Bytes((dynamic) domainEvent, jsonSerializerOptions);

        var domainEventMetadata = new DomainEventMetadata
        {
            EventType = domainEvent.GetType().AssemblyQualifiedName
        };

        var rawDomainEventMetadata = JsonSerializer.SerializeToUtf8Bytes(domainEventMetadata, jsonSerializerOptions);

        var eventPayload = new EventData(Uuid.NewUuid(), domainEvent.GetType().Name, rawDomainEvent, rawDomainEventMetadata);

        return eventPayload;
    }

    private DomainEvent DeserializeDomainEvent(string type, string data)
    {
        var eventType = domainEventTypesCache.GetOrAdd(type, _ => domainEventsAssembly.GetType(type, false) ?? Type.GetType(type));
        if (eventType is null)
        {
            throw new ArgumentOutOfRangeException(nameof(type), $"Invalid event type: {type}");
        }

        var result = JsonSerializer.Deserialize(data, eventType, jsonSerializerOptions);

        return (DomainEvent) result;
    }
}
