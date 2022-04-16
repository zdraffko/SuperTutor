namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.FavoriteFilters.Commands.Add.Models;

internal class AddFavoriteFilterResponse
{
    public Guid? StudentId { get; init; }

    public string? Filter { get; init; }

    public DateTime? CreationDate { get; init; }
}
