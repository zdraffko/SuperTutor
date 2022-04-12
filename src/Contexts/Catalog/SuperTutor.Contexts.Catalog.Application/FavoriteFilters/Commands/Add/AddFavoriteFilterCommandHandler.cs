using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.FavoriteFilters.Commands.Add;

internal class AddFavoriteFilterCommandHandler : ICommandHandler<AddFavoriteFilterCommand>
{
    public Task<Result> Handle(AddFavoriteFilterCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}
