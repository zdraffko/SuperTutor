using Autofac;
using SuperTutor.Contexts.Payments.Api;
using SuperTutor.Contexts.Payments.Application;
using SuperTutor.Contexts.Payments.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;

namespace SuperTutor.Contexts.Payments.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(IPaymentsApiAssemblyMarker).Assembly,
            typeof(IPaymentsApplicationAssemblyMarker).Assembly,
            typeof(IPaymentsInfrastructureAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
        };

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
