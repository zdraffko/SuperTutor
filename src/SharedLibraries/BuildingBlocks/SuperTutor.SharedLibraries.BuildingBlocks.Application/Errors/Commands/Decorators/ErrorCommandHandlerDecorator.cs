using FluentResults;
using Microsoft.Extensions.Logging;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Exceptions;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Errors.Commands.Decorators;

public class ErrorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly ILogger<TCommand> commandLogger;
    private readonly ICommandHandler<TCommand> decoratedCommandHandler;

    public ErrorCommandHandlerDecorator(ILogger<TCommand> commandLogger, ICommandHandler<TCommand> decoratedCommandHandler)
    {
        this.commandLogger = commandLogger;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var decoratedResult = await decoratedCommandHandler.Handle(command, cancellationToken);
            if (decoratedResult.IsFailed)
            {
                commandLogger.LogInformation("Command {CommandName} failed with result {Result}", typeof(TCommand).FullName, decoratedResult);
            }

            return decoratedResult;
        }
        catch (InvariantValidationException exception)
        {
            commandLogger.LogError(exception, "Command {CommandName} broke invariant {InvariantName} with message {ErrorMessage}", typeof(TCommand).FullName, exception.BrokenInvariant.GetType().FullName, exception.BrokenInvariant.ErrorMessage);

            return Result.Fail(exception.BrokenInvariant.ErrorMessage);
        }
        catch (Exception exception)
        {
            commandLogger.LogError(exception, "Command {CommandName} threw an unexpected exception with message {ErrorMessage}", typeof(TCommand).FullName, exception.Message);

            return Result.Fail("An unexpected error has occurred");
        }
    }
}

public class ErrorCommandHandlerDecorator<TCommand, TPayload> : ICommandHandler<TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    private readonly ILogger<TCommand> commandLogger;
    private readonly ICommandHandler<TCommand, TPayload> decoratedCommandHandler;

    public ErrorCommandHandlerDecorator(ILogger<TCommand> commandLogger, ICommandHandler<TCommand, TPayload> decoratedCommandHandler)
    {
        this.commandLogger = commandLogger;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result<TPayload>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var decoratedResult = await decoratedCommandHandler.Handle(command, cancellationToken);
            if (decoratedResult.IsFailed)
            {
                commandLogger.LogInformation("Command {CommandName} failed with result {Result}", typeof(TCommand).FullName, decoratedResult);
            }

            return decoratedResult;
        }
        catch (InvariantValidationException exception)
        {
            commandLogger.LogError(exception, "Command {CommandName} broke invariant {InvariantName} with message {ErrorMessage}", typeof(TCommand).FullName, exception.BrokenInvariant.GetType().FullName, exception.BrokenInvariant.ErrorMessage);

            return Result.Fail(exception.BrokenInvariant.ErrorMessage);
        }
        catch (Exception exception)
        {
            commandLogger.LogError(exception, "Command {CommandName} threw an unexpected exception with message {ErrorMessage}", typeof(TCommand).FullName, exception.Message);

            return Result.Fail("An unexpected error has occurred");
        }
    }
}
