using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create;

public class CreateStudentProfileCommand : Command
{
    public CreateStudentProfileCommand(Guid studentId, IEnumerable<int> studySubjects, int studyGrade)
    {
        StudentId = studentId;
        StudySubjects = studySubjects;
        StudyGrade = studyGrade;
    }

    public Guid StudentId { get; }

    public IEnumerable<int> StudySubjects { get; }

    public int StudyGrade { get; }
}
