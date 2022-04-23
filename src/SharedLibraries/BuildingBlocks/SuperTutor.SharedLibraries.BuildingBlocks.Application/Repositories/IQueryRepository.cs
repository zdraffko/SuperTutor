namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Repositories;

public interface IQueryRepository
{
    Task<IEnumerable<TPayload>> GetAll<TPayload>(string query, object? parameters, CancellationToken cancellationToken);
}
