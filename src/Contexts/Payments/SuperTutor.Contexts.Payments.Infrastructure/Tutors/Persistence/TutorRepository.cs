using EventStore.Client;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence;

public class TutorRepository : AggregateRootEventsRepository<Tutor, TutorId, Guid>
{
    public TutorRepository(EventStoreClient eventStoreClient, IDomainEventSerializer domainEventSerializer)
        : base(eventStoreClient, domainEventSerializer)
    {
    }
}
