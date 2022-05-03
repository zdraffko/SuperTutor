using EventStore.Client;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Persistence.Lessons;

public class LessonRepository : AggregateRootEventsRepository<Lesson, LessonId, Guid>
{
    public LessonRepository(EventStoreClient eventStoreClient, IDomainEventSerializer domainEventSerializer)
        : base(eventStoreClient, domainEventSerializer)
    {
    }
}
