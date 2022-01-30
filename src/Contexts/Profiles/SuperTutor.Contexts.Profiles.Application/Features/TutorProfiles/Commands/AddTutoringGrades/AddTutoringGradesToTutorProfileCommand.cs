using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.AddTutoringGrades;

public class AddTutoringGradesToTutorProfileCommand : Command
{
    public AddTutoringGradesToTutorProfileCommand(TutorProfileId tutorProfileId, IEnumerable<int> newTutoringGrades)
    {
        TutorProfileId = tutorProfileId;
        NewTutoringGrades = newTutoringGrades;
    }

    public TutorProfileId TutorProfileId { get; }

    public IEnumerable<int> NewTutoringGrades { get; }
}
