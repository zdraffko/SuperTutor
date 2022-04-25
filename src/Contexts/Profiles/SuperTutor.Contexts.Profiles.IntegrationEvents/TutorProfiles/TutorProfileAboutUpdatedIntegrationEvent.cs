using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileAboutUpdatedIntegrationEvent : IntegrationEvent
{
    public TutorProfileAboutUpdatedIntegrationEvent(Guid tutorProfileId, string newAbout)
    {
        TutorProfileId = tutorProfileId;
        NewAbout = newAbout;
    }

    public Guid TutorProfileId { get; }

    public string NewAbout { get; }
}
