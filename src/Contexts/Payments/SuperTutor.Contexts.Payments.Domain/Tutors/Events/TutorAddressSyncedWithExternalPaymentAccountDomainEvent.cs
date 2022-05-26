using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorAddressSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorAddressSyncedWithExternalPaymentAccountDomainEvent(TutorId tutorId, bool isAddressSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        IsAddressSyncedWithExternalPaymentAccount = isAddressSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public bool IsAddressSyncedWithExternalPaymentAccount { get; }
}
