using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.FavoriteFilters.Commands.Add;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Catalog.Api.FavoriteFilters.Controllers;

public class FavoriteFiltersController : ApiControllerMediatr
{
    public FavoriteFiltersController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<ActionResult> Add(AddFavoriteFilterCommand command, CancellationToken cancellationToken) => await Handle(command, cancellationToken);
}
