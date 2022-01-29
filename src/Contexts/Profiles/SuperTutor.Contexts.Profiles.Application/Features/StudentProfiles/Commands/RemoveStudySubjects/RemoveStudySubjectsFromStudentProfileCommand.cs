using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.RemoveStudySubject;

public class RemoveStudySubjectsFromStudentProfileCommand : Command
{
    public RemoveStudySubjectsFromStudentProfileCommand(StudentProfileId studentProfileId, IEnumerable<int> studySubjectsForRemoval)
    {
        StudentProfileId = studentProfileId;
        StudySubjectsForRemoval = studySubjectsForRemoval;
    }

    public StudentProfileId StudentProfileId { get; }

    public IEnumerable<int> StudySubjectsForRemoval { get; }
}
