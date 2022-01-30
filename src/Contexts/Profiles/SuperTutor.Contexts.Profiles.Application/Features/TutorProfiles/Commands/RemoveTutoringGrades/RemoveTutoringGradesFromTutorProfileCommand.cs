using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RemoveTutoringGrades;

public class RemoveTutoringGradesFromTutorProfileCommand : Command
{
    public RemoveTutoringGradesFromTutorProfileCommand(TutorProfileId tutorProfileId, IEnumerable<int> tutoringGradesForRemoval)
    {
        TutorProfileId = tutorProfileId;
        TutoringGradesForRemoval = tutoringGradesForRemoval;
    }

    public TutorProfileId TutorProfileId { get; }

    public IEnumerable<int> TutoringGradesForRemoval { get; }
}
