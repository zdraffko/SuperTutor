using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.IntegrationEvents.Users;

public class TutorRegisteredIntegrationEvent : IntegrationEvent
{
    public TutorRegisteredIntegrationEvent(Guid userId, string userEmail, string firstName, string lastName)
    {
        UserId = userId;
        UserEmail = userEmail;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid UserId { get; }

    public string UserEmail { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
