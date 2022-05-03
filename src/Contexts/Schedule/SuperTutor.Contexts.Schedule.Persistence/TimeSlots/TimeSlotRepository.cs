using EventStore.Client;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Serializers;

namespace SuperTutor.Contexts.Schedule.Persistence.TimeSlots;

public class TimeSlotRepository : AggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid>
{
    public TimeSlotRepository(EventStoreClient eventStoreClient, IDomainEventSerializer domainEventSerializer)
        : base(eventStoreClient, domainEventSerializer)
    {
    }
}
