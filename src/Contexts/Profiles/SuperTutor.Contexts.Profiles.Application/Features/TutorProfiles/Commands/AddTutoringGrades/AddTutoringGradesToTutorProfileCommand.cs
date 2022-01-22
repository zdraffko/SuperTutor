using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.AddTutoringGrades;

public class AddTutoringGradesToTutorProfileCommand : Command
{
    public AddTutoringGradesToTutorProfileCommand(Guid tutorProfileId, IEnumerable<int> newTutoringGrades)
    {
        TutorProfileId = tutorProfileId;
        NewTutoringGrades = newTutoringGrades;
    }

    public Guid TutorProfileId { get; }

    public IEnumerable<int> NewTutoringGrades { get; }
}
