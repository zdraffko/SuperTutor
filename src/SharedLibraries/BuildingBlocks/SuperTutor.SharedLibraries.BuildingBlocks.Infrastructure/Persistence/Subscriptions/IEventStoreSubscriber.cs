using FluentResults;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Subscriptions;

public interface IEventStoreSubscriber
{
    Task<Result> SubscribeToAll(string subscriptionId, CancellationToken cancellationToken);
}
