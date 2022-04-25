using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileDeletedIntegrationEvent : IntegrationEvent
{
    public TutorProfileDeletedIntegrationEvent(Guid tutorProfileId) => TutorProfileId = tutorProfileId;

    public Guid TutorProfileId { get; }
}
