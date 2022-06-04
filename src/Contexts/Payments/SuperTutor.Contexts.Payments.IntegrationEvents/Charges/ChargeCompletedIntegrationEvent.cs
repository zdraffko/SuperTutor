using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Payments.IntegrationEvents.Charges;

public class ChargeCompletedIntegrationEvent : IntegrationEvent
{
    public ChargeCompletedIntegrationEvent(Guid chargeId, Guid lessonId, Guid tutorId, Guid studentId)
    {
        ChargeId = chargeId;
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
    }

    public Guid ChargeId { get; }

    public Guid LessonId { get; }

    public Guid TutorId { get; }

    public Guid StudentId { get; }
}
