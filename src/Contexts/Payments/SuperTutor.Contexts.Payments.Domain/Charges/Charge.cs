using SuperTutor.Contexts.Payments.Domain.Charges.Events;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.Enumerations;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Charges;

public class Charge : AggregateRoot<ChargeId, Guid>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Charge() : base(new ChargeId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Charge(
        ChargeId id,
        LessonId lessonId,
        StudentId studentId,
        TutorId tutorId,
        decimal amount,
        string currency,
        ExternalPayment externalPayment) : base(id)
    {
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
        Amount = amount;
        Currency = currency;
        ExternalPayment = externalPayment;
        Status = ChargeStatus.Pending;

        RaiseDomainEvent(new ChargeCreatedDomainEvent(
            Id,
            LessonId,
            StudentId,
            TutorId,
            Amount,
            Currency,
            ExternalPayment,
            Status
        ));
    }

    public LessonId LessonId { get; private set; }

    public StudentId StudentId { get; private set; }

    public TutorId TutorId { get; private set; }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; }

    public ExternalPayment ExternalPayment { get; private set; }

    public ChargeStatus Status { get; private set; }

    #region Apply Domain Events

    public override void ApplyDomainEvent(DomainEvent domainEvent) => Apply((dynamic) domainEvent);

    private void Apply(ChargeCreatedDomainEvent domainEvent)
    {
        Id = domainEvent.ChargeId;
        LessonId = domainEvent.LessonId;
        StudentId = domainEvent.StudentId;
        TutorId = domainEvent.TutorId;
        Amount = domainEvent.Amount;
        Currency = domainEvent.Currency;
        ExternalPayment = domainEvent.ExternalPayment;
        Status = domainEvent.Status;
    }

    #endregion Apply Domain Events
}
