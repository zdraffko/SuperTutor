using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Events;

public class LessonStartedDomainEvent : DomainEvent
{
    public LessonStartedDomainEvent(LessonId lessonId, LessonStatus status)
    {
        LessonId = lessonId;
        Status = status;
    }

    public LessonId LessonId { get; }

    public LessonStatus Status { get; }
}
