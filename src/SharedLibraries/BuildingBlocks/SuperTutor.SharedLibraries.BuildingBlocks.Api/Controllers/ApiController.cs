using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Queries;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    private readonly IMediator mediator;

    protected ApiController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected async Task<ActionResult> Handle(Command command, CancellationToken cancellationToken)
    {
        var commandResult = await mediator.Send(command, cancellationToken);

        return commandResult.ToActionResult();
    }

    protected async Task<ActionResult<TPayload>> Handle<TPayload>(Command<TPayload> command, CancellationToken cancellationToken)
    {
        var commandResult = await mediator.Send(command, cancellationToken);

        return commandResult.ToActionResult();
    }

    protected async Task<ActionResult<TPayload>> Handle<TPayload>(Query<TPayload> query, CancellationToken cancellationToken)
    {
        var queryResult = await mediator.Send(query, cancellationToken);

        return queryResult.ToActionResult();
    }
}
