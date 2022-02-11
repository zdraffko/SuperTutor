using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents.Commands.Decorators;

public class IntegrationEventsCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly IIntegrationEventsService integrationEventsService;
    private readonly ICommandHandler<TCommand> decoratedCommandHandler;

    public IntegrationEventsCommandHandlerDecorator(IIntegrationEventsService integrationEventsService, ICommandHandler<TCommand> decoratedCommandHandler)
    {
        this.integrationEventsService = integrationEventsService;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var decoratedResult = await decoratedCommandHandler.Handle(command, cancellationToken);
        if (decoratedResult.IsFailed)
        {
            return decoratedResult;
        }

        await integrationEventsService.DispatchAll(cancellationToken);

        return decoratedResult;
    }
}

public class IntegrationEventsCommandHandlerDecorator<TCommand, TPayload> : ICommandHandler<TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    private readonly IIntegrationEventsService integrationEventsService;
    private readonly ICommandHandler<TCommand, TPayload> decoratedCommandHandler;

    public IntegrationEventsCommandHandlerDecorator(IIntegrationEventsService integrationEventsService, ICommandHandler<TCommand, TPayload> decoratedCommandHandler)
    {
        this.integrationEventsService = integrationEventsService;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result<TPayload>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var decoratedResult = await decoratedCommandHandler.Handle(command, cancellationToken);
        if (decoratedResult.IsFailed)
        {
            return decoratedResult;
        }

        await integrationEventsService.DispatchAll(cancellationToken);

        return decoratedResult;
    }
}
