using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;

public class LessonStartedIntegrationEvent : IntegrationEvent
{
    public LessonStartedIntegrationEvent(Guid lessonId, Guid tutorId, Guid studentId)
    {
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
    }

    public Guid LessonId { get; }

    public Guid TutorId { get; }

    public Guid StudentId { get; }
}
