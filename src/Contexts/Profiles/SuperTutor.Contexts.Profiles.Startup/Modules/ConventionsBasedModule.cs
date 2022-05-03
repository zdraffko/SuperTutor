using Autofac;
using SuperTutor.Contexts.Profiles.Api;
using SuperTutor.Contexts.Profiles.Application;
using SuperTutor.Contexts.Profiles.Infrastructure;
using SuperTutor.Contexts.Profiles.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(IProfilesApiAssemblyMarker).Assembly,
            typeof(IProfilesApplicationAssemblyMarker).Assembly,
            typeof(IProfilesInfrastructureAssemblyMarker).Assembly,
            typeof(IProfilesPersistenceAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
            typeof(IBuildingBlocksPersistenceAssemblyMarker).Assembly
        };

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
