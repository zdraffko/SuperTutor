using Autofac;
using SuperTutor.Contexts.Classrooms.Api;
using SuperTutor.Contexts.Classrooms.Application;
using SuperTutor.Contexts.Classrooms.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;

namespace SuperTutor.Contexts.Classrooms.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(IClassroomApiAssemblyMarker).Assembly,
            typeof(IClassroomApplicationAssemblyMarker).Assembly,
            typeof(IClassroomInfrastructureAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
        };

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
