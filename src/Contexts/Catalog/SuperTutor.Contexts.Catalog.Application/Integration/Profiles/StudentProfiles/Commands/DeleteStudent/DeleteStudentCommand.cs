using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.StudentProfiles.Commands.DeleteStudent;

public class DeleteStudentCommand : Command
{
    public DeleteStudentCommand(StudentId studentId) => StudentId = studentId;

    public StudentId StudentId { get; }
}
