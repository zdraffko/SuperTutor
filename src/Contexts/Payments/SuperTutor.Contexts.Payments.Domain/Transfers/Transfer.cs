using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Transfers.Events;
using SuperTutor.Contexts.Payments.Domain.Transfers.Models.ValueObjects;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Transfers;

public class Transfer : AggregateRoot<TransferId, Guid>
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Transfer() : base(new TransferId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Transfer(
        ChargeId chargeId,
        LessonId lessonId,
        StudentId studentId,
        TutorId tutorId,
        decimal amount,
        string currency,
        ExternalPayment externalPayment) : base(new TransferId(Guid.NewGuid()))
    {
        ChargeId = chargeId;
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
        Amount = amount;
        Currency = currency;
        ExternalPayment = externalPayment;

        RaiseDomainEvent(new TransferCreatedDomainEvent(
            Id,
            ChargeId,
            LessonId,
            StudentId,
            TutorId,
            Amount,
            Currency,
            ExternalPayment
        ));
    }

    public ChargeId ChargeId { get; private set; }

    public LessonId LessonId { get; private set; }

    public StudentId StudentId { get; private set; }

    public TutorId TutorId { get; private set; }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; }

    public ExternalPayment ExternalPayment { get; private set; }

    #region Apply Domain Events

    public override void ApplyDomainEvent(DomainEvent domainEvent) => throw new NotImplementedException();

    #endregion Apply Domain Events
}
