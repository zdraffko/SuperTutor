using SuperTutor.Contexts.Payments.Domain.Charges.Models.Enumerations;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Charges.Events;

public class ChargeCreatedDomainEvent : DomainEvent
{
    public ChargeCreatedDomainEvent(
        ChargeId chargeId,
        LessonId lessonId,
        StudentId studentId,
        TutorId tutorId,
        decimal amount,
        string currency,
        ExternalPayment externalPayment,
        ChargeStatus status)
    {
        ChargeId = chargeId;
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
        Amount = amount;
        Currency = currency;
        ExternalPayment = externalPayment;
        Status = status;
    }

    public ChargeId ChargeId { get; }
    public LessonId LessonId { get; }

    public StudentId StudentId { get; }

    public TutorId TutorId { get; }

    public decimal Amount { get; }

    public string Currency { get; }

    public ExternalPayment ExternalPayment { get; }

    public ChargeStatus Status { get; }
}
