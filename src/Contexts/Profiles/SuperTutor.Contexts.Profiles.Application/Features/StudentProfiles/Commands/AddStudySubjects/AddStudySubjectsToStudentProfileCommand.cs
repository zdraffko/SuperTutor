using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.AddStudySubjects;

public class AddStudySubjectsToStudentProfileCommand : Command
{
    public AddStudySubjectsToStudentProfileCommand(StudentProfileId studentProfileId, IEnumerable<int> newStudySubjects)
    {
        StudentProfileId = studentProfileId;
        NewStudySubjects = newStudySubjects;
    }

    public StudentProfileId StudentProfileId { get; }

    public IEnumerable<int> NewStudySubjects { get; }
}
