using FluentResults;
using MediatR;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Queries;

public abstract class Query<TPayload> : IRequest<Result<TPayload>>
{
    protected Query() => Id = Guid.NewGuid();

    protected Query(Guid id) => Id = id;

    public Guid Id { get; }
}
