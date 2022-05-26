using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorTermsOfServiceAcceptedDomainEvent : DomainEvent
{
    public TutorTermsOfServiceAcceptedDomainEvent(TutorId tutorId, TermsOfService termsOfService, bool areTermsOfServiceSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        TermsOfService = termsOfService;
        AreTermsOfServiceSyncedWithExternalPaymentAccount = areTermsOfServiceSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public TermsOfService TermsOfService { get; }

    public bool AreTermsOfServiceSyncedWithExternalPaymentAccount { get; }
}
