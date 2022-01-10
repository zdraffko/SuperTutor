using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Queries;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Queries;

public interface IQueryHandler<in TQuery, TPayload>
    where TQuery : Query<TPayload>
{
    Task<Result<TPayload>> Handle(TQuery query, CancellationToken cancellationToken);
}
