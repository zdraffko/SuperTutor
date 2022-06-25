using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;

public class LessonEndedIntegrationEvent : IntegrationEvent
{
    public LessonEndedIntegrationEvent(Guid lessonId) => LessonId = lessonId;

    public Guid LessonId { get; }
}
