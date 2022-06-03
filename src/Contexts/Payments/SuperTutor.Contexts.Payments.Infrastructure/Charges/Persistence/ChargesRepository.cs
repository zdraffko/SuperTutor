using EventStore.Client;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

namespace SuperTutor.Contexts.Payments.Infrastructure.Charges.Persistence;

public class ChargesRepository : AggregateRootEventsRepository<Charge, ChargeId, Guid>
{
    public ChargesRepository(EventStoreClient eventStoreClient, IDomainEventSerializer domainEventSerializer)
        : base(eventStoreClient, domainEventSerializer)
    {
    }
}
