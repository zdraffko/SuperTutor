using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.UpdateStudyGrade;

public class UpdateStudyGradeForStudentProfileCommand : Command
{
    public UpdateStudyGradeForStudentProfileCommand(StudentProfileId studentProfileId, int newStudyGrade)
    {
        StudentProfileId = studentProfileId;
        NewStudyGrade = newStudyGrade;
    }

    public StudentProfileId StudentProfileId { get; }

    public int NewStudyGrade { get; }
}
