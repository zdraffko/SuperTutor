using Microsoft.EntityFrameworkCore;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Contracts;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> Commit(CancellationToken cancellationToken = default) => await dbContext.SaveChangesAsync(cancellationToken);
}
