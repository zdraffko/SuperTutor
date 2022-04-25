using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileActivatedIntegrationEvent : IntegrationEvent
{
    public TutorProfileActivatedIntegrationEvent(Guid tutorProfileId) => TutorProfileId = tutorProfileId;

    public Guid TutorProfileId { get; }
}
