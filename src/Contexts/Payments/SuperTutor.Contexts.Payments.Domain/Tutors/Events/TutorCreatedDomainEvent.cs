using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorCreatedDomainEvent : DomainEvent
{
    public TutorCreatedDomainEvent(TutorId tutorId, string email)
    {
        TutorId = tutorId;
        Email = email;
    }

    public TutorId TutorId { get; }

    public string Email { get; }
}
