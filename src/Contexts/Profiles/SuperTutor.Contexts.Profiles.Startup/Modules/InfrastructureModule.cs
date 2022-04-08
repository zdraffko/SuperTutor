using Autofac;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder) => RegisterServices(builder);

    private static void RegisterServices(ContainerBuilder builder)
        => builder.RegisterType<IntegrationEventsService>().As<IIntegrationEventsService>().InstancePerLifetimeScope();
}
