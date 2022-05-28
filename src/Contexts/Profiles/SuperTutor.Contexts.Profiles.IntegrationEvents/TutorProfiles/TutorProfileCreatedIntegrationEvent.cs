using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;

public class TutorProfileCreatedIntegrationEvent : IntegrationEvent
{
    public TutorProfileCreatedIntegrationEvent(Guid tutorId, Guid tutorProfileId, string about, Subject tutoringSubject, IEnumerable<Grade> tutoringGrades, decimal rateForOneHour, bool isActive)
    {
        TutorId = tutorId;
        TutorProfileId = tutorProfileId;
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
        IsActive = isActive;
    }

    public Guid TutorId { get; }

    public Guid TutorProfileId { get; }

    public string About { get; }

    public Subject TutoringSubject { get; }

    public IEnumerable<Grade> TutoringGrades { get; }

    public decimal RateForOneHour { get; }

    public bool IsActive { get; }

    public class Subject
    {
        public Subject(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Value { get; }

        public string Name { get; }
    }

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
