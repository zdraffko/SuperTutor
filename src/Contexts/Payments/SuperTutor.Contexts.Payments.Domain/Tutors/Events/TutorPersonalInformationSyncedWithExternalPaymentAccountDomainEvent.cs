using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent(TutorId tutorId, bool isPersonalInformationSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        IsPersonalInformationSyncedWithExternalPaymentAccount = isPersonalInformationSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public bool IsPersonalInformationSyncedWithExternalPaymentAccount { get; }
}
