using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileTutoringGradesAddedIntegrationEvent : IntegrationEvent
{
    public TutorProfileTutoringGradesAddedIntegrationEvent(Guid tutorProfileId, IEnumerable<Grade> finalTutoringGradesFortutorProfile)
    {
        TutorProfileId = tutorProfileId;
        FinalTutoringGradesFortutorProfile = finalTutoringGradesFortutorProfile;
    }

    public Guid TutorProfileId { get; }

    public IEnumerable<Grade> FinalTutoringGradesFortutorProfile { get; }

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
