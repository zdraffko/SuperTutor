using Autofac;
using SuperTutor.Contexts.Schedule.Api;
using SuperTutor.Contexts.Schedule.Application;
using SuperTutor.Contexts.Schedule.Infrastructure;
using SuperTutor.Contexts.Schedule.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence;

namespace SuperTutor.Contexts.Schedule.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(IScheduleApiAssemblyMarker).Assembly,
            typeof(IScheduleApplicationAssemblyMarker).Assembly,
            typeof(IScheduleInfrastructureAssemblyMarker).Assembly,
            typeof(ISchedulePersistenceAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
            typeof(IBuildingBlocksPersistenceAssemblyMarker).Assembly
        };

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
