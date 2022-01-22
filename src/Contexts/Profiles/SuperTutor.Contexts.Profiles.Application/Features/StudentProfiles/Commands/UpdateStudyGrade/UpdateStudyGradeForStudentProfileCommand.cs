using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.UpdateStudyGrade;

public class UpdateStudyGradeForStudentProfileCommand : Command
{
    public UpdateStudyGradeForStudentProfileCommand(Guid studentProfileId, int newStudyGrade)
    {
        StudentProfileId = studentProfileId;
        NewStudyGrade = newStudyGrade;
    }

    public Guid StudentProfileId { get; }

    public int NewStudyGrade { get; }
}
