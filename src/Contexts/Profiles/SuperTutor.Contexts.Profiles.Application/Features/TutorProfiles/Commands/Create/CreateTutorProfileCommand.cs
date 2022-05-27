using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create;

public class CreateTutorProfileCommand : Command<CreateTutorProfileCommandPayload>
{
    public CreateTutorProfileCommand(TutorId tutorId, string about, int tutoringSubject, IEnumerable<int> tutoringGrades, decimal rateForOneHour)
    {
        TutorId = tutorId;
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
    }

    public TutorId TutorId { get; }

    public string About { get; }

    public int TutoringSubject { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal RateForOneHour { get; }
}
