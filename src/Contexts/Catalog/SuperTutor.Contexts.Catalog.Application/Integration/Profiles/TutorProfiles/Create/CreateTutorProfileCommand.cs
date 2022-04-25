using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Create;

public class CreateTutorProfileCommand : Command
{
    public CreateTutorProfileCommand(TutorProfileId tutorProfileId, string about, TutoringSubject tutoringSubject, IEnumerable<TutoringGrade> tutoringGrades, decimal rateForOneHour, bool isActive)
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

    public TutoringSubject TutoringSubject { get; }

    public IEnumerable<TutoringGrade> TutoringGrades { get; }

    public decimal RateForOneHour { get; }

    public bool IsActive { get; }
}
