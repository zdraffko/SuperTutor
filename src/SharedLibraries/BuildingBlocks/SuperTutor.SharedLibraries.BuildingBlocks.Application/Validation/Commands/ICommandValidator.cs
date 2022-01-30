using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

public interface ICommandValidator<in TCommand>
    where TCommand : Command
{
    Result Validate(TCommand command);
}

public interface ICommandValidator<in TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    Result Validate(TCommand command);
}
