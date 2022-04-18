using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Students.Commands.AddFavoriteFilter;

public class AddFavoriteFilterForStudentCommand : Command
{
    public AddFavoriteFilterForStudentCommand(StudentId studentId, string filter)
    {
        StudentId = studentId;
        Filter = filter;
    }

    public StudentId StudentId { get; }

    public string Filter { get; }
}
