using EventStore.Client;
using System.Text;
using System.Text.Json;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Repositories;

public class EventStoreSubscriptionCheckpointRepository : IEventStoreSubscriptionCheckpointRepository
{
    private readonly EventStoreClient eventStoreClient;

    public EventStoreSubscriptionCheckpointRepository(EventStoreClient eventStoreClient) => this.eventStoreClient = eventStoreClient;

    public async Task<ulong?> Load(string subscriptionId, CancellationToken cancellationToken)
    {
        var readStreamResult = eventStoreClient.ReadStreamAsync(
            Direction.Backwards,
            GetCheckpointStreamName(subscriptionId),
            StreamPosition.End,
            maxCount: 1,
            cancellationToken: cancellationToken);

        if (await readStreamResult.ReadState == ReadState.StreamNotFound)
        {
            return null;
        }

        var resolvedEvent = await readStreamResult.SingleOrDefaultAsync(cancellationToken);
        var rawCheckpointCreatedEvent = Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span);
        var checkpointCreatedEvent = JsonSerializer.Deserialize<CheckpointCreatedEvent>(rawCheckpointCreatedEvent);

        return checkpointCreatedEvent?.Position;
    }

    public async Task Store(string subscriptionId, ulong position, CancellationToken cancellationToken)
    {
        var checkpointCreatedEvent = new CheckpointCreatedEvent(position);
        var rawCheckpointCreatedEvent = JsonSerializer.SerializeToUtf8Bytes(checkpointCreatedEvent);
        var eventData = new EventData(Uuid.NewUuid(), typeof(CheckpointCreatedEvent).Name, rawCheckpointCreatedEvent);

        try
        {
            await eventStoreClient.AppendToStreamAsync(
                GetCheckpointStreamName(subscriptionId),
                StreamState.StreamExists,
                new List<EventData> { eventData },
                cancellationToken: cancellationToken);
        }
        catch (WrongExpectedVersionException) // If we enter this catch block it means that the stream did not exist
        {
            // Set the checkpoint stream to have at most 1 event because we only need the last state of the checkpoint
            await eventStoreClient.SetStreamMetadataAsync(
                GetCheckpointStreamName(subscriptionId),
                StreamState.NoStream,
                new StreamMetadata(1),
                cancellationToken: cancellationToken);

            // Try to append the event again but this time we are expecting the stream to not exist
            await eventStoreClient.AppendToStreamAsync(
                GetCheckpointStreamName(subscriptionId),
                StreamState.NoStream,
                new List<EventData> { eventData },
                cancellationToken: cancellationToken);
        }
    }

    private static string GetCheckpointStreamName(string subscriptionId) => $"{subscriptionId}_checkpoint";

    public class CheckpointCreatedEvent
    {
        public CheckpointCreatedEvent(ulong position)
        {
            Position = position;
            OccurredOn = DateTime.UtcNow;
        }

        public ulong Position { get; }

        public DateTime OccurredOn { get; }
    }
}
