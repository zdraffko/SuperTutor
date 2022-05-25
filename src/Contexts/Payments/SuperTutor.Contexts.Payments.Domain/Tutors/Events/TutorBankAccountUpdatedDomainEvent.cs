using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorBankAccountUpdatedDomainEvent : DomainEvent
{
    public TutorBankAccountUpdatedDomainEvent(TutorId tutorId, BankAccount bankAccount, bool isBankAccountSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        BankAccount = bankAccount;
        IsBankAccountSyncedWithExternalPaymentAccount = isBankAccountSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public BankAccount BankAccount { get; }

    public bool IsBankAccountSyncedWithExternalPaymentAccount { get; }
}
