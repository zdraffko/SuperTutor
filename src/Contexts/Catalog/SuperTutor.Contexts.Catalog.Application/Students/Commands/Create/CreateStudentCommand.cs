using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Students.Commands.Create;

public class CreateStudentCommand : Command
{
    public CreateStudentCommand(StudentId studentId) => StudentId = studentId;

    public StudentId StudentId { get; }
}
