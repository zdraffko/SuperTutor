using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.AddTutoringGrades;

public class AddTutoringGradesToProfileCommand : Command
{
    public AddTutoringGradesToProfileCommand(Guid profileId, IEnumerable<int> newTutoringGrades)
    {
        ProfileId = profileId;
        NewTutoringGrades = newTutoringGrades;
    }

    public Guid ProfileId { get; }

    public IEnumerable<int> NewTutoringGrades { get; }
}
