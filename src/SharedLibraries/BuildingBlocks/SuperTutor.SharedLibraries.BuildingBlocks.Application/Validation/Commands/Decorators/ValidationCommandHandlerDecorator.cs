using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands.Decorators;

public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly IEnumerable<ICommandValidator<TCommand>> commandValidators;
    private readonly ICommandHandler<TCommand> decoratedCommandHandler;

    public ValidationCommandHandlerDecorator(IEnumerable<ICommandValidator<TCommand>> commandValidators, ICommandHandler<TCommand> decoratedCommandHandler)
    {
        this.commandValidators = commandValidators;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
    {
        foreach (var commandValidator in commandValidators)
        {
            var commandValidationResult = commandValidator.Validate(command);
            if (commandValidationResult.IsFailed)
            {
                return commandValidationResult;
            }
        }

        return await decoratedCommandHandler.Handle(command, cancellationToken);
    }
}

public class ValidationCommandHandlerDecorator<TCommand, TPayload> : ICommandHandler<TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    private readonly IEnumerable<ICommandValidator<TCommand, TPayload>> commandValidators;
    private readonly ICommandHandler<TCommand, TPayload> decoratedCommandHandler;

    public ValidationCommandHandlerDecorator(IEnumerable<ICommandValidator<TCommand, TPayload>> commandValidators, ICommandHandler<TCommand, TPayload> decoratedCommandHandler)
    {
        this.commandValidators = commandValidators;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result<TPayload>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        foreach (var commandValidator in commandValidators)
        {
            var commandValidationResult = commandValidator.Validate(command);
            if (commandValidationResult.IsFailed)
            {
                return commandValidationResult;
            }
        }

        return await decoratedCommandHandler.Handle(command, cancellationToken);
    }
}
