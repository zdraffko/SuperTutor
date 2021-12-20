using Autofac;
using SuperTutor.Contexts.Profiles.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

namespace SuperTutor.Contexts.Profiles.Startup.Modules.Persistence;

internal class InitializersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProfilesDbInitializer>().As<IDbInitializer>();
    }
}
