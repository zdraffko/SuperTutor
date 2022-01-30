using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create;

public class CreateStudentProfileCommand : Command
{
    public CreateStudentProfileCommand(StudentId studentId, IEnumerable<int> studySubjects, int studyGrade)
    {
        StudentId = studentId;
        StudySubjects = studySubjects;
        StudyGrade = studyGrade;
    }

    public StudentId StudentId { get; }

    public IEnumerable<int> StudySubjects { get; }

    public int StudyGrade { get; }
}
