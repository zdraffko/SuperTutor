namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.AddFavoriteFilter.Models;

internal class FavoriteFilterDatabaseQueryResponse
{
    public Guid? StudentId { get; init; }

    public string? Filter { get; init; }
}
