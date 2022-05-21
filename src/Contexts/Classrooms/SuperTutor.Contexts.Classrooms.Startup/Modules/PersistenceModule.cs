using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms;
using SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Shared;

namespace SuperTutor.Contexts.Catalog.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<ClassroomDbContext>()
        .As<DbContext>()
        .As<IClassroomsDbContext>()
        .InstancePerLifetimeScope();
}
