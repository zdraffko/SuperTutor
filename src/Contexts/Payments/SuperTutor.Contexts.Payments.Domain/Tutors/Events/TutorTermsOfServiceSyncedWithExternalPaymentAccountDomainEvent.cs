using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent(bool areTermsOfServiceSyncedWithExternalPaymentAccount) => AreTermsOfServiceSyncedWithExternalPaymentAccount = areTermsOfServiceSyncedWithExternalPaymentAccount;

    public bool AreTermsOfServiceSyncedWithExternalPaymentAccount { get; }
}
