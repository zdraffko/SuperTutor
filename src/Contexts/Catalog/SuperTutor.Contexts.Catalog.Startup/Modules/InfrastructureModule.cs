using Autofac;

namespace SuperTutor.Contexts.Catalog.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder) => RegisterServices(builder);

    private static void RegisterServices(ContainerBuilder builder)
    {

    }
}
