using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorPersonalInformationUpdatedDomainEvent : DomainEvent
{
    public TutorPersonalInformationUpdatedDomainEvent(TutorId tutorId, PersonalInformation personalInformation, bool isPersonalInformationSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        PersonalInformation = personalInformation;
        IsPersonalInformationSyncedWithExternalPaymentAccount = isPersonalInformationSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public PersonalInformation PersonalInformation { get; }

    public bool IsPersonalInformationSyncedWithExternalPaymentAccount { get; }
}
