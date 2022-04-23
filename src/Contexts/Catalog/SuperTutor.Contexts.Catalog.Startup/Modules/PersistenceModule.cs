using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Persistence.Shared;
using SuperTutor.Contexts.Catalog.Persistence.Students;
using SuperTutor.Contexts.Catalog.Persistence.TutorProfiles;

namespace SuperTutor.Contexts.Catalog.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<CatalogDbContext>()
        .As<DbContext>()
        .As<IStudentsDbContext>()
        .As<ITutorProfilesDbContext>()
        .InstancePerLifetimeScope();
}
