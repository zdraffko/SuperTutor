using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;

public class FavoriteFilter : Entity<FavoriteFilterId, Guid>, IAggregateRoot
{
    public FavoriteFilter(StudentId studentId, string filter) : base(new FavoriteFilterId(Guid.NewGuid()))
    {
        StudentId = studentId;
        Filter = filter;
        CreationDate = DateTime.UtcNow;
    }
    public StudentId StudentId { get; }

    public string Filter { get; }

    public DateTime CreationDate { get; }
}
