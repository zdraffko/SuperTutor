using FluentResults;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

public interface IQueryHandler<in TQuery, TPayload>
    where TQuery : Query<TPayload>
{
    Task<Result<TPayload>> Handle(TQuery query, CancellationToken cancellationToken);
}
