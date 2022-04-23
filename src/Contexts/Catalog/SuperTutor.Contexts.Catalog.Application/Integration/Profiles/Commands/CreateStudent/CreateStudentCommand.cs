using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.Commands.CreateStudent;

public class CreateStudentCommand : Command
{
    public CreateStudentCommand(StudentId studentId) => StudentId = studentId;

    public StudentId StudentId { get; }
}
