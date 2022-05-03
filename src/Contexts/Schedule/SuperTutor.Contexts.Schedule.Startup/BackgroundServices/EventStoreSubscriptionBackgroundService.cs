using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Subscriptions;

namespace SuperTutor.Contexts.Schedule.Startup.BackgroundServices;

public class EventStoreSubscriptionBackgroundService : BackgroundService
{
    private const string SubscriptionId = "all_subscription";
    private readonly IEventStoreSubscriber eventStoreDBSubscriptionToAll;

    public EventStoreSubscriptionBackgroundService(IEventStoreSubscriber eventStoreDBSubscriptionToAll) => this.eventStoreDBSubscriptionToAll = eventStoreDBSubscriptionToAll;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            var subscriptionResult = await eventStoreDBSubscriptionToAll.SubscribeToAll(SubscriptionId, cancellationToken);
            if (subscriptionResult.IsSuccess)
            {
                return;
            }
        }
    }
}
