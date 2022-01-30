using Microsoft.AspNetCore.Mvc;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    protected async Task<ActionResult> Handle<TCommand>(
        ICommandHandler<TCommand> commandHandler,
        TCommand command,
        CancellationToken cancellationToken) where TCommand : Command
    {
        var commandResult = await commandHandler.Handle(command, cancellationToken);

        return commandResult.ToActionResult();
    }

    protected async Task<ActionResult<TPayload>> Handle<TCommand, TPayload>(
        ICommandHandler<TCommand, TPayload> commandHandler,
        TCommand command,
        CancellationToken cancellationToken) where TCommand : Command<TPayload>
    {
        var commandResult = await commandHandler.Handle(command, cancellationToken);

        return commandResult.ToActionResult();
    }

    protected async Task<ActionResult<TPayload>> Handle<TQuery, TPayload>(
        IQueryHandler<TQuery, TPayload> queryHandler,
        TQuery query,
        CancellationToken cancellationToken) where TQuery : Query<TPayload>
    {
        var queryResult = await queryHandler.Handle(query, cancellationToken);

        return queryResult.ToActionResult();
    }
}
