using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorExternalPaymentAccountCreatedDomainEvent : DomainEvent
{
    public TutorExternalPaymentAccountCreatedDomainEvent(TutorId tutorId, string externalPaymentAccountId)
    {
        TutorId = tutorId;
        ExternalPaymentAccountId = externalPaymentAccountId;
    }

    public TutorId TutorId { get; }

    public string ExternalPaymentAccountId { get; }
}
