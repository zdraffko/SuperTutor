using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;

public class FavoriteFilter : ValueObject
{
    public FavoriteFilter(StudentId studentId, string filter)
    {
        StudentId = studentId;
        Filter = filter;
    }

    public StudentId StudentId { get; }

    public string Filter { get; }
}
