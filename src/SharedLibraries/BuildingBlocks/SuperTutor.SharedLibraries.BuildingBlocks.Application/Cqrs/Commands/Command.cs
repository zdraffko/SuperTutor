using FluentResults;
using MediatR;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

public abstract class Command : IRequest<Result>
{
    protected Command() => Id = Guid.NewGuid();

    protected Command(Guid id) => Id = id;

    public Guid Id { get; }
}

public abstract class Command<TPayload> : IRequest<Result<TPayload>>
{
    protected Command() => Id = Guid.NewGuid();

    protected Command(Guid id) => Id = id;

    public Guid Id { get; }
}
