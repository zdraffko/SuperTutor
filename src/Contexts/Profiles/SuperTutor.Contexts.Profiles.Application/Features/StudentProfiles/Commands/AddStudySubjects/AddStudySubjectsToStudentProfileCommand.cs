using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.AddStudySubjects;

public class AddStudySubjectsToStudentProfileCommand : Command
{
    public AddStudySubjectsToStudentProfileCommand(Guid studentProfileId, IEnumerable<int> newStudySubjects)
    {
        StudentProfileId = studentProfileId;
        NewStudySubjects = newStudySubjects;
    }

    public Guid StudentProfileId { get; }

    public IEnumerable<int> NewStudySubjects { get; }
}
