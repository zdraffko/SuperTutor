using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Events;

public class LessonAbandonedDomainEvent : DomainEvent
{
    public LessonAbandonedDomainEvent(LessonId lessonId, LessonStatus status, LessonPaymentStatus paymentStatus)
    {
        LessonId = lessonId;
        Status = status;
        PaymentStatus = paymentStatus;
    }

    public LessonId LessonId { get; }

    public LessonStatus Status { get; }

    public LessonPaymentStatus PaymentStatus { get; }
}
