using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.FavoriteFilters.Commands.Add;

public class AddFavoriteFilterCommand : Command
{
    public AddFavoriteFilterCommand(StudentId studentId, string filter)
    {
        StudentId = studentId;
        Filter = filter;
    }

    public StudentId StudentId { get; }

    public string Filter { get; }
}
