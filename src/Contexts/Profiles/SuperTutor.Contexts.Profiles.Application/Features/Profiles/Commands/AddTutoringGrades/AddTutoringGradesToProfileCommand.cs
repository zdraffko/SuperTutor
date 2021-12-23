using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.AddTutoringGrades;

public class AddTutoringGradesToProfileCommand : Command
{
    public AddTutoringGradesToProfileCommand(int profileId, IEnumerable<int> newTutoringGrades)
    {
        ProfileId = profileId;
        NewTutoringGrades = newTutoringGrades;
    }

    public int ProfileId { get; }

    public IEnumerable<int> NewTutoringGrades { get; }
}
