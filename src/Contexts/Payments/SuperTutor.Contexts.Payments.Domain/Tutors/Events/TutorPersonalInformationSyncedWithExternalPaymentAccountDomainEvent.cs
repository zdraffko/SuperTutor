using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent : DomainEvent
{
    public TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent(bool isPersonalInformationSyncedWithExternalPaymentAccount) => IsPersonalInformationSyncedWithExternalPaymentAccount = isPersonalInformationSyncedWithExternalPaymentAccount;

    public bool IsPersonalInformationSyncedWithExternalPaymentAccount { get; }
}
