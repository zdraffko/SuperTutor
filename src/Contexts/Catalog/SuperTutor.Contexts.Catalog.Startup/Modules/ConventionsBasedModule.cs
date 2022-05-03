using Autofac;
using SuperTutor.Contexts.Catalog.Api;
using SuperTutor.Contexts.Catalog.Application;
using SuperTutor.Contexts.Catalog.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;

namespace SuperTutor.Contexts.Catalog.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(ICatalogApiAssemblyMarker).Assembly,
            typeof(ICatalogApplicationAssemblyMarker).Assembly,
            typeof(ICatalogInfrastructureAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
        };

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
