using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;

public class CreateProfileCommand : Command
{
    public CreateProfileCommand(int userId, string about, int tutoringSubject, IEnumerable<int> tutoringGrades, decimal rateForOneHour)
    {
        UserId = userId;
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
    }

    public int UserId { get; }

    public string About { get; }

    public int TutoringSubject { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal RateForOneHour { get; }
}
