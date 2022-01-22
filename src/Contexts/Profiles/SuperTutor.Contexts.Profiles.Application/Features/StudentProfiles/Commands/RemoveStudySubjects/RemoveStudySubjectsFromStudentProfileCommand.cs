using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.RemoveStudySubject;

public class RemoveStudySubjectsFromStudentProfileCommand : Command
{
    public RemoveStudySubjectsFromStudentProfileCommand(Guid studentProfileId, IEnumerable<int> studySubjectsForRemoval)
    {
        StudentProfileId = studentProfileId;
        StudySubjectsForRemoval = studySubjectsForRemoval;
    }

    public Guid StudentProfileId { get; }

    public IEnumerable<int> StudySubjectsForRemoval { get; }
}
