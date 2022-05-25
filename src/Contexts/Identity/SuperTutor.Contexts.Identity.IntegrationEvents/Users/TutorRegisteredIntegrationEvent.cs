using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.IntegrationEvents.Users;

public class TutorRegisteredIntegrationEvent : IntegrationEvent
{
    public TutorRegisteredIntegrationEvent(Guid userId, string userEmail)
    {
        UserId = userId;
        UserEmail = userEmail;
    }

    public Guid UserId { get; }

    public string UserEmail { get; }
}
