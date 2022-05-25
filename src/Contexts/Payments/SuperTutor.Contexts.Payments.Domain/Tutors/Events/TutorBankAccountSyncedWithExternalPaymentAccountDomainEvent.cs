using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent(bool isBankAccountSyncedWithExternalPaymentAccount) => IsBankAccountSyncedWithExternalPaymentAccount = isBankAccountSyncedWithExternalPaymentAccount;

    public bool IsBankAccountSyncedWithExternalPaymentAccount { get; }
}
