using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorAddressSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorAddressSyncedWithExternalPaymentAccountDomainEvent(bool isAddressSyncedWithExternalPaymentAccount) => IsAddressSyncedWithExternalPaymentAccount = isAddressSyncedWithExternalPaymentAccount;

    public bool IsAddressSyncedWithExternalPaymentAccount { get; }
}
