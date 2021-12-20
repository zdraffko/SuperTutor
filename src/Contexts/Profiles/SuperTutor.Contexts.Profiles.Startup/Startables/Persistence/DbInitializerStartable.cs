using Autofac;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

namespace SuperTutor.Contexts.Profiles.Startup.Startables.Persistence;

internal class DbInitializerStartable : IStartable
{
    private readonly IDbInitializer dbInitializer;

    public DbInitializerStartable(IDbInitializer dbInitializer)
    {
        this.dbInitializer = dbInitializer;
    }

    public void Start() => dbInitializer.Initialize();
}
