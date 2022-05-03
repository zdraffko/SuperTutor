namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;

public interface IEventStoreSubscriptionCheckpointRepository
{
    Task<ulong?> Load(string subscriptionId, CancellationToken cancellationToken);

    Task Store(string subscriptionId, ulong position, CancellationToken cancellationToken);
}
