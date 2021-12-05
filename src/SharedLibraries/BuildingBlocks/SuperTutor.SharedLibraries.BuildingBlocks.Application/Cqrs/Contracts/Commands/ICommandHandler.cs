using FluentResults;
using MediatR;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : Command
{
}

public interface ICommandHandler<in TCommand, TPayload> : IRequestHandler<TCommand, Result<TPayload>>
    where TCommand : Command<TPayload>
{
}
