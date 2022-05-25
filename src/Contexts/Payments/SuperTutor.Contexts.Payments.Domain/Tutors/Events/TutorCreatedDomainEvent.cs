using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorCreatedDomainEvent : DomainEvent
{
    public TutorCreatedDomainEvent(TutorId tutorId, UserId userId, string email)
    {
        TutorId = tutorId;
        UserId = userId;
        Email = email;
    }

    public TutorId TutorId { get; }

    public UserId UserId { get; }

    public string Email { get; }
}
