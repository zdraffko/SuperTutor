namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken);
}
