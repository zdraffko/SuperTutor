using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent(bool areVerificationDocumentsSyncedWithExternalPaymentAccount) => AreVerificationDocumentsSyncedWithExternalPaymentAccount = areVerificationDocumentsSyncedWithExternalPaymentAccount;

    public bool AreVerificationDocumentsSyncedWithExternalPaymentAccount { get; }
}
