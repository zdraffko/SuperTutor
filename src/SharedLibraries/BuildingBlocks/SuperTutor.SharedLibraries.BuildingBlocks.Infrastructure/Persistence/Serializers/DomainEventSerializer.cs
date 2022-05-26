using EventStore.Client;
using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

public class DomainEventSerializer : IDomainEventSerializer
{
    private readonly Assembly domainEventsAssembly;
    private readonly ConcurrentDictionary<string, Type> domainEventTypesCache;
    private readonly JsonSerializerOptions jsonSerializerOptions;

    public DomainEventSerializer(Assembly domainEventsAssembly)
    {
        this.domainEventsAssembly = domainEventsAssembly;
        domainEventTypesCache = new();

        jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory());
        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    }

    public Result<DomainEvent> Deserialize(ResolvedEvent resolvedEvent)
    {
        try
        {
            var rawDomainEventMetadata = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.Span);
            var domainEventMetadata = JsonSerializer.Deserialize<DomainEventMetadata>(rawDomainEventMetadata, jsonSerializerOptions);

            if (domainEventMetadata is null)
            {
                return Result.Fail($"Could not deserialize domain event metadata. Raw metadata {rawDomainEventMetadata}");
            }

            var rawDomainEvent = Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span);

            var domainEventTypeFromDomainEventsAssembly = domainEventsAssembly.GetType(domainEventMetadata.EventType, false)!;
            var domainEventType = Type.GetType(domainEventMetadata.EventType)!;

            var eventType = domainEventTypesCache.GetOrAdd(
                domainEventMetadata.EventType,
                _ => domainEventTypeFromDomainEventsAssembly ??
                    domainEventType);

            if (eventType is null)
            {
                return Result.Fail($"Invalid event type received for deserialization: {domainEventMetadata.EventType}");
            }

            var domainEventObject = JsonSerializer.Deserialize(rawDomainEvent, eventType, jsonSerializerOptions);
            if (domainEventObject is null)
            {
                return Result.Fail($"Could not deserialize domain event. Raw domain event {rawDomainEvent}");
            }

            return Result.Ok((DomainEvent) domainEventObject);
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public Result<EventData> Serialize(DomainEvent domainEvent)
    {
        try
        {
            var rawDomainEvent = JsonSerializer.SerializeToUtf8Bytes((dynamic) domainEvent, jsonSerializerOptions);

            var eventType = domainEvent.GetType().AssemblyQualifiedName;
            if (eventType is null)
            {
                return Result.Fail("Could not get the type of the domain event.");
            }

            var domainEventMetadata = new DomainEventMetadata
            {
                EventType = eventType
            };

            var rawDomainEventMetadata = JsonSerializer.SerializeToUtf8Bytes(domainEventMetadata, jsonSerializerOptions);

            var eventData = new EventData(Uuid.NewUuid(), domainEvent.GetType().Name, rawDomainEvent, rawDomainEventMetadata);

            return Result.Ok(eventData);
        }
        catch (Exception exception)
        {
            return Result.Fail<EventData>(exception.Message);
        }
    }
}
