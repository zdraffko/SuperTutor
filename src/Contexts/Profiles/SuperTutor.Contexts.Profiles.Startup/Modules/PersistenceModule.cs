using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Persistence;
using SuperTutor.Contexts.Profiles.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Contracts;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Services;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterDbComponents(builder);
        RegisterRepositories(builder);
    }

    private void RegisterDbComponents(ContainerBuilder builder)
    {
        builder.RegisterType<ProfilesDbContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
    }
}
