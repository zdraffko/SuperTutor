using Autofac;
using SuperTutor.Contexts.Identity.Persistence;
using SuperTutor.Contexts.Identity.Startup.Startables.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

namespace SuperTutor.Contexts.Identity.Startup.IocContainerModules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DbInitializerStartable>().AsSelf().As<IStartable>().SingleInstance();

        builder.RegisterType<IdentityDbInitializer>().As<IDbInitializer>();
    }
}
