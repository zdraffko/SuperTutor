using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent(TutorId tutorId, bool isBankAccountSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        IsBankAccountSyncedWithExternalPaymentAccount = isBankAccountSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public bool IsBankAccountSyncedWithExternalPaymentAccount { get; }
}
