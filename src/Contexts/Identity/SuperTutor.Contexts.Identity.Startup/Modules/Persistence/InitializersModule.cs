using Autofac;
using SuperTutor.Contexts.Identity.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

namespace SuperTutor.Contexts.Identity.Startup.Modules.Persistence;

internal class InitializersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<IdentityDbInitializer>().As<IDbInitializer>();
    }
}
