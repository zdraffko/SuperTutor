using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileCreatedIntegrationEvent : IntegrationEvent
{
    public TutorProfileCreatedIntegrationEvent(Guid tutorProfileId, string about, int tutoringSubject, IEnumerable<int> tutoringGrades, decimal rateForOneHour, bool isActive)
    {
        TutorProfileId = tutorProfileId;
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
        IsActive = isActive;
    }

    public Guid TutorProfileId { get; }

    public string About { get; }

    public int TutoringSubject { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal RateForOneHour { get; }

    public bool IsActive { get; }
}
