using EventStore.Client;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Repositories;

namespace SuperTutor.Contexts.Schedule.Persistence.Lessons;

public class LessonRepository : AggregateRootEventsRepository<Lesson, LessonId, Guid>
{
    public LessonRepository(EventStoreClient eventStoreClient) : base(eventStoreClient)
    {
    }
}
