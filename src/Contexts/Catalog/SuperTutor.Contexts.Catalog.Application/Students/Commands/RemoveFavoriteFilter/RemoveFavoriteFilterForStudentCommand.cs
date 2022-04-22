using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Students.Commands.RemoveFavoriteFilter;

public class RemoveFavoriteFilterForStudentCommand : Command
{
    public RemoveFavoriteFilterForStudentCommand(StudentId studentId, string filter)
    {
        StudentId = studentId;
        Filter = filter;
    }

    public StudentId StudentId { get; }

    public string Filter { get; }
}
