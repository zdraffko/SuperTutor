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

    public BankAccount? BankAccount { get; private set; }

    public bool? IsBankAccountSyncedWithExternalPaymentAccount { get; private set; }

    public Document? IdentityVerificationDocumentFront { get; private set; }

    public Document? IdentityVerificationDocumentBack { get; private set; }

    public Document? AddressVerificationDocument { get; private set; }

    public bool? AreVerificationDocumentsSyncedWithExternalPaymentAccount { get; private set; }

    public TermsOfService? TermsOfService { get; private set; }

    public bool? AreTermsOfServiceSyncedWithExternalPaymentAccount { get; private set; }

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

        RaiseDomainEvent(new TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent(Id, IsPersonalInformationSyncedWithExternalPaymentAccount.Value));
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

        RaiseDomainEvent(new TutorAddressSyncedWithExternalPaymentAccountDomainEvent(Id, IsAddressSyncedWithExternalPaymentAccount.Value));
    }

    public void UpdateBankAccount(BankAccount bankAccount)
    {
        BankAccount = bankAccount;
        IsBankAccountSyncedWithExternalPaymentAccount = false;

        RaiseDomainEvent(new TutorBankAccountUpdatedDomainEvent(Id, BankAccount, IsBankAccountSyncedWithExternalPaymentAccount.Value));
    }

    public void MarkBankAccountAsSyncedWithExternalPaymentAccount()
    {
        IsBankAccountSyncedWithExternalPaymentAccount = true;

        RaiseDomainEvent(new TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent(Id, IsBankAccountSyncedWithExternalPaymentAccount.Value));
    }

    public void UploadVerificationDocuments(Document identityVerificationDocumentFront, Document identityVerificationDocumentBack, Document addressVerificationDocument)
    {
        IdentityVerificationDocumentFront = identityVerificationDocumentFront;
        IdentityVerificationDocumentBack = identityVerificationDocumentBack;
        AddressVerificationDocument = addressVerificationDocument;
        AreVerificationDocumentsSyncedWithExternalPaymentAccount = false;

        RaiseDomainEvent(new TutorVerificationDocumentsUploadedDomainEvent(Id, IdentityVerificationDocumentFront, IdentityVerificationDocumentBack, AddressVerificationDocument, AreVerificationDocumentsSyncedWithExternalPaymentAccount.Value));
    }

    public void MarkVerificationDocumentsAsSyncedWithExternalPaymentAccount()
    {
        AreVerificationDocumentsSyncedWithExternalPaymentAccount = true;

        RaiseDomainEvent(new TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent(Id, AreVerificationDocumentsSyncedWithExternalPaymentAccount.Value));
    }

    public void AcceptTermsOfService(string ipOfAcceptance)
    {
        TermsOfService = new TermsOfService("full", ipOfAcceptance);
        AreTermsOfServiceSyncedWithExternalPaymentAccount = false;

        RaiseDomainEvent(new TutorTermsOfServiceAcceptedDomainEvent(Id, TermsOfService, AreTermsOfServiceSyncedWithExternalPaymentAccount.Value));
    }

    public void MarkTermsOfServiceAsSyncedWithExternalPaymentAccount()
    {
        AreTermsOfServiceSyncedWithExternalPaymentAccount = true;

        RaiseDomainEvent(new TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent(Id, AreTermsOfServiceSyncedWithExternalPaymentAccount.Value));
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

    private void Apply(TutorBankAccountUpdatedDomainEvent domainEvent)
    {
        BankAccount = domainEvent.BankAccount;
        IsBankAccountSyncedWithExternalPaymentAccount = domainEvent.IsBankAccountSyncedWithExternalPaymentAccount;
    }

    private void Apply(TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent domainEvent) => IsBankAccountSyncedWithExternalPaymentAccount = domainEvent.IsBankAccountSyncedWithExternalPaymentAccount;

    private void Apply(TutorVerificationDocumentsUploadedDomainEvent domainEvent)
    {
        IdentityVerificationDocumentFront = domainEvent.IdentityVerificationDocumentFront;
        IdentityVerificationDocumentBack = domainEvent.IdentityVerificationDocumentBack;
        AddressVerificationDocument = domainEvent.AddressVerificationDocument;
    }

    private void Apply(TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent domainEvent) => AreVerificationDocumentsSyncedWithExternalPaymentAccount = domainEvent.AreVerificationDocumentsSyncedWithExternalPaymentAccount;

    private void Apply(TutorTermsOfServiceAcceptedDomainEvent domainEvent)
    {
        TermsOfService = domainEvent.TermsOfService;
        AreTermsOfServiceSyncedWithExternalPaymentAccount = domainEvent.AreTermsOfServiceSyncedWithExternalPaymentAccount;
    }

    private void Apply(TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent domainEvent) => AreTermsOfServiceSyncedWithExternalPaymentAccount = domainEvent.AreTermsOfServiceSyncedWithExternalPaymentAccount;

    #endregion Apply Domain Events
}
