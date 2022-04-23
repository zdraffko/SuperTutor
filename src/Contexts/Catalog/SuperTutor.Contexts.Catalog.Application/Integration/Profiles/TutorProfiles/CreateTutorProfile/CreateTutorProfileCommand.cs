using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.CreateTutorProfile;

public class CreateTutorProfileCommand : Command
{
    public CreateTutorProfileCommand(TutorProfileId tutorProfileId, string about, int tutoringSubject, IEnumerable<int> tutoringGrades, decimal rateForOneHour, bool isActive)
    {
        TutorProfileId = tutorProfileId;
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
        IsActive = isActive;
    }

    public TutorProfileId TutorProfileId { get; }

    public string About { get; }

    public int TutoringSubject { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal RateForOneHour { get; }

    public bool IsActive { get; }
}
