using EventStore.Client;
using SuperTutor.Contexts.Payments.Domain.Transfers;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

namespace SuperTutor.Contexts.Payments.Infrastructure.Transfers.Persistence;

public class TransferRepository : AggregateRootEventsRepository<Transfer, TransferId, Guid>
{
    public TransferRepository(EventStoreClient eventStoreClient, IDomainEventSerializer domainEventSerializer)
        : base(eventStoreClient, domainEventSerializer)
    {
    }
}
