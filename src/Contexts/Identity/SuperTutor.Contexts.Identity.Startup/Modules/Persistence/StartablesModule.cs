using Autofac;
using SuperTutor.Contexts.Identity.Startup.Startables.Persistence;

namespace SuperTutor.Contexts.Identity.Startup.Modules.Persistence;

internal class StartablesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DbInitializerStartable>().AsSelf().As<IStartable>().SingleInstance();
    }
}
