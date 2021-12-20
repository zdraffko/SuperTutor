using Autofac;
using SuperTutor.Contexts.Profiles.Startup.Startables.Persistence;

namespace SuperTutor.Contexts.Profiles.Startup.Modules.Persistence;

internal class StartablesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DbInitializerStartable>().AsSelf().As<IStartable>().SingleInstance();
    }
}
