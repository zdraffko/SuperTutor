using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;

public class LessonCompletedIntegrationEvent : IntegrationEvent
{
    public LessonCompletedIntegrationEvent(Guid lessonId, Guid tutorId, Guid studentId, Guid paymentId)
    {
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
        PaymentId = paymentId;
    }

    public Guid LessonId { get; }

    public Guid TutorId { get; }

    public Guid StudentId { get; }

    public Guid PaymentId { get; }
}
