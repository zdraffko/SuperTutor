using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent(TutorId tutorId, bool areVerificationDocumentsSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        AreVerificationDocumentsSyncedWithExternalPaymentAccount = areVerificationDocumentsSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public bool AreVerificationDocumentsSyncedWithExternalPaymentAccount { get; }
}
