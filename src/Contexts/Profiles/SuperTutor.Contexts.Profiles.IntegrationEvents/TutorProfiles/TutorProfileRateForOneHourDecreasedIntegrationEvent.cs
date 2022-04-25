using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileRateForOneHourDecreasedIntegrationEvent : IntegrationEvent
{
    public TutorProfileRateForOneHourDecreasedIntegrationEvent(Guid tutorProfileId, decimal newRateForOneHour)
    {
        TutorProfileId = tutorProfileId;
        NewRateForOneHour = newRateForOneHour;
    }

    public Guid TutorProfileId { get; }

    public decimal NewRateForOneHour { get; }
}
