using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileTutoringGradesRemovedIntegrationEvent : IntegrationEvent
{
    public TutorProfileTutoringGradesRemovedIntegrationEvent(Guid tutorProfileId, IEnumerable<Grade> finalTutoringGradesForTutorProfile)
    {
        TutorProfileId = tutorProfileId;
        FinalTutoringGradesForTutorProfile = finalTutoringGradesForTutorProfile;
    }

    public Guid TutorProfileId { get; }

    public IEnumerable<Grade> FinalTutoringGradesForTutorProfile { get; }

    public class Grade
    {
        public Grade(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Value { get; }

        public string Name { get; }
    }
}
