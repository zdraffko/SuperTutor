using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.IntegrationEvents.Users;

public class UserDeletedIntegrationEvent : IntegrationEvent
{
    public UserDeletedIntegrationEvent(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
