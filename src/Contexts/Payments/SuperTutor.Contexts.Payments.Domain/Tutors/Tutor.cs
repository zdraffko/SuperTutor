using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.Contexts.Payments.Domain.Tutors.Invariants;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors;

public class Tutor : AggregateRoot<TutorId, Guid>
{
    // Required for loading the aggregate root from the event store 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Tutor() : base(new TutorId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Tutor(TutorId tutorId, string email) : base(tutorId) => Email = email;

    public string Email { get; private set; }

    public ExternalPaymentAccount? ExternalPaymentAccount { get; private set; }

    public PersonalInformation? PersonalInformation { get; private set; }

    public bool? IsPersonalInformationSyncedWithExternalPaymentAccount { get; private set; }

    public Address? Address { get; private set; }

    public bool? IsAddressSyncedWithExternalPaymentAccount { get; private set; }

    public BankAccount? BankAccount { get; }

    public Document? IdentityFrontVerificationDocument { get; }

    public Document? IdentityBackVerificationDocument { get; }

    public Document? AddressVerificationDocument { get; }

    public TermsOfService? TermsOfService { get; }

    public static Tutor Create(TutorId tutorId, string email)
    {
        var tutor = new Tutor(tutorId, email);

        tutor.RaiseDomainEvent(new TutorCreatedDomainEvent(tutor.Id, email));

        return tutor;
    }

    public void CreateExternalPaymentAccount(string externalPaymentAccountId, string externalPaymentAccountPersonId)
    {
        CheckInvariant(new TutorExternalPaymentAccountCanOnlyBeCreatedOnceInvariant(ExternalPaymentAccount));

        ExternalPaymentAccount = new ExternalPaymentAccount(externalPaymentAccountId, externalPaymentAccountPersonId);

        RaiseDomainEvent(new TutorExternalPaymentAccountCreatedDomainEvent(Id, ExternalPaymentAccount));
    }

    public void UpdatePersonalInformation(PersonalInformation personalInformation)
    {
        PersonalInformation = personalInformation;
        IsPersonalInformationSyncedWithExternalPaymentAccount = false;

        RaiseDomainEvent(new TutorPersonalInformationUpdatedDomainEvent(Id, PersonalInformation, IsPersonalInformationSyncedWithExternalPaymentAccount.Value));
    }

    public void MarkPersonalInformationAsSyncedWithExternalPaymentAccount()
    {
        IsPersonalInformationSyncedWithExternalPaymentAccount = true;

        RaiseDomainEvent(new TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent(IsPersonalInformationSyncedWithExternalPaymentAccount.Value));
    }

    public void UpdateAddress(Address address)
    {
        Address = address;
        IsAddressSyncedWithExternalPaymentAccount = false;

        RaiseDomainEvent(new TutorAddressUpdatedDomainEvent(Id, Address, IsAddressSyncedWithExternalPaymentAccount.Value));
    }

    public void MarkAddressAsSyncedWithExternalPaymentAccount()
    {
        IsAddressSyncedWithExternalPaymentAccount = true;

        RaiseDomainEvent(new TutorAddressSyncedWithExternalPaymentAccountDomainEvent(IsAddressSyncedWithExternalPaymentAccount.Value));
    }

    #region Apply Domain Events

    public override void ApplyDomainEvent(DomainEvent domainEvent) => Apply((dynamic) domainEvent);

    private void Apply(TutorCreatedDomainEvent domainEvent)
    {
        Id = domainEvent.TutorId;
        Email = domainEvent.Email;
    }

    private void Apply(TutorExternalPaymentAccountCreatedDomainEvent domainEvent) => ExternalPaymentAccount = domainEvent.ExternalPaymentAccount;

    private void Apply(TutorPersonalInformationUpdatedDomainEvent domainEvent)
    {
        PersonalInformation = domainEvent.PersonalInformation;
        IsPersonalInformationSyncedWithExternalPaymentAccount = domainEvent.IsPersonalInformationSyncedWithExternalPaymentAccount;
    }

    private void Apply(TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent domainEvent) => IsPersonalInformationSyncedWithExternalPaymentAccount = domainEvent.IsPersonalInformationSyncedWithExternalPaymentAccount;

    private void Apply(TutorAddressUpdatedDomainEvent domainEvent)
    {
        Address = domainEvent.Address;
        IsAddressSyncedWithExternalPaymentAccount = domainEvent.IsAddressSyncedWithExternalPaymentAccount;
    }

    private void Apply(TutorAddressSyncedWithExternalPaymentAccountDomainEvent domainEvent) => IsAddressSyncedWithExternalPaymentAccount = domainEvent.IsAddressSyncedWithExternalPaymentAccount;

    #endregion Apply Domain Events
}
