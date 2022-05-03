using Autofac;
using SuperTutor.Contexts.Identity.Api;
using SuperTutor.Contexts.Identity.Application;
using SuperTutor.Contexts.Identity.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(IIdentityApiAssemblyMarker).Assembly,
            typeof(IIdentityApplicationAssemblyMarker).Assembly,
            typeof(IIdentityInfrastructureAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
        };

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
