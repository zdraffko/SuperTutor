using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RemoveTutoringGrades;

public class RemoveTutoringGradesFromProfileCommand : Command
{
    public RemoveTutoringGradesFromProfileCommand(int profileId, IEnumerable<int> tutoringGradesForRemoval)
    {
        ProfileId = profileId;
        TutoringGradesForRemoval = tutoringGradesForRemoval;
    }

    public int ProfileId { get; }

    public IEnumerable<int> TutoringGradesForRemoval { get; }
}
