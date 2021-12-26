using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RemoveTutoringGrades;

public class RemoveTutoringGradesFromProfileCommand : Command
{
    public RemoveTutoringGradesFromProfileCommand(Guid profileId, IEnumerable<int> tutoringGradesForRemoval)
    {
        ProfileId = profileId;
        TutoringGradesForRemoval = tutoringGradesForRemoval;
    }

    public Guid ProfileId { get; }

    public IEnumerable<int> TutoringGradesForRemoval { get; }
}
