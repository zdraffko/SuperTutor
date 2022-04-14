using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.FavoriteFilters.Commands.Add;

internal class AddFavoriteFilterCommandHandler : ICommandHandler<AddFavoriteFilterCommand>
{
    private readonly IFavoriteFilterRepository favoriteFilterRepository;

    public AddFavoriteFilterCommandHandler(IFavoriteFilterRepository favoriteFilterRepository) => this.favoriteFilterRepository = favoriteFilterRepository;

    public async Task<Result> Handle(AddFavoriteFilterCommand command, CancellationToken cancellationToken)
    {
        var favoriteFilter = new FavoriteFilter(command.StudentId, command.Filter);

        favoriteFilterRepository.Add(favoriteFilter);

        return await Task.FromResult(Result.Ok());
    }
}
