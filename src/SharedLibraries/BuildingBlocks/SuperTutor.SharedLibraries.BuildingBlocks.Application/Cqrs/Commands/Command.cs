using FluentResults;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

public abstract class Command
{
    protected Command() => Id = Guid.NewGuid();

    protected Command(Guid id) => Id = id;

    public Guid Id { get; }
}

public abstract class Command<TPayload>
{
    protected Command() => Id = Guid.NewGuid();

    protected Command(Guid id) => Id = id;

    public Guid Id { get; }
}
