using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors;

public class Tutor : AggregateRoot<TutorId, Guid>
{
    // Required for loading the aggregate root from the event store 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Tutor() : base(new TutorId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Tutor(UserId userId, string email) : base(new TutorId(Guid.NewGuid()))
    {
        UserId = userId;
        Email = email;
    }

    public UserId UserId { get; private set; }

    public string Email { get; private set; }

    public string? ExternalPaymentAccountId { get; private set; }

    public string? ExternalPaymentAccountPersonId { get; }

    public PersonalInformation? PersonalInformation { get; }

    public Address? Address { get; }

    public BankAccount? BankAccount { get; }

    public Document? IdentityFrontVerificationDocument { get; }

    public Document? IdentityBackVerificationDocument { get; }

    public Document? AddressVerificationDocument { get; }

    public TermsOfService? TermsOfService { get; }

    public static Tutor Create(UserId userId, string email)
    {
        var tutor = new Tutor(userId, email);

        tutor.RaiseDomainEvent(new TutorCreatedDomainEvent(tutor.Id, userId, email));

        return tutor;
    }

    public void SetExternalPaymentAccountId(string externalPaymentAccountId)
    {
        ExternalPaymentAccountId = externalPaymentAccountId;

        RaiseDomainEvent(new TutorExternalPaymentAccountCreatedDomainEvent(Id, ExternalPaymentAccountId));
    }

    #region Apply Domain Events

    public override void ApplyDomainEvent(DomainEvent domainEvent) => Apply((dynamic) domainEvent);

    private void Apply(TutorCreatedDomainEvent domainEvent)
    {
        Id = domainEvent.TutorId;
        UserId = domainEvent.UserId;
        Email = domainEvent.Email;
    }

    private void Apply(TutorExternalPaymentAccountCreatedDomainEvent domainEvent) => ExternalPaymentAccountId = domainEvent.ExternalPaymentAccountId;

    #endregion Apply Domain Events
}
