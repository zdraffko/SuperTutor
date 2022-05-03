using FluentResults;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Subscriptions;

public interface IEventStoreSubscriber
{
    Task<Result> SubscribeToAll(string subscriptionId, CancellationToken cancellationToken);
}
