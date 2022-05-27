using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorCreatedDomainEvent : DomainEvent
{
    public TutorCreatedDomainEvent(TutorId tutorId, string email, string firstName, string lastName)
    {
        TutorId = tutorId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public TutorId TutorId { get; }

    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
