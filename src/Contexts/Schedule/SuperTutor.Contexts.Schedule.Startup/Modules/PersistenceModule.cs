using Autofac;
using SuperTutor.Contexts.Schedule.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterAssemblyTypes(typeof(ISchedulePersistenceAssemblyMarker).Assembly)
        .AsClosedTypesOf(typeof(IAggregateRootEventsRepository<,,>))
        .InstancePerLifetimeScope();
}
