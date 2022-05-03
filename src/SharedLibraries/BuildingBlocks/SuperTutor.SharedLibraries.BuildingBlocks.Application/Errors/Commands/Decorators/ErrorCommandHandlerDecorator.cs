using FluentResults;
using Microsoft.Extensions.Logging;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Exceptions;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Errors.Commands.Decorators;

public class ErrorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly ILogger<ErrorCommandHandlerDecorator<TCommand>> commandLogger;
    private readonly ICommandHandler<TCommand> decoratedCommandHandler;

    public ErrorCommandHandlerDecorator(ILogger<ErrorCommandHandlerDecorator<TCommand>> commandLogger, ICommandHandler<TCommand> decoratedCommandHandler)
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
                commandLogger.LogInformation("Command {@Command} failed with result {@Result}", command, decoratedResult);
            }

            return decoratedResult;
        }
        catch (InvariantValidationException invariantValidationException)
        {
            commandLogger.LogInformation(invariantValidationException, "Command {@Command} broke invariant {@Invariant} with message {ErrorMessage}", command, invariantValidationException.BrokenInvariant, invariantValidationException.BrokenInvariant.ErrorMessage);

            return Result.Fail(invariantValidationException.BrokenInvariant.ErrorMessage);
        }
        catch (Exception exception)
        {
            commandLogger.LogError(exception, "Command {@Command} threw an unexpected exception with message {ErrorMessage}", command, exception.Message);

            return Result.Fail("An unexpected error has occurred");
        }
    }
}

public class ErrorCommandHandlerDecorator<TCommand, TPayload> : ICommandHandler<TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    private readonly ILogger<ErrorCommandHandlerDecorator<TCommand, TPayload>> commandLogger;
    private readonly ICommandHandler<TCommand, TPayload> decoratedCommandHandler;

    public ErrorCommandHandlerDecorator(ILogger<ErrorCommandHandlerDecorator<TCommand, TPayload>> commandLogger, ICommandHandler<TCommand, TPayload> decoratedCommandHandler)
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
                commandLogger.LogInformation("Command {@Command} failed with result {@Result}", command, decoratedResult);
            }

            return decoratedResult;
        }
        catch (InvariantValidationException invariantValidationException)
        {
            commandLogger.LogInformation(invariantValidationException, "Command {@Command} broke invariant {@Invariant} with message {ErrorMessage}", command, invariantValidationException.BrokenInvariant, invariantValidationException.BrokenInvariant.ErrorMessage);

            return Result.Fail(invariantValidationException.BrokenInvariant.ErrorMessage);
        }
        catch (Exception exception)
        {
            commandLogger.LogError(exception, "Command {@Command} threw an unexpected exception with message {ErrorMessage}", command, exception.Message);

            return Result.Fail("An unexpected error has occurred");
        }
    }
}
