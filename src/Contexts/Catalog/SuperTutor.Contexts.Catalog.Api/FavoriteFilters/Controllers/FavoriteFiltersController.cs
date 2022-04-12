using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.FavoriteFilters.Commands.Add;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Api.FavoriteFilters.Controllers;

public class FavoriteFiltersController : ApiController
{
    private readonly ICommandHandler<AddFavoriteFilterCommand> addFavoriteFilterCommandHandler;

    public FavoriteFiltersController(ICommandHandler<AddFavoriteFilterCommand> addFavoriteFilterCommandHandler) => this.addFavoriteFilterCommandHandler = addFavoriteFilterCommandHandler;

    [HttpPost]
    public async Task<ActionResult> Add(AddFavoriteFilterCommand command, CancellationToken cancellationToken) => await Handle(addFavoriteFilterCommandHandler, command, cancellationToken);
}
