using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Shared;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Students;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.TutorProfiles;

namespace SuperTutor.Contexts.Catalog.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<CatalogDbContext>()
        .As<DbContext>()
        .As<IStudentsDbContext>()
        .As<ITutorProfilesDbContext>()
        .InstancePerLifetimeScope();
}
