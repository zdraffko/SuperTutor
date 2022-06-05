using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Transfers.Models.ValueObjects;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Transfers.Events;

public class TransferCreatedDomainEvent : DomainEvent
{
    public TransferCreatedDomainEvent(
        TransferId transferId,
        ChargeId chargeId,
        LessonId lessonId,
        StudentId studentId,
        TutorId tutorId,
        decimal amount,
        string currency,
        ExternalPayment externalPayment)
    {
        TransferId = transferId;
        ChargeId = chargeId;
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
        Amount = amount;
        Currency = currency;
        ExternalPayment = externalPayment;
    }

    public TransferId TransferId { get; }

    public ChargeId ChargeId { get; }

    public LessonId LessonId { get; }

    public StudentId StudentId { get; }

    public TutorId TutorId { get; }

    public decimal Amount { get; }

    public string Currency { get; }

    public ExternalPayment ExternalPayment { get; }
}
