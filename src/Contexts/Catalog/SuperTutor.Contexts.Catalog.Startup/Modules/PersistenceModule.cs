using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Persistence.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.UnitOfWork;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Services;

namespace SuperTutor.Contexts.Catalog.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterDbComponents(builder);
        RegisterRepositories(builder);
    }

    private static void RegisterDbComponents(ContainerBuilder builder)
    {
        builder.RegisterType<CatalogDbContext>()
            .As<DbContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }

    private static void RegisterRepositories(ContainerBuilder builder)
    {
    }
}
