using Microsoft.EntityFrameworkCore;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

public abstract class DefaultDbInitializer : IDbInitializer
{
    private readonly DbContext dbContext;

    protected internal DefaultDbInitializer(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual void Initialize() => dbContext.Database.Migrate();
}
