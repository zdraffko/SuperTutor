using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorExternalPaymentAccountCreatedDomainEvent : DomainEvent
{
    public TutorExternalPaymentAccountCreatedDomainEvent(TutorId tutorId, ExternalPaymentAccount externalPaymentAccount)
    {
        TutorId = tutorId;
        ExternalPaymentAccount = externalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public ExternalPaymentAccount ExternalPaymentAccount { get; }
}
