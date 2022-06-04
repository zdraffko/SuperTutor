using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Events;

public class LessonScheduledDomainEvent : DomainEvent
{
    public LessonScheduledDomainEvent(LessonId lessonId, PaymentId paymentId, LessonPaymentStatus paymentStatus, LessonStatus status)
    {
        LessonId = lessonId;
        PaymentId = paymentId;
        PaymentStatus = paymentStatus;
        Status = status;
    }

    public LessonId LessonId { get; }

    public PaymentId PaymentId { get; }

    public LessonPaymentStatus PaymentStatus { get; }

    public LessonStatus Status { get; }
}
