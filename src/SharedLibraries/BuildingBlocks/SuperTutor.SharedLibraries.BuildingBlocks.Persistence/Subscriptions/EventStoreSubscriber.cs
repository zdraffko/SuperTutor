using EventStore.Client;
using FluentResults;
using Microsoft.Extensions.Logging;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Serializers;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Subscriptions;

public class EventStoreSubscriber : IEventStoreSubscriber
{
    private readonly EventStoreClient eventStoreClient;
    private readonly IEventStoreSubscriptionCheckpointRepository eventStoreSubscriptionCheckpointRepository;
    private readonly IDomainEventDispatcher domainEventDispatcher;
    private readonly IDomainEventSerializer domainEventSerializer;
    private readonly ILogger<EventStoreSubscriber> logger;

    private CancellationToken cancellationToken;
    private string subscriptionId;

    public EventStoreSubscriber(
        EventStoreClient eventStoreClient,
        IEventStoreSubscriptionCheckpointRepository eventStoreSubscriptionCheckpointRepository,
        IDomainEventDispatcher domainEventDispatcher,
        IDomainEventSerializer domainEventSerializer,
        ILogger<EventStoreSubscriber> logger)
    {
        this.eventStoreClient = eventStoreClient;
        this.eventStoreSubscriptionCheckpointRepository = eventStoreSubscriptionCheckpointRepository;
        this.domainEventDispatcher = domainEventDispatcher;
        this.domainEventSerializer = domainEventSerializer;
        this.logger = logger;

        cancellationToken = new CancellationTokenSource().Token;
        subscriptionId = string.Empty;
    }

    public async Task<Result> SubscribeToAll(string subscriptionId, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Subscribing to Event Store All Stream with subscription Id '{EventStoreSubscriptionId}'", subscriptionId);

            this.subscriptionId = subscriptionId;
            this.cancellationToken = cancellationToken;

            var checkpoint = await eventStoreSubscriptionCheckpointRepository.Load(subscriptionId, cancellationToken);

            await eventStoreClient.SubscribeToAllAsync(
                checkpoint is null ? FromAll.Start : FromAll.After(new Position(checkpoint.Value, checkpoint.Value)),
                HandleEvent,
                false,
                HandleDrop,
                filterOptions: new SubscriptionFilterOptions(StreamFilter.Prefix("supertutor_")),
                cancellationToken: cancellationToken
            );

            logger.LogInformation("Subscribed to Event Store All Stream with subscription Id '{EventStoreSubscriptionId}'", subscriptionId);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            logger.LogCritical(exception, "Failed to subscribe to Event Store All Stream with subscription Id '{EventStoreSubscriptionId}'", subscriptionId);

            return Result.Fail("Subscription failed");
        }
    }

    private async Task HandleEvent(StreamSubscription _, ResolvedEvent resolvedEvent, CancellationToken cancellationToken)
    {
        try
        {
            if (resolvedEvent.Event.Data.Length == 0)
            {
                await eventStoreSubscriptionCheckpointRepository.Store(subscriptionId, resolvedEvent.Event.Position.CommitPosition, cancellationToken);

                return;
            }

            var deserializationResult = domainEventSerializer.Deserialize(resolvedEvent);
            if (deserializationResult.IsFailed)
            {
                await eventStoreSubscriptionCheckpointRepository.Store(subscriptionId, resolvedEvent.Event.Position.CommitPosition, cancellationToken);

                return;
            }

            await domainEventDispatcher.Dispatch((dynamic) deserializationResult.Value, cancellationToken);
            await eventStoreSubscriptionCheckpointRepository.Store(subscriptionId, resolvedEvent.Event.Position.CommitPosition, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unexpected error has occurred while handling event");
        }
    }

    private void HandleDrop(StreamSubscription _, SubscriptionDroppedReason reason, Exception? exception)
    {
        logger.LogError(exception, "Subscription with Id '{EventStoreSubscriptionId}' was dropped with reason '{EventStoreSubscriptionDropReason}'", subscriptionId, reason);

        Resubscribe().GetAwaiter().GetResult();
    }

    private async Task Resubscribe()
    {
        var resubscriptionAttemptNumber = 0;

        while (true)
        {
            resubscriptionAttemptNumber++;

            logger.LogInformation("Resubscription attempt number '{EventStoreResubscriptionAttemptNumber}' for subscription Id '{EventStoreSubscriptionId}'", resubscriptionAttemptNumber, subscriptionId);

            try
            {
                var subscriptionResult = await SubscribeToAll(subscriptionId, cancellationToken).ConfigureAwait(false);
                if (subscriptionResult.IsSuccess)
                {
                    return;
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Resubscription attempt number '{EventStoreResubscriptionAttemptNumber}' for subscription Id '{EventStoreSubscriptionId}' failed, retrying after {EventStoreResubscriptionRetryDelay} seconds", resubscriptionAttemptNumber, subscriptionId, resubscriptionAttemptNumber);

                await Task.Delay(TimeSpan.FromSeconds(resubscriptionAttemptNumber)).ConfigureAwait(false);
            }
        }
    }
}
