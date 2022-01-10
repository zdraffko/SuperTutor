using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : Command
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    Task<Result<TPayload>> Handle(TCommand command, CancellationToken cancellationToken);
}
