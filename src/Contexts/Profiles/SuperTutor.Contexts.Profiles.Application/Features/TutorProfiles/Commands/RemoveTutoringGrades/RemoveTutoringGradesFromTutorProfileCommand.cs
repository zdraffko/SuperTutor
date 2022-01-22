using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RemoveTutoringGrades;

public class RemoveTutoringGradesFromTutorProfileCommand : Command
{
    public RemoveTutoringGradesFromTutorProfileCommand(Guid tutorProfileId, IEnumerable<int> tutoringGradesForRemoval)
    {
        TutorProfileId = tutorProfileId;
        TutoringGradesForRemoval = tutoringGradesForRemoval;
    }

    public Guid TutorProfileId { get; }

    public IEnumerable<int> TutoringGradesForRemoval { get; }
}
