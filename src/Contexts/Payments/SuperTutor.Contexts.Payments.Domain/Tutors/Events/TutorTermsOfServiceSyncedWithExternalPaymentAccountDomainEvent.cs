using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent(TutorId tutorId, bool areTermsOfServiceSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        AreTermsOfServiceSyncedWithExternalPaymentAccount = areTermsOfServiceSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public bool AreTermsOfServiceSyncedWithExternalPaymentAccount { get; }
}
